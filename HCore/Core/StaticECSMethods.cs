using HPhysics.ECS.Collision;
//using HCore.ECS.Control;
using HPhysics.ECS.Movement;
//using HECSCore.ECS.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using HPhysics.ECS.Collision.CollisionDetection;
//using HCore.EventManage;
using HPhysics.ECS.Collision.CollisionProcessing;
using GraphicsData;
//using HCore.ECS.Camera;
using HDataCore;
using SceneDataInterface;

namespace HCore.Core
{
    static public class GameDataAccess
    {
        //static public SceneManager SceneManager { private get; set; }
        //static public ResourceManager ResourceManager { private get; set; }
        /*static public GameScene GameScene { get { return SceneManager.currentScene; } }*/

        //static public void SpawnEntity(List<BaseComponent> components, ISceneData scene)
        //{
        //    Entity tempEntity = new Entity();
        //    tempEntity.components = new Dictionary<Type, BaseComponent>();//new BaseComponent[gameWorld.componentsByType.Count];
        //    AttachComponentsToEntity(tempEntity, components);
        //    scene.Entities.Add(tempEntity);
        //    tempEntity.Id = (uint)scene.Entities.Count - 1; //Надо бы подправить нумерацию. Чтобы путаницы не было в номерах

        //    foreach (BaseComponent component in components)
        //    {
        //        scene.ComponentsByType[component.GetType()].Item1.Add(component);
        //    } 
        //}

        /*static public void AddPlayer(Vector2 location)
        {
            SpawnEntity(new List<BaseComponent>() {
                            new LocationComponent(location),
                            new MovementComponent(new Vector2(0, 0), 1.5f, 1f),
                            new SpriteComponent(new Sprite(new SpriteAtlas(ResourceManager.GetTexture("playerCharacterDown"), 1, 6), 0), new Vector2(0, 0), 1f),
                            new SpriteAnimationComponent(new List<Sprite>(){ new Sprite(new SpriteAtlas(ResourceManager.GetTexture("playerCharacterDown"), 1, 6), 0), new Sprite(new SpriteAtlas(ResourceManager.GetTexture("playerCharacterDown"), 1, 6), 1)}, 6, true),
                            new AnimationSelectorComponent(new Dictionary<string, SpriteAnimation>() { {"MoveDown", new SpriteAnimation(new List<Sprite>() { new Sprite(new SpriteAtlas(ResourceManager.GetTexture("playerCharacterDown"), 1, 6), 0), new Sprite(new SpriteAtlas(ResourceManager.GetTexture("playerCharacterDown"), 1, 6), 1) }, 6, false) },
                                                                                                       {"MoveUp", new SpriteAnimation(new List<Sprite>() { new Sprite(new SpriteAtlas(ResourceManager.GetTexture("playerCharacterUp"), 1, 6), 0), new Sprite(new SpriteAtlas(ResourceManager.GetTexture("playerCharacterUp"), 1, 6), 1) }, 6, false) } }),
                            new CameraComponent(new Camera(SceneManager.graphicsDevice.Viewport, SceneManager.graphicsDevice, 0, 2f), true, new List<PostprocessingEffect>(){ }),
                            //new SpriteComponent(new Dictionary<string, SpriteData>(){
                            //                   { "WalkRight", new SpriteData(gameWorld.gameInstance.spriteList["playerCharacterRight"], 1, 6, 6, 0)},
                            //                   { "WalkDown", new SpriteData(gameWorld.gameInstance.spriteList["playerCharacterDown"], 1, 6, 6, 0)},
                            //                   { "WalkLeft", new SpriteData(gameWorld.gameInstance.spriteList["playerCharacterLeft"], 1, 6, 6, 0)},
                            //                   { "WalkUp", new SpriteData(gameWorld.gameInstance.spriteList["playerCharacterUp"], 1, 6, 6, 0)}},
                            //    new Vector2(0, 0),
                            //    1f),
                            //new SpriteAnimationComponent(),
                            new CollisionComponent(new Rectangle(0, 0, 16, 16), false, CollisionInteractionType.SoftBlock, new Vector2(0, -16)),
                            new ControlComponent()});*/

