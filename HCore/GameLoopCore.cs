using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using HInfrastructure;

namespace HCore
{
    public class GameLoopCore : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private SceneManager sceneManager;
        private ResourceManager resourceManager;
        private AssemblyLoader assemblyLoader;
        //public GraphicsDeviceManager _graphics;
        //private SpriteBatch spriteBatch;
        //public GameWorld gameWorld;

        public GameLoopCore()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            graphics.HardwareModeSwitch = false;
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            graphics.ApplyChanges();

            assemblyLoader = new AssemblyLoader();

            assemblyLoader.LoadAssembly();
            assemblyLoader.LoadAssembly("StandartComponents");
            assemblyLoader.LoadAssembly("Input");
            assemblyLoader.LoadAssembly("HPhysics");
            assemblyLoader.LoadAssembly("HGraphics");
            assemblyLoader.LoadAssembly("Game");
            assemblyLoader.LoadAssembly("DebugTools");

            assemblyLoader.LoadSystemsFromAssemblies();
            //assemblyLoader.LoadAssembly();

            resourceManager = new ResourceManager(Content, GraphicsDevice);
            sceneManager = new SceneManager(this, assemblyLoader, resourceManager, GraphicsDevice);
            sceneManager.Initialize();


            base.Initialize();
        }

        protected override void LoadContent()
        {
            //spriteBatch = new SpriteBatch(GraphicsDevice);
            //Texture2D t = new Texture2D(GraphicsDevice, 1, 1);
            //t.SetData(new[] { Color.Red });
            //textureList.Add("DebugPixel", t);

            //foreach (string file in Directory.EnumerateFiles(Directory.GetParent(Directory.GetCurrentDirectory()).Parent + "/Resources/Sprites/", "*.png", SearchOption.AllDirectories)) //Автоматическая загрузка .png
            //{
            //    using (var fileStream = new FileStream(file, FileMode.Open))
            //    {
            //        Texture2D t_texture = Texture2D.FromStream(GraphicsDevice, fileStream);
            //        FileInfo t_file = new FileInfo(file);
            //        textureList.Add(t_file.Name.Split(".")[0], t_texture);
            //    }
            //}
            //fonts.Add("FRM325", Content.Load<SpriteFont>("FRM325x8"));
            //    gameWorld = CreateWorld();

            spriteBatch = new SpriteBatch(GraphicsDevice);
            sceneManager.LoadContent(Content, GraphicsDevice, spriteBatch);
        }

        protected override void Update(GameTime gameTime)
        {
            sceneManager.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) 
        {
            GraphicsDevice.Clear(Color.Black);
            sceneManager.Draw(gameTime);
            //GraphicsDevice.Clear(Color.CornflowerBlue);

            //gameWorld.Draw(gameTime, GraphicsDevice, spriteBatch);
            //spriteBatch.Begin();
            //spriteBatch.DrawString(fonts["FRM325"], "FPS: " + Convert.ToInt32(1000 / (float)gameTime.ElapsedGameTime.Milliseconds), new Vector2(_graphics.PreferredBackBufferWidth - 100, _graphics.PreferredBackBufferHeight - 20), Color.Black);
            //spriteBatch.End();
            base.Draw(gameTime);
        }

        /*protected GameWorld CreateWorld()
        {
            return new GameWorld(this, spriteBatch);
        }

        public void SwitchFullScreenMode()
        {
            if (_graphics.IsFullScreen)
            {
                _graphics.PreferredBackBufferWidth = 1280;
                _graphics.PreferredBackBufferHeight = 720;
            }
            else
            {
                _graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
                _graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            }
            _graphics.IsFullScreen = !_graphics.IsFullScreen;
            _graphics.ApplyChanges();
            gameWorld.AdaptiveScreenScale();
        }*/


    }
}