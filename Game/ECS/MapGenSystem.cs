using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using GraphicsData;
using HGraphics.ECS.Camera;
using HPhysics.ECS.Collision.CollisionProcessing;
using HPhysics.ECS.Movement;
using HGraphics.ECS.Rendering;
using HDataCore;
using HSystemsCore;
using static SceneDataInterface.ECSManager;
using System;
using System.Collections.Generic;
using StandartComponents.ECS;
using HInfrastructure;
using HPhysics.ECS.Collision;
using Input.ECS.Control;
using Physics;

namespace HCore.ECS
{
    public class MapGenSystem : BaseProcessingSystem
    {
        public override void Initialize()
        {
            base.Initialize();

            SpawnEntity(new List<BaseComponent>()
            {
                new LocationComponent(new Vector2(1, 0)),
                new MovementComponent(new Vector2(0, 0), 1.5f, 1f),

                new SpriteComponent(
                    new Sprite(new SpriteAtlas(gameScene.ResourceManager.GetTexture("playerCharacterDown"), 1, 6), 0),
                    new Vector2(0, 0),
                    1f
                ),

                new SpriteAnimationComponent(new List<Sprite>()
                {
                    new Sprite(new SpriteAtlas(gameScene.ResourceManager.GetTexture("playerCharacterDown"), 1, 6), 0),
                    new Sprite(new SpriteAtlas(gameScene.ResourceManager.GetTexture("playerCharacterDown"), 1, 6), 1)
                }, 6, true),

                new AnimationSelectorComponent(new Dictionary<string, SpriteAnimation>()
                {
                    {
                        "MoveDown", new SpriteAnimation(new List<Sprite>()
                        {
                            new Sprite(new SpriteAtlas(gameScene.ResourceManager.GetTexture("playerCharacterDown"), 1, 6), 0),
                            new Sprite(new SpriteAtlas(gameScene.ResourceManager.GetTexture("playerCharacterDown"), 1, 6), 1)
                        }, 6, false)
                    },
                    {
                        "MoveUp", new SpriteAnimation(new List<Sprite>()
                        {
                            new Sprite(new SpriteAtlas(gameScene.ResourceManager.GetTexture("playerCharacterUp"), 1, 6), 0),
                            new Sprite(new SpriteAtlas(gameScene.ResourceManager.GetTexture("playerCharacterUp"), 1, 6), 1)
                        }, 6, false)
                    }
                }),

                new CameraComponent(
                    new Camera(
                        gameScene.GraphicsDevice.Viewport,
                        gameScene.GraphicsDevice,
                        0,
                        2f
                    ),
                    true,
                    new List<PostprocessingEffect>()
                ),
                //new CollisionComponent(new AABBCollision(0, 0, -8, 8, -8, 8), CollisionInteractionType.BlockAndOverlap),
                new CollisionComponent(new CircleCollision(0, 0, 16), CollisionInteractionType.BlockAndOverlap),
                new ControlComponent()
            }, gameScene);

            int seed = 1;
            Random rnd = new Random(seed);
            for (int i = -111; i < 111; i++)
            {
                for (int j = -64; j < 63; j++)
                    SpawnEntity(new List<BaseComponent>() { new LocationComponent(new Vector2(i * 16, j * 16)), new SpriteComponent(new Sprite(new SpriteAtlas(gameScene.ResourceManager.GetTexture("tilesAtlas1"), 3, 9), 0), new Vector2(0, 0), 0f) }, gameScene);
            }

            for (int j = -19; j < 99; j++)
                SpawnEntity(new List<BaseComponent>() { new LocationComponent(new Vector2(-130, j * 16)), new SpriteComponent(new Sprite(new SpriteAtlas(gameScene.ResourceManager.GetTexture("tilesAtlas1"), 3, 9), 9), new Vector2(0, 0), 1f),
                new CollisionComponent(new AABBCollision(0, 0, -8, 8, -8, 8), CollisionInteractionType.BlockAndOverlap)}, gameScene);
                //new CollisionComponent(new CircleCollision(0, 0, 16), CollisionInteractionType.BlockAndOverlap)}, gameScene);
        }
    }
}