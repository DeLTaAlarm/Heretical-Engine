using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using HDataCore;
using HInfrastructure;

namespace HCore
{
    public class SceneManager
    {
        private readonly GameLoopCore gameCore;
        private readonly AssemblyLoader assemblyLoader;
        private readonly ResourceManager resourceManager;
        public readonly GraphicsDevice graphicsDevice;
        public GameScene currentScene;

        public Dictionary<Type, GlobalDataContainer> globalDataStorage = new Dictionary<Type, GlobalDataContainer>();

        public SceneManager(GameLoopCore _gameCore, AssemblyLoader _assemblyLoader, ResourceManager _resourceManager, GraphicsDevice _graphicsDevice)
        {
            gameCore = _gameCore;
            assemblyLoader = _assemblyLoader;
            resourceManager = _resourceManager;
            graphicsDevice = _graphicsDevice;
        }

        public void Initialize()
        {

        }

        public void LoadContent(ContentManager content, GraphicsDevice graphicsDevice, SpriteBatch spriteBatch)
        {
            currentScene = new GameScene(graphicsDevice, spriteBatch, gameCore, assemblyLoader, resourceManager);
            currentScene.Initialize();
        }

        public void Update(GameTime gameTime)
        {
            currentScene.Update(gameTime);
        }

        public void Draw(GameTime gameTime)
        {
            currentScene.Draw(gameTime);
        }
    }
}
