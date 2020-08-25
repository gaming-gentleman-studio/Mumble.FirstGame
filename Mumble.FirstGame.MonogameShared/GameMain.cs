#region Using Statements
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Mumble.FirstGame.Client;
using Mumble.FirstGame.Client.Online;
using Mumble.FirstGame.Core.Action;
using Mumble.FirstGame.Core.Action.Meta;
using Mumble.FirstGame.Core.ActionResult;
using Mumble.FirstGame.Core.ActionResult.Menu;
using Mumble.FirstGame.Core.ActionResult.Meta;
using Mumble.FirstGame.Core.Background;
using Mumble.FirstGame.Core.Entity;
using Mumble.FirstGame.Core.Entity.Player;
using Mumble.FirstGame.Core.Scene;
using Mumble.FirstGame.Core.Scene.Battle;
using Mumble.FirstGame.Core.Scene.EntityContainer;
using Mumble.FirstGame.Core.Scene.Menu;
using Mumble.FirstGame.MonogameShared.DisplayHandlers;
using Mumble.FirstGame.MonogameShared.DisplayHandlers.Battle;
using Mumble.FirstGame.MonogameShared.DisplayHandlers.Menu;
using Mumble.FirstGame.MonogameShared.InputHandlers;
using Mumble.FirstGame.MonogameShared.InputHandlers.Menu;
using Mumble.FirstGame.MonogameShared.ResultHandler;
using Mumble.FirstGame.MonogameShared.ResultHandler.Menu;
using Mumble.FirstGame.MonogameShared.Settings;
using Mumble.FirstGame.MonogameShared.SpriteMetadata;
using Mumble.FirstGame.MonogameShared.SpriteMetadata.Background;
using Mumble.FirstGame.MonogameShared.Utils;
using Mumble.FirstGame.Serialization.Protobuf.Factory;
using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace Mumble.FirstGame.MonogameShared
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class GameMain : Game
    {
        GraphicsDeviceManager graphics;
        IGameClient client;

        ContentImages contentImages;
        ContentFonts contentFonts;

        GameServiceContainer provider;
        float windowScale;
        IGameSettings settings;

        IResultHandler resultHandler;
        IInputHandler inputHandler;
        IDisplayHandler displayHandler;

        IScene scene;
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
            scene = client.CurrentScene;
        }
        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            SetWindowScale();

            client.Register();
            base.Initialize();
        }
        private void SetWindowScale()
        {
            windowScale = 1f;
            scalingUtils = new ScalingUtils(settings, windowScale);
        }
        private void InitializeMainMenuScene(List<IActionResult> results)
        {
            int width = graphics.GraphicsDevice.Viewport.Width;
            int height = graphics.GraphicsDevice.Viewport.Height;

            List<MenuItemMetadata> menuItems = new List<MenuItemMetadata>();

            int space = 100;
            int i = 0;
            foreach (GetMenuOptionsActionResult result in results.Where(x => x is GetMenuOptionsActionResult))
            {
                foreach(MenuOption option in result.Options)
                {
                    menuItems.Add(new MenuItemMetadata(option, new Vector2(width / 2, (height / 2)+(space*i)), contentFonts.Arial20));
                    i++;
                }
            }

            displayHandler = new MainMenuDisplayHandler(graphics, contentImages, menuItems,InitializeUISprites());
            resultHandler = new MainMenuResultHandler();
            inputHandler = new MenuInputHandler(menuItems);
        }
        
        private void InitializeBattleScene(List<IActionResult> results)
        {
            //TODO - this is a hacky way to get the player entity
            
            EntitiesCreatedActionResult createdResult = (EntitiesCreatedActionResult)results.Where(x => x is EntitiesCreatedActionResult).FirstOrDefault();
            Player player = (Player)createdResult.Entities.Where(x => x.OwnerIdentifier.Equals(client.Owner)).FirstOrDefault();
            Dictionary<IEntity, AbstractSpriteMetadata> entitySprites = new Dictionary<IEntity, AbstractSpriteMetadata>();
            entitySprites[player] = SpriteMetadataFactory.CreateSpriteMetadata(player);
            resultHandler = new BattleResultHandler(player, entitySprites);
            inputHandler = new BattleInputHandler(player, scalingUtils, entitySprites);

            displayHandler = new BattleDisplayHandler(graphics, contentImages,scalingUtils, entitySprites, InitializeUISprites(), InitializeBackgroundSprites());
            ApplyResults(results);
        }
        private List<AbstractSpriteMetadata> InitializeUISprites()
        {
            return new List<AbstractSpriteMetadata>() { new CursorMetadata() };
        }
        private List<AbstractSpriteMetadata> InitializeBackgroundSprites()
        {
            List<AbstractSpriteMetadata> backgroundSprites = new List<AbstractSpriteMetadata>();
            HashSet<IBackground> backgrounds = ((BattleScene)scene).Boundary.Backgrounds;
            foreach(IBackground background in backgrounds)
            {
                if (background is Wall)
                {
                    backgroundSprites.Add(new WallSpriteMetadata((Wall)background));
                }
                else if (background is Floor)
                {
                    backgroundSprites.Add(new FloorSpriteMetadata(new Vector2(background.Position.X, background.Position.Y), background.Scale));
                }
                
            }
            return backgroundSprites;
        }
        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            contentImages = new ContentImages();
            contentImages.LoadContent(Content, graphics.GraphicsDevice);
            contentFonts = new ContentFonts();
            contentFonts.LoadContent(Content);


            //There are here bc we have a dependency on contentImages
            List<IActionResult> results = client.Init();
            ApplyResults(results);

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


            actions.AddRange(inputHandler.HandleInput());

            results = client.Update(actions);
            ApplyResults(results);
            //DebugUtils.PrintActions(actions);
            base.Update(gameTime);
        }
        private void ApplyResults(List<IActionResult> results)
        {
            if (results.Count > 0)
            {
                foreach (GameExitedActionResult result in results.Where(x => x is GameExitedActionResult))
                {
                    Exit();
                }
                foreach (SceneLoadedActionResult result in results.Where(x => x is SceneLoadedActionResult))
                {
                    if (results.Count > 1)
                    {
                        throw new Exception("Load Scene actions must be standalone - something went wrong with core");
                    }
                    //scene instance has changed
                    client.CheckForSceneUpdate();
                    scene = client.CurrentScene;
                    EnterSceneAction enterAction = new EnterSceneAction();
                    ApplyResults(client.Update(new List<IAction>()
                    {
                        enterAction
                    }));

                }
                foreach (SceneEnteredActionResult result in results.Where(x => x is SceneEnteredActionResult))
                {

                    if (scene is BattleScene)
                    {
                        InitializeBattleScene(results.Where(x => x != result).ToList());
                    }
                    else if (scene is MainMenuScene)
                    {
                        InitializeMainMenuScene(results.Where(x => x != result).ToList());
                    }
                    return;
                }
                resultHandler.ApplyResults(results);

            }
        }
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            Vector2 mousePosition = scalingUtils.DescalePosition(Mouse.GetState().Position.ToVector2());
            displayHandler.Draw(mousePosition);
            base.Draw(gameTime);
        }
    }
}