        /*SpawnEntity(new List<BaseComponent>() {
                        new LocationComponent(location - new Vector2(64)),
                        new MovementComponent(new Vector2(0, 0), 1.5f, 1f),
                        new SpriteComponent(new Sprite(new SpriteAtlas(gameWorld.gameInstance.textureList["playerCharacterDown"], 1, 6), 0), new Vector2(0, 0), 1f),
                        new SpriteAnimationComponent(new List<Sprite>(){ new Sprite(new SpriteAtlas(gameWorld.gameInstance.textureList["playerCharacterDown"], 1, 6), 0), new Sprite(new SpriteAtlas(gameWorld.gameInstance.textureList["playerCharacterDown"], 1, 6), 1)}, 6, true),
                        new AnimationSelectorComponent(new Dictionary<string, SpriteAnimation>() { {"MoveDown", new SpriteAnimation(new List<Sprite>() { new Sprite(new SpriteAtlas(gameWorld.gameInstance.textureList["playerCharacterDown"], 1, 6), 0), new Sprite(new SpriteAtlas(gameWorld.gameInstance.textureList["playerCharacterDown"], 1, 6), 1) }, 6, false) },
                                                                                                   {"MoveUp", new SpriteAnimation(new List<Sprite>() { new Sprite(new SpriteAtlas(gameWorld.gameInstance.textureList["playerCharacterUp"], 1, 6), 0), new Sprite(new SpriteAtlas(gameWorld.gameInstance.textureList["playerCharacterUp"], 1, 6), 1) }, 6, false) } }),
                        new CameraComponent(new Camera(gameWorld.gameInstance.GraphicsDevice.Viewport, gameWorld.gameInstance.GraphicsDevice, 0, 2f), false, null),
                        new CollisionComponent(new Rectangle(0, 0, 16, 16), false, CollisionInteractionType.SoftBlock, new Vector2(0, -16)),
                        new ControlComponent()});*/

        /*SpawnEntity(new List<BaseComponent>() {
                        new LocationComponent(location - new Vector2(-64)),
                        new MovementComponent(new Vector2(0, 0), 1.5f, 1f),
                        new SpriteComponent(new Sprite(new SpriteAtlas(gameWorld.gameInstance.textureList["playerCharacterDown"], 1, 6), 0), new Vector2(0, 0), 1f),
                        new SpriteAnimationComponent(new List<Sprite>(){ new Sprite(new SpriteAtlas(gameWorld.gameInstance.textureList["playerCharacterDown"], 1, 6), 0), new Sprite(new SpriteAtlas(gameWorld.gameInstance.textureList["playerCharacterDown"], 1, 6), 1)}, 6, true),
                        new AnimationSelectorComponent(new Dictionary<string, SpriteAnimation>() { {"MoveDown", new SpriteAnimation(new List<Sprite>() { new Sprite(new SpriteAtlas(gameWorld.gameInstance.textureList["playerCharacterDown"], 1, 6), 0), new Sprite(new SpriteAtlas(gameWorld.gameInstance.textureList["playerCharacterDown"], 1, 6), 1) }, 6, false) },
                                                                                                   {"MoveUp", new SpriteAnimation(new List<Sprite>() { new Sprite(new SpriteAtlas(gameWorld.gameInstance.textureList["playerCharacterUp"], 1, 6), 0), new Sprite(new SpriteAtlas(gameWorld.gameInstance.textureList["playerCharacterUp"], 1, 6), 1) }, 6, false) } }),
                        new CameraComponent(new Camera(gameWorld.gameInstance.GraphicsDevice.Viewport, gameWorld.gameInstance.GraphicsDevice, 0, 2f), false, null),
                        new CollisionComponent(new Rectangle(0, 0, 16, 16), false, CollisionInteractionType.SoftBlock, new Vector2(0, -16)),
                        new ControlComponent()});

        SpawnEntity(new List<BaseComponent>() {
                        new LocationComponent(location - new Vector2(0, 64)),
                        new MovementComponent(new Vector2(0, 0), 1.5f, 1f),
                        new SpriteComponent(new Sprite(new SpriteAtlas(gameWorld.gameInstance.textureList["playerCharacterDown"], 1, 6), 0), new Vector2(0, 0), 1f),
                        new SpriteAnimationComponent(new List<Sprite>(){ new Sprite(new SpriteAtlas(gameWorld.gameInstance.textureList["playerCharacterDown"], 1, 6), 0), new Sprite(new SpriteAtlas(gameWorld.gameInstance.textureList["playerCharacterDown"], 1, 6), 1)}, 6, true),
                        new AnimationSelectorComponent(new Dictionary<string, SpriteAnimation>() { {"MoveDown", new SpriteAnimation(new List<Sprite>() { new Sprite(new SpriteAtlas(gameWorld.gameInstance.textureList["playerCharacterDown"], 1, 6), 0), new Sprite(new SpriteAtlas(gameWorld.gameInstance.textureList["playerCharacterDown"], 1, 6), 1) }, 6, false) },
                                                                                                   {"MoveUp", new SpriteAnimation(new List<Sprite>() { new Sprite(new SpriteAtlas(gameWorld.gameInstance.textureList["playerCharacterUp"], 1, 6), 0), new Sprite(new SpriteAtlas(gameWorld.gameInstance.textureList["playerCharacterUp"], 1, 6), 1) }, 6, false) } }),
                        new CameraComponent(new Camera(gameWorld.gameInstance.GraphicsDevice.Viewport, gameWorld.gameInstance.GraphicsDevice, 0, 2f), false, null),
                        new CollisionComponent(new Rectangle(0, 0, 16, 16), false, CollisionInteractionType.SoftBlock, new Vector2(0, -16)),
                        new ControlComponent()});*/
        //}

