﻿#region Using Statements
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Mumble.FirstGame.Client;
using Mumble.FirstGame.Client.Online;
using Mumble.FirstGame.Core.Action;
using Mumble.FirstGame.Core.Action.Fire;
using Mumble.FirstGame.Core.Action.Movement;
using Mumble.FirstGame.Core.ActionResult;
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
        IScene scene;
        public GameMain()
        {   
            graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";
            
            //Should line up with server tick rate
            
            RegisterServices();
            graphics.PreferredBackBufferWidth = settings.AspectRatio.Item1;
            graphics.PreferredBackBufferHeight = settings.AspectRatio.Item2;
            TargetElapsedTime = settings.TickRate;
            graphics.IsFullScreen = settings.FullScreen;
            IsFixedTimeStep = true;

        }
        private void RegisterServices()
        {

            provider = new GameServiceContainer();
            provider.AddService<IGameSettings>(new GameSettings());
            
            provider.AddService<IEntityContainer>(new BattleEntityContainer());
            provider.AddService<ISceneFactory>(new BattleSceneFactory());
            provider.AddService<ICollisionSystem>(new CollisionSystem(provider.GetService<IEntityContainer>()));
            
            provider.AddService<IActionResultFactory>(new ActionResultFactory(provider.GetService<IEntityContainer>()));
            provider.AddService<IActionFactory>(new ActionFactory(provider.GetService<IEntityContainer>()));
            provider.AddService<IEntityFactory>(new EntityFactory());
            provider.AddService<ISerializationFactoryContainer>(new SerializationFactoryContainer(provider.GetService<IActionFactory>(), provider.GetService<IActionResultFactory>(), provider.GetService<IEntityFactory>()));

            settings = provider.GetService<IGameSettings>();
            
            if (settings.ClientType == ClientType.Solo)
            {
                scene = provider.GetService<ISceneFactory>().Create(provider.GetService<IEntityContainer>(), new List<IActionAdapter>(),provider.GetService<ICollisionSystem>());
                provider.AddService<IGameClient>(new SoloGameClient(scene));
            }
            else if (settings.ClientType == ClientType.Online)
            {
                //We need an empty scene when online - server should tell us how to fill it in, not scene factory
                //TODO - maybe do this a different way? it's awk right now, this scene isn't actually being used
                scene = new BattleScene(provider.GetService<IEntityContainer>(), new List<IActionAdapter>(),new List<IActionResultAdapter>(), provider.GetService<ICollisionSystem>(), new BattleSceneBoundary(100,100, new IBackground[100,100]));
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

            IOwnerIdentifier owner = client.Register();
            List<IActionResult> results = client.Init(owner);

            SetWindowScale();

            //TODO - this is a hacky way to get the player entity
            EntitiesCreatedActionResult createdResult = (EntitiesCreatedActionResult)results.Where(x => x is EntitiesCreatedActionResult).FirstOrDefault();
            player = (Player)createdResult.Entities.Where(x => x.OwnerIdentifier.Equals(owner)).FirstOrDefault();
            EntitySprites[player] = SpriteMetadataUtil.CreateSpriteMetadata(player);

            InitializeUISprites();
            InitializeBackgroundSprites();
            ApplyResults(results);
            base.Initialize();
        }
        //Maybe remove this?
        private void SetWindowScale()
        {
            windowScale = 1f;
            //float width = (float)graphics.GraphicsDevice.Viewport.Width / ((float)(scene.Boundary.Width * settings.SpritePixelSpacing)) ;
            //float height = (float)graphics.GraphicsDevice.Viewport.Height / ((float)(scene.Boundary.Height * settings.SpritePixelSpacing));
            //if (height > width)
            //{
            //    windowScale = width;
            //}
            //else
            //{
            //    windowScale = height;
            //}
        }
        private void InitializeUISprites()
        {
            UISprites.Add(new CursorMetadata());
        }
        private void InitializeBackgroundSprites()
        {
            IBackground[,] backgrounds = scene.Boundary.Backgrounds;
            for(int i = 0; i< scene.Boundary.Width; i++)
            {
                for (int j = 0; j < scene.Boundary.Height; j++)
                {
                    if (backgrounds[i,j] is Wall)
                    {
                        BackgroundSprites.Add(new WallSpriteMetadata(new Vector2(i, j)));
                    }
                    
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
            actions.AddIfNotNull(mouseHandler.HandleMouseClick(player, ScalePosition(EntitySprites[player].GetPosition())));

            results = client.Update(actions);
            ApplyResults(results);
            //DebugUtils.PrintActions(actions);
            base.Update(gameTime);
        }
        private Vector2 ScalePosition(Vector2 position)
        {
            return new Vector2((position.X * settings.SpritePixelSpacing * windowScale)+settings.BorderPixelSize,
                (position.Y * settings.SpritePixelSpacing * windowScale) + settings.BorderPixelSize);
        }
        private Vector2 DescalePosition(Vector2 position)
        {
            return new Vector2((position.X - settings.BorderPixelSize) / (settings.SpritePixelSpacing * windowScale),
                (position.Y - settings.BorderPixelSize) / (settings.SpritePixelSpacing * windowScale));
        }
        private Vector2 ScaleSize(Vector2 scale)
        {
            return new Vector2(scale.X * settings.ScreenScale * windowScale, scale.Y * settings.ScreenScale * windowScale);
        }
        private void ApplyResults(List<IActionResult> results)
        {
            if (results.Count > 0)
            {
                //TODO - make a result handler class probably
                foreach (EntitiesCreatedActionResult result in results.Where(x => x is EntitiesCreatedActionResult))
                {
                    foreach (IEntity entity in result.Entities)
                    {
                        EntitySprites[entity] = SpriteMetadataUtil.CreateSpriteMetadata(entity);


                    }
                    
                }
                foreach (MoveActionResult result in results.Where(x => x is MoveActionResult))
                {
                    if (!EntitySprites.Keys.Contains(result.Entity))
                    {
                        EntitySprites[result.Entity] = SpriteMetadataUtil.CreateSpriteMetadata(result.Entity);
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
                foreach(AttackActionResult result in results.Where(x => x is AttackActionResult))
                {
                    EntitySprites[result.Source].AnimateAttack();
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
            spriteBatch.Begin(SpriteSortMode.Deferred,BlendState.AlphaBlend,SamplerState.PointClamp);
            Vector2 mousePosition = DescalePosition(Mouse.GetState().Position.ToVector2());
            foreach (AbstractSpriteMetadata sprite in BackgroundSprites)
            {
                spriteBatch.Draw(sprite.GetImage(contentImages),
                    ScalePosition(sprite.GetPosition()), 
                    sprite.GetSpritesheetRectange(mousePosition), 
                    sprite.GetColor(), 
                    sprite.GetRotation(), 
                    sprite.GetOrigin(), 
                    ScaleSize(sprite.GetScale()), 
                    SpriteEffects.None, 0f);
            }
            foreach (IEntity entity in EntitySprites.Keys)
            {
                AbstractSpriteMetadata sprite = EntitySprites[entity];
                spriteBatch.Draw(sprite.GetImage(contentImages),
                    ScalePosition(sprite.GetPosition()), 
                    sprite.GetSpritesheetRectange(mousePosition), 
                    sprite.GetColor(), 
                    sprite.GetRotation(), 
                    sprite.GetOrigin(),
                    ScaleSize(sprite.GetScale()), 
                    SpriteEffects.None, 0f);
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
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
