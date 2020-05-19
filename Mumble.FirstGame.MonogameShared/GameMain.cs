#region Using Statements
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Mumble.FirstGame.Client;
using Mumble.FirstGame.Core.Action;
using Mumble.FirstGame.Core.Action.Fire;
using Mumble.FirstGame.Core.Action.Movement;
using Mumble.FirstGame.Core.ActionResult;
using Mumble.FirstGame.Core.Entity;
using Mumble.FirstGame.Core.Entity.Enemy;
using Mumble.FirstGame.Core.Entity.Player;
using Mumble.FirstGame.Core.Entity.Projectile;
using Mumble.FirstGame.MonogameShared.Utils;
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
        ClientType clientType = ClientType.Online;
        IGameClient client;
        Dictionary<IEntity, Vector2> positions = new Dictionary<IEntity, Vector2>();
        Player player;
        int scaling = 5;
        ContentImages contentImages;
        MovementKeyHandler keyHandler;
        MouseHandler mouseHandler;
        public GameMain()
        {   
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.IsFullScreen = false;
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

            player = new Player(3, 10);
            //solo
            if (clientType == ClientType.Solo)
            {
                client = new SoloGameClient();
            }
            else if (clientType == ClientType.Online)
            {
                IPEndPoint endpoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 27000);
                client = new OnlineGameClient(endpoint);
            }
            client.Init(player);
            
            positions.Add(player, new Vector2(0, 0));
            // TODO: Add your initialization logic here
            base.Initialize();
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
            List<IAction> actions = new List<IAction>();
            actions.AddIfNotNull(keyHandler.HandleKeyPress(player));
            actions.AddIfNotNull(mouseHandler.HandleMouseClick(player,positions[player]));
            actions.AddRange(client.Update(actions,gameTime.ElapsedGameTime));
            if (actions.Count > 0)
            {
                foreach(IFireWeaponAction action in actions.Where(x => x is IFireWeaponAction))
                {
                    EntitiesCreatedActionResult entityCreatedResult = (EntitiesCreatedActionResult)action.Result;
                    if (action.Result is EntitiesCreatedActionResult)
                    {
                        EntitiesCreatedActionResult result = (EntitiesCreatedActionResult)action.Result;
                        foreach(IEntity entity in result.Entities)
                        {
                            positions[entity] = new Vector2(
                                (entity.PositionComponent.X * 2 * scaling)+16,
                                (entity.PositionComponent.Y * 2 * scaling)+16
                            );
                        }
                    }
                }
                foreach (IMoveAction action in actions.Where(x => x is IMoveAction))
                {

                    MoveActionResult moveResult = (MoveActionResult)action.Result;
                    if (moveResult.OutOfBounds)
                    {
                        if (action.Entity != player)
                        {
                            positions.Remove(action.Entity);
                        }
 
                    }
                    else
                    {
                        if (action.Entity != player)
                        {
                            positions[action.Entity] = new Vector2(
                                moveResult.XPos * 2 * scaling+16,
                                moveResult.YPos * 2 * scaling+16
                            );
                        }
                        else
                        {
                            positions[action.Entity] = new Vector2(
                                moveResult.XPos * 2 * scaling,
                                moveResult.YPos * 2 * scaling
                            );
                        }
                        
                    }
                       
                }
            }
            //DebugUtils.PrintActions(actions);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            
            foreach (IEntity entity in positions.Keys)
            {
                if (entity == player)
                {
                    spriteBatch.Draw(contentImages.ImgTheDude, positions[entity], null, Color.DarkGray, 0f, Vector2.Zero, new Vector2(2, 2), SpriteEffects.None, 0f);
                }
                if (entity is SimpleBullet)
                {
                    SimpleBullet bulletEntity = (SimpleBullet)entity;
                    //origin = <width/2,height/2>
                    spriteBatch.Draw(contentImages.Bullet, positions[bulletEntity], null, Color.DarkGray, bulletEntity.VelocityComponent.Direction.Radians, new Vector2(8,8), new Vector2(1, 1), SpriteEffects.None, 0f);
                }
            }
            spriteBatch.End();
            //TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
