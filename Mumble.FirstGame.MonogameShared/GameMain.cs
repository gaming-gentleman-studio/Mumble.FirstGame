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
using Mumble.FirstGame.Core.Entity;
using Mumble.FirstGame.Core.Entity.Components.Position;
using Mumble.FirstGame.Core.Entity.Enemy;
using Mumble.FirstGame.Core.Entity.OwnerIdentifier;
using Mumble.FirstGame.Core.Entity.Player;
using Mumble.FirstGame.Core.Entity.Projectile;
using Mumble.FirstGame.Core.Scene;
using Mumble.FirstGame.Core.Scene.Battle;
using Mumble.FirstGame.Core.Scene.EntityContainer;
using Mumble.FirstGame.MonogameShared.Settings;
using Mumble.FirstGame.MonogameShared.SpriteMetadata;
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
        Dictionary<IEntity, ISpriteMetadata> EntitySprites = new Dictionary<IEntity, ISpriteMetadata>();
        List<ISpriteMetadata> UISprites = new List<ISpriteMetadata>();
        Player player;
        int scaling = 5;
        ContentImages contentImages;
        MovementKeyHandler keyHandler;
        MouseHandler mouseHandler;
        GameServiceContainer provider;
        IGameSettings settings;
        public GameMain()
        {   
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            
            //Should line up with server tick rate
            
            RegisterServices();
            settings = provider.GetService<IGameSettings>();
            TargetElapsedTime = settings.TickRate;
            graphics.IsFullScreen = settings.FullScreen;
            IsFixedTimeStep = true;

        }
        private void RegisterServices()
        {

            provider = new GameServiceContainer();
            provider.AddService<IGameSettings>(new GameSettings());
            provider.AddService<IEntityContainer>(new BattleEntityContainer());
            provider.AddService<IScene>(new BattleScene(provider.GetService<IEntityContainer>(),new List<IActionAdapter>()));
            provider.AddService<IActionResultFactory>(new ActionResultFactory(provider.GetService<IEntityContainer>()));
            provider.AddService<IActionFactory>(new ActionFactory(provider.GetService<IEntityContainer>()));
            provider.AddService<IEntityFactory>(new EntityFactory());
            provider.AddService<IFactoryContainer>(new FactoryContainer(provider.GetService<IActionFactory>(), provider.GetService<IActionResultFactory>(), provider.GetService<IEntityFactory>()));
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
            //solo
            if (settings.ClientType == ClientType.Solo)
            {
                client = new SoloGameClient(provider.GetService<IScene>());
            }
            else if (settings.ClientType == ClientType.Online)
            {
                client = new OnlineGameClient(provider.GetService<IEntityContainer>(), settings, provider.GetService<IFactoryContainer>());
            }
            IOwnerIdentifier owner = client.Register();
            List<IActionResult> results = client.Init(owner);
            EntitiesCreatedActionResult createdResult = (EntitiesCreatedActionResult)results.Where(x => x is EntitiesCreatedActionResult).FirstOrDefault();
            player = (Player)createdResult.Entities.Where(x => x.OwnerIdentifier.Equals(owner)).FirstOrDefault();
            EntitySprites[player] = SpriteMetadataUtil.CreateSpriteMetadata(player);

            InitializeUISprites();    
            ApplyResults(results);
            // TODO: Add your initialization logic here
            base.Initialize();
        }
        private void InitializeUISprites()
        {
            UISprites.Add(new CursorMetadata());
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
            contentImages.LoadContent(Content);

            //TODO: use this.Content to load your game content here 
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
            Mouse.GetState().Position.ToVector2().Normalize();
            //TODO - move all this stuff into separate class
            // Really need to clean this up lol, will do later
            List<IActionResult> results = new List<IActionResult>();
            List<IAction> actions = new List<IAction>();
            actions.AddIfNotNull(keyHandler.HandleKeyPress(player));
            actions.AddIfNotNull(mouseHandler.HandleMouseClick(player, EntitySprites[player].GetPosition()));
            if (actions.Count > 0)
            {
                Debug.WriteLine("Tried to move {0}", actions.Where(x => x is MoveAction && ((MoveAction)x).Entity is Player).Count());
            }
            results = client.Update(actions);
            ApplyResults(results);
            //DebugUtils.PrintActions(actions);
            base.Update(gameTime);
        }
        private void ApplyResults(List<IActionResult> results)
        {
            if (results.Count > 0)
            {
                Debug.WriteLine("Moved {0}",results.Where(x => x is MoveActionResult && ((MoveActionResult)x).Entity is Player).Count());
                
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
                    EntitySprites[result.Entity].Animate();
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
            
            foreach (IEntity entity in EntitySprites.Keys)
            {
                ISpriteMetadata sprite = EntitySprites[entity];
                spriteBatch.Draw(sprite.GetImage(contentImages), sprite.GetPosition(), sprite.GetSpritesheetRectange(), Color.DarkGray, sprite.GetRotation(), sprite.GetOrigin(), sprite.GetScale(), SpriteEffects.None, 0f);
            }
            foreach (ISpriteMetadata sprite in UISprites)
            {
                spriteBatch.Draw(sprite.GetImage(contentImages), sprite.GetPosition(), sprite.GetSpritesheetRectange(), Color.DarkGray, sprite.GetRotation(), sprite.GetOrigin(), sprite.GetScale(), SpriteEffects.None, 0f);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