        ///*//static public List<T> GetAllComponentsList<T>(ISceneData scene) where T : BaseComponent
        ////{
        ////    return scene.ComponentsByType[typeof(T)].Item1.Cast<T>().ToList();
        ////}

        ////static public T[] GetAllComponents<T>(ISceneData scene) where T : BaseComponent
        ////{
        ////    return scene.ComponentsByType[typeof(T)].Item1.Cast<T>().ToArray();
        ////}

        ////static public void DestroyComponent(BaseComponent component, ISceneData scene)
        ////{
        ////    if (component != null)
        ////    {
        ////        //component.entityOfComponent.components[GetComponentIDbyObject(component)] = null;
        ////        component.entityOfComponent.components.Remove(component.GetType());
        ////        scene.ComponentsByType[component.GetType()].Item1[scene.ComponentsByType[component.GetType()].Item1.FindIndex(f => f == component)] = scene.ComponentsByType[component.GetType()].Item1.Last();

        ////        scene.ComponentsByType[component.GetType()].Item1.RemoveAt(scene.ComponentsByType[component.GetType()].Item1.Count - 1);

        ////        //gameWorld.componentsByType[component.GetType()].Item1.Remove(component);
        ////    }
        ////}

        ////static public void DestroyEntity(Entity entity, ISceneData scene)
        ////{
        ////    if (entity != null)
        ////    {
        ////        //for (int i = 0; i < entity.components.Length; i++) 
        ////        //{
        ////        //    if (entity.components[i] != null) gameWorld.componentsByType[entity.components[i].GetType()].Item1.Remove(entity.components[i]);
        ////        //}

        ////        foreach (BaseComponent bc in entity.components.Values)
        ////        {
        ////            scene.ComponentsByType[bc.GetType()].Item1.Remove(bc);
        ////        }
        ////        GetSceneByInterface(scene).entitiesToDestroy.Add(entity);
        ////    }
        ////}
        ////static public void DestroyEntities(ISceneData scene)
        ////{

        ////    if (GetSceneByInterface(scene).entitiesToDestroy.Count < 1) return;

        ////    scene.Entities.RemoveAll(e => GetSceneByInterface(scene).entitiesToDestroy.Contains(e));
        ////    GetSceneByInterface(scene).entitiesToDestroy.Clear();
        ////}*/
        /*static public void DestroyEntities(List<Entity> entities)
        {
            if (entities.Count < 1) return;

            Dictionary<Type, List<BaseComponent>> componentsByType = new Dictionary<Type, List<BaseComponent>>();

            foreach (Entity entity in entities)
            {
                foreach (BaseComponent component in entity.components)
                {
                    if (component is not null) 
                        if (!componentsByType.ContainsKey(component.GetType())) componentsByType.Add(component.GetType(), new List<BaseComponent>() { component });
                        else componentsByType[component.GetType()].Add(component);
                }
            }
            foreach (KeyValuePair<Type, List<BaseComponent>> t in componentsByType)
            {
                gameWorld.componentsByType[t.Key].Item1.RemoveAll(с => t.Value.Contains(с));
            }
            gameWorld.entities.RemoveAll(e => entities.Contains(e));
            gameWorld.serverManager.entitiesToDestroy.Clear();
        }*/

        /*static public void DestroyComponents(List<BaseComponent> components)
        {
            if (components.Count < 1) return;

            Dictionary<Type, List<BaseComponent>> componentsByType = new Dictionary<Type, List<BaseComponent>>();

            foreach (BaseComponent c in components)
            {
                c.entityOfComponent.components[GetComponentIDbyObject(c)] = null;
                componentsByType[c.GetType()].Add(c);
            }

            foreach (KeyValuePair<Type, List<BaseComponent>> t in componentsByType)
            {
                gameWorld.componentsByType[t.Key].Item1.RemoveAll(с => t.Value.Contains(с));
            }
            gameWorld.serverManager.componentsToDestroy.Clear();
        }*/

        //static public T GetSceneDataContainer<T>(GameScene scene) where T : SceneDataContainer
        //{
        //    if (scene.SceneDataStorage.ContainsKey(typeof(T)))
        //    {
        //        return (T)scene.SceneDataStorage[typeof(T)];
        //    }
        //    else return null;
        //}

        static public T GetGlobalDataContainer<T>(SceneManager sceneManager) where T : GlobalDataContainer
        {
            if (sceneManager.globalDataStorage.ContainsKey(typeof(T)))
            {
                return (T)sceneManager.globalDataStorage[typeof(T)];
            }
            else return null;
        }
    }
}
