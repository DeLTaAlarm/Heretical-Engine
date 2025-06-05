//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;
//using HCore.ECS;
//using HCore.ECS.Collision;
//using HCore.ECS.Collision.CollisionDetection;
//using HCore.ECS.Movement;
//using HCore.ECS.Rendering;
//using HCore.Core.Graphics;

//using static HCore.Core.StaticECSMethods;

//namespace HCore;

//public class MapGenerator
//{
//    private GameScene gameWorldInstance;
//    public MapGenerator(GameScene _gameWorldInstance) 
//    {
//        gameWorldInstance = _gameWorldInstance;
//        int seed = 1;
//        Random rnd = new Random(seed);
//        /*for (int i = -111; i < 111; i++)
//        {
//            for (int j = -64; j < 63; j++)
//                SpawnEntity(new List<BaseComponent>() { new LocationComponent(new Vector2(i * 16, j * 16)), new SpriteComponent(new Sprite(new SpriteAtlas(Core.StaticECSMethods.GameScene.gameInstance.textureList["tilesAtlas1"], 3, 9), 0), new Vector2(0, 0), 0f) });
//        }*/

//        /*for (int i = -64; i < 63; i++)
//        {
//            for (int j = -64; j < 63; j++)
//                SpawnEntity(new List<BaseComponent>() { new LocationComponent(new Vector2(i * 16, j * 16)), new SpriteComponent(new Sprite(new SpriteAtlas(gameWorld.gameInstance.textureList["tilesAtlas1"], 3, 9), 0), new Vector2(0, 0), 0f), *//*new CollisionComponent(new Circle(8))*//*});
//        }*/
//        //for (int i = 0; i < 2000; i++) for (int j = 0; j < 1; j++)
//        //    {
//        //        //SpawnEntity(new List<BaseComponent>() { new LocationComponent(new Vector2(i * 64, -64 - j * 64)), new SpriteComponent(new Dictionary<string, SpriteData>() { { "tileAtlas1", new SpriteData(gameWorldInstance.gameInstance.spriteList["tilesAtlas1"], 3, 9, 0, 1) } }, new Vector2(0, 0), 0f), new CollisionComponent(new Circle(20), false, CollisionInteractionType.SoftBlock, new Vector2(0, 0)), new MovementComponent(new Vector2((float)new Random().NextDouble() - 0.5f, (float)new Random().NextDouble() - 0.5f), _mass: 10) });
//        //        SpawnEntity(new List<BaseComponent>() { new LocationComponent(new Vector2(rnd.Next() % (200 * 16) - 100 * 16, rnd.Next() % (200 * 16) - 100 * 16)), new SpriteComponent(new Dictionary<string, SpriteData>() { { "tileAtlas1", new SpriteData(gameWorldInstance.gameInstance.spriteList["tilesAtlas1"], 3, 9, 0, 5) } }, new Vector2(0, 0), 0f), new CollisionComponent(new Circle(20), false, CollisionInteractionType.SoftBlock, new Vector2(0, 0)), new MovementComponent(new Vector2(((float)rnd.NextDouble() * 5 - 2.5f) / 7*0, ((float)rnd.NextDouble() * 5 - 2.5f) / 7)*0, _mass: 1) });
//        //    }

//        //for (int i = 0; i < 0; i++) for (int j = 0; j < 1; j++)
//        //    {
//        //        //SpawnEntity(new List<BaseComponent>() { new LocationComponent(new Vector2(rnd.Next() % (200 * 16) - 100 * 16, rnd.Next() % (200 * 16) - 100 * 16)), new SpriteComponent(new Dictionary<string, SpriteData>() { { "tileAtlas1", new SpriteData(gameWorldInstance.gameInstance.spriteList["tilesAtlas1"], 3, 9, 0, 5) } }, new Vector2(0, 0), 0f), new CollisionComponent(new Circle(20), false, CollisionInteractionType.SoftBlock, new Vector2(0, 0)), new MovementComponent(new Vector2((2) / 9999f, (2) / 9999f), _mass: 1) });
//        //        SpawnEntity(new List<BaseComponent>() { new LocationComponent(new Vector2(rnd.Next() % (200 * 16) - 100 * 16, rnd.Next() % (200 * 16) - 100 * 16)), new SpriteComponent(new Sprite(new SpriteAtlas(gameWorldInstance.gameInstance.textureList["tilesAtlas1"], 3, 9), 5), new Vector2(0, 0), 0f), new CollisionComponent(new Circle(20), false, CollisionInteractionType.SoftBlock, new Vector2(0, 0)), new MovementComponent(new Vector2((2) / 9999f, (2) / 9999f), _mass: 1) });
//        //    }
//    }
//}
