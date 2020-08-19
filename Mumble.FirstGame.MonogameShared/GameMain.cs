#region Using Statements
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Mumble.FirstGame.Client;
using Mumble.FirstGame.Client.Online;
using Mumble.FirstGame.Core;
using Mumble.FirstGame.Core.Action;
using Mumble.FirstGame.Core.Action.Fire;
using Mumble.FirstGame.Core.Action.Movement;
using Mumble.FirstGame.Core.ActionResult;
using Mumble.FirstGame.Core.ActionResult.Meta;
using Mumble.FirstGame.Core.Background;
using Mumble.FirstGame.Core.Entity;
using Mumble.FirstGame.Core.Entity.Components.Position;
using Mumble.FirstGame.Core.Entity.Enemy;
using Mumble.FirstGame.Core.Entity.OwnerIdentifier;
using Mumble.FirstGame.Core.Entity.Player;
using Mumble.FirstGame.Core.Entity.Projectile;
using Mumble.FirstGame.Core.Scene;
using Mumble.FirstGame.Core.Scene.Battle;
using Mumble.FirstGame.Core.Scene.Battle.SceneBoundary;
using Mumble.FirstGame.Core.Scene.EntityContainer;
using Mumble.FirstGame.Core.Scene.Factory;
using Mumble.FirstGame.Core.System.Collision;
using Mumble.FirstGame.MonogameShared.Settings;
using Mumble.FirstGame.MonogameShared.SpriteMetadata;
using Mumble.FirstGame.MonogameShared.SpriteMetadata.Background;
using Mumble.FirstGame.MonogameShared.Utils;
using Mumble.FirstGame.Serialization.OnlineActionResult;
using Mumble.FirstGame.Serialization.Protobuf.Factory;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;

#endregion

namespace Mumble.FirstGame.MonogameShared
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class GameMain : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        IGameClient client;
        Dictionary<IEntity, AbstractSpriteMetadata> EntitySprites = new Dictionary<IEntity, AbstractSpriteMetadata>();
        List<AbstractSpriteMetadata> UISprites = new List<AbstractSpriteMetadata>();
        List<AbstractSpriteMetadata> BackgroundSprites = new List<AbstractSpriteMetadata>();
        Player player;
        ContentImages contentImages;
        MovementKeyHandler keyHandler;
        MouseHandler mouseHandler;
        GameServiceContainer provider;
        float windowScale;
        IGameSettings settings;
        IScene scene => client.CurrentScene;
        ScalingUtils scalingUtils;
        public GameMain()
        {   
            graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";
            
            
            RegisterServices();
            graphics.PreferredBackBufferWidth = settings.AspectRatio.Item1;
            graphics.PreferredBackBufferHeight = settings.AspectRatio.Item2;

            //Should line up with server tick rate
            TargetElapsedTime = settings.TickRate;
            graphics.IsFullScreen = settings.FullScreen;
            IsFixedTimeStep = true;

        }
        private void RegisterServices()
        {

            provider = new GameServiceContainer();
            provider.AddService<IGameSettings>(new GameSettings());
            
            
            provider.AddService<IActionResultFactory>(new ActionResultFactory(provider.GetService<IEntityContainer>()));
            provider.AddService<IActionFactory>(new ActionFactory(provider.GetService<IEntityContainer>()));
            provider.AddService<IEntityFactory>(new EntityFactory());
            provider.AddService<ISerializationFactoryContainer>(new SerializationFactoryContainer(provider.GetService<IActionFactory>(), provider.GetService<IActionResultFactory>(), provider.GetService<IEntityFactory>()));

            settings = provider.GetService<IGameSettings>();
            
            if (settings.ClientType == ClientType.Solo)
            {
                //TODO - the client will likely at some point need a copy of game settings
                provider.AddService<IGameClient>(new SoloGameClient());
            }
            else if (settings.ClientType == ClientType.Online)
            {
                //We need an empty scene when online - server should tell us how to fill it in, not scene factory
                //TODO - maybe do this a different way? it's awk right now, this scene isn't actually being used
                provider.AddService<IGameClient>(new OnlineGameClient(provider.GetService<IEntityContainer>(), settings, provider.GetService<ISerializationFactoryContainer>()));
            }
            client = provider.GetService<IGameClient>();
        }
        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            keyHandler = new MovementKeyHandler();
            mouseHandler = new MouseHandler();

            client.Register();
            List<IActionResult> results = client.Init();
            ApplyResults(results);
            SetWindowScale();


            base.Initialize();
        }
        private void SetWindowScale()
        {
            windowScale = 1f;
            scalingUtils = new ScalingUtils(settings, windowScale);
        }
        private void InitializeBattleScene(List<IActionResult> results)
        {
            //TODO - this is a hacky way to get the player entity
            EntitiesCreatedActionResult createdResult = (EntitiesCreatedActionResult)results.Where(x => x is EntitiesCreatedActionResult).FirstOrDefault();
            player = (Player)createdResult.Entities.Where(x => x.OwnerIdentifier.Equals(client.Owner)).FirstOrDefault();
            EntitySprites[player] = SpriteMetadataFactory.CreateSpriteMetadata(player);

            InitializeUISprites();
            InitializeBackgroundSprites();
            ApplyResults(results);
        }
        private void InitializeUISprites()
        {
            UISprites.Add(new CursorMetadata());
        }
        private void InitializeBackgroundSprites()
        {
            HashSet<IBackground> backgrounds = ((BattleScene)scene).Boundary.Backgrounds;
            foreach(IBackground background in backgrounds)
            {
                if (background is Wall)
                {
                    BackgroundSprites.Add(new WallSpriteMetadata((Wall)background));
                }
                else if (background is Floor)
                {
                    BackgroundSprites.Add(new FloorSpriteMetadata(new Vector2(background.Position.X, background.Position.Y), background.Scale));
                }
            }
        }
        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            contentImages = new ContentImages();
            contentImages.LoadContent(Content,graphics.GraphicsDevice);

        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // For Mobile devices, this logic will close the Game when the Back button is pressed
            // Exit() is obsolete on iOS
