#region Using Statements
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Mumble.FirstGame.Core.Action;
using Mumble.FirstGame.Core.Entity;
using Mumble.FirstGame.Core.Entity.Enemy;
using Mumble.FirstGame.Core.Entity.Player;
using Mumble.FirstGame.Core.Scene;
using Mumble.FirstGame.Core.Scene.Battle;
using Mumble.FirstGame.Core.TagArguments;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        IScene scene;
        Player player;
        public GameMain()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.IsFullScreen = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            graphics.ToggleFullScreen();
            player = new Player(3, 10);
            Slime slime = new Slime(2, 4);
            SceneBoundary boundary = new SceneBoundary(30, 30);
            scene = new BattleScene(
                new List<IMoveableCombatEntity>() { player },
                new List<ICombatAIEntity>() { slime },
                boundary);
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
            MovementKeyHandler keyHandler = new MovementKeyHandler();
            IAction action = keyHandler.HandleKeyPress(player);
            scene.Update(action);
            if (action != null)
            {
                Debug.WriteLine("");
                Debug.Write(action.Result.Tag.Id.ToString());

                ITagArguments args = action.Result.Tag.Arguments;
                foreach (FieldInfo field in args.GetType().GetFields())
                {
                    Debug.Write(";"+ field.Name+":"+ field.GetValue(args).ToString());
                }
            }
            
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
