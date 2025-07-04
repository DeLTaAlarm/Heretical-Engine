using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using EventsSystem.EventManage;
using HGraphics.ECS.Camera;
using HCore.Core;
using HSystemsCore;
using HDataCore;
using SceneDataInterface;

using HInfrastructure;
using static SceneDataInterface.ECSManager;

namespace HCore
{
    public class GameScene : ISceneData
    {
        private readonly GraphicsDevice graphicsDevice;
        private readonly SpriteBatch spriteBatch;
        
        public readonly GameLoopCore gameCore;

        private List<Entity> entities = new List<Entity>();
        private Dictionary<string, (List<BaseComponent>, int)> componentsByType = new Dictionary<string, (List<BaseComponent>, int)>();
        private Dictionary<string, SceneDataContainer> sceneDataStorage;

        public BaseProcessingSystem[] processingSystems;
        public Dictionary<SystemsRenderingOrderEnum, BaseRenderingSystem[]> renderingSystems;
        public EventsManager sceneEventManager;

        private readonly AssemblyLoader assemblyLoader;
        private readonly ResourceManager resourceManager;

        public GraphicsDevice GraphicsDevice { get { return graphicsDevice; } }
        public ResourceManager ResourceManager { get { return resourceManager; } }
        public List<Entity> Entities { get { return entities; } }
        public Dictionary<string, (List<BaseComponent>, int)> ComponentsByType { get { return componentsByType; } }
        public Dictionary<string, SceneDataContainer> SceneDataStorage { get { return sceneDataStorage; } }
        public EventsManager SceneEventManager {  get { return sceneEventManager; } }
        public SpriteBatch SpriteBatch { get { return spriteBatch; } }
        public List<Entity> EntitiesToDestroy { get; set; } = new List<Entity>();
        public List<BaseComponent> ComponentsToDestroy { get; set; } = new List<BaseComponent>();



        public GameScene(GraphicsDevice _graphicsDevice, SpriteBatch _spriteBatch, GameLoopCore _gameCore, AssemblyLoader _assemblyLoader, ResourceManager _resourceManager)
        {
            graphicsDevice = _graphicsDevice;
            spriteBatch = _spriteBatch;
            gameCore = _gameCore;
            assemblyLoader = _assemblyLoader;
            resourceManager = _resourceManager;
        }

        public void Initialize()
        {
            processingSystems = assemblyLoader.CreateProcessingSystems();
            renderingSystems = assemblyLoader.CreateRenderingSystems();
            componentsByType = assemblyLoader.GetComponentsArray();
            sceneDataStorage = assemblyLoader.GetSceneDataContainersArray();


            sceneEventManager = new EventsManager();

            for (int i = 0; i < processingSystems.Length; i++)
            {
                processingSystems[i].gameScene = this;
                processingSystems[i].Initialize();
            }

            foreach (SystemsRenderingOrderEnum orderType in renderingSystems.Keys)
                for (int i = 0; i < renderingSystems[orderType].Length; i++)
                {
                    renderingSystems[orderType][i].gameScene = this;
                    renderingSystems[orderType][i].Initialize();
                }

            //StaticECSMethods.AddPlayer(Vector2.Zero);

            //CreateSceneDataContainer<CurrentCameraSceneContainer>(this);
        }

        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < processingSystems.Length; i++)
            {
                processingSystems[i].Update(gameTime);
            }

            DestroyEntities(this as ISceneData);
        }

        public void Draw(GameTime gameTime)
        {
            CameraComponent[] cameraComponents = GetAllComponents<CameraComponent>(this as ISceneData);

            for (int i = 0; i < cameraComponents.Length; i++)
            {
                if (!cameraComponents[i].isActive) continue;

                graphicsDevice.Viewport = cameraComponents[i].camera.Viewport;



                cameraComponents[i].camera.RenderToTarget(graphicsDevice, spriteBatch, sB =>
                {
                    GetSceneDataContainer<CurrentCameraSceneContainer>(this).currentCamera = cameraComponents[i];

                    foreach (SystemsRenderingOrderEnum order in renderingSystems.Keys)
                    {
                        SpriteSortMode selectedMode = SpriteSortMode.Immediate;
                        bool useTransform = false;

                        if (order == SystemsRenderingOrderEnum.Rendering)
                        {
                            selectedMode = SpriteSortMode.FrontToBack;
                            useTransform = true;
                        }

                        spriteBatch.Begin(
                            sortMode: selectedMode,
                            samplerState: SamplerState.PointClamp,
                            transformMatrix: (useTransform) ? cameraComponents[i].camera.Transform : null,
                            blendState: BlendState.NonPremultiplied
                        );

                        foreach (var system in renderingSystems[order])
                        {
                            system.Draw(gameTime, graphicsDevice, sB);
                        }

                        //if (order == SystemsRenderingOrderEnum.Rendering) foreach (BaseProcessingSystem s in systems) if (s is CollisionPreparationSystem) (s as CollisionPreparationSystem).quadTree.Draw(spriteBatch, gameInstance.fonts["FRM325"]);

                        spriteBatch.End();
                    }

                });

                spriteBatch.Begin(
                    sortMode: SpriteSortMode.Immediate,
                    samplerState: SamplerState.PointClamp,
                    transformMatrix: null,
                    blendState: BlendState.NonPremultiplied
                );

                spriteBatch.Draw(
                    cameraComponents[i].camera.RenderTarget,
                    destinationRectangle: new Rectangle(
                        cameraComponents[i].camera.Viewport.X,
                        cameraComponents[i].camera.Viewport.Y,
                        cameraComponents[i].camera.Viewport.Width,
                        cameraComponents[i].camera.Viewport.Height
                    ),
                    Color.White
                );

                spriteBatch.End();

            }
        }

    }
}