#if !__IOS__ && !__TVOS__
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

#endif

            List<IActionResult> results = new List<IActionResult>();
            List<IAction> actions = new List<IAction>();

            actions.AddIfNotNull(keyHandler.HandleKeyPress(player));
            actions.AddIfNotNull(mouseHandler.HandleMouseClick(player, scalingUtils.ScalePosition(EntitySprites[player].GetPosition())));

            results = client.Update(actions);
            ApplyResults(results);
            //DebugUtils.PrintActions(actions);
            base.Update(gameTime);
        }

        private void ApplyResults(List<IActionResult> results)
        {
            if (results.Count > 0)
            {
                foreach(SceneEnteredActionResult result in results.Where(x => x is SceneEnteredActionResult))
                {
                    
                    if (scene is BattleScene)
                    {
                        InitializeBattleScene(results.Where(x => x != result).ToList());
                    }
                    return;
                }
                if (scene is BattleScene)
                {
                    //TODO - make a result handler class probably
                    foreach (EntitiesCreatedActionResult result in results.Where(x => x is EntitiesCreatedActionResult))
                    {
                        foreach (IEntity entity in result.Entities)
                        {
                            EntitySprites[entity] = SpriteMetadataFactory.CreateSpriteMetadata(entity);


                        }

                    }
                    foreach (MoveActionResult result in results.Where(x => x is MoveActionResult))
                    {
                        if (!EntitySprites.Keys.Contains(result.Entity))
                        {
                            EntitySprites[result.Entity] = SpriteMetadataFactory.CreateSpriteMetadata(result.Entity);
                        }
                        EntitySprites[result.Entity].AnimateMovement(result);
                        if (result.OutOfBounds)
                        {
                            if (result.Entity != player)
                            {
                                EntitySprites.Remove(result.Entity);
                            }

                        }
                        else
                        {
                            //this is a little awkwardly placed I think
                            PositionComponent newPos = new PositionComponent(result.XPos, result.YPos);
                            result.Entity.PositionComponent.Move(newPos);
                        }

                    }
                    foreach (EntityDestroyedActionResult result in results.Where(x => x is EntityDestroyedActionResult))
                    {
                        EntitySprites.Remove(result.Entity);
                    }
                    foreach (DamageActionResult result in results.Where(x => x is DamageActionResult))
                    {
                        EntitySprites[result.Entity].AnimateDamage();
                    }
                    foreach (AttackActionResult result in results.Where(x => x is AttackActionResult))
                    {
                        EntitySprites[result.Source].AnimateAttack();
                    }
                }
            }
        }
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin(SpriteSortMode.BackToFront,BlendState.AlphaBlend,SamplerState.PointClamp);
            Vector2 mousePosition = scalingUtils.DescalePosition(Mouse.GetState().Position.ToVector2());
            foreach (AbstractSpriteMetadata sprite in BackgroundSprites)
            {
                Vector2 pos = sprite.GetPosition();
                spriteBatch.Draw(sprite.GetImage(contentImages),
                    scalingUtils.ScalePosition(sprite.GetPosition()),
                    sprite.GetSpritesheetRectange(mousePosition),
                    sprite.GetColor(),
                    sprite.GetRotation(),
                    sprite.GetOrigin(),
                    scalingUtils.ScaleSize(sprite.GetScale()),
                    SpriteEffects.None,
                    sprite.GetLayerDepth());
            }
            foreach (IEntity entity in EntitySprites.Keys)
            {
                AbstractSpriteMetadata sprite = EntitySprites[entity];
                spriteBatch.Draw(sprite.GetImage(contentImages),
                    scalingUtils.ScalePosition(sprite.GetPosition()), 
                    sprite.GetSpritesheetRectange(mousePosition), 
                    sprite.GetColor(), 
                    sprite.GetRotation(), 
                    sprite.GetOrigin(),
                    scalingUtils.ScaleSize(sprite.GetScale()), 
                    SpriteEffects.None,
                    sprite.GetLayerDepth());
            }
            foreach (AbstractSpriteMetadata sprite in UISprites)
            {
                spriteBatch.Draw(sprite.GetImage(contentImages),
                    sprite.GetPosition(),
                    sprite.GetSpritesheetRectange(mousePosition), 
                    sprite.GetColor(), 
                    sprite.GetRotation(), 
                    sprite.GetOrigin(), 
                    sprite.GetScale(), 
                    SpriteEffects.None, 0f);
            }
            //DebugUtils.DrawGrid(spriteBatch, graphics.GraphicsDevice, scalingUtils, scene.Boundary.Width, scene.Boundary.Height);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
