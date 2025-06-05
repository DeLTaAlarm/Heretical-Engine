using System;
using System.Collections.Generic;
using System.Linq;
using HDataCore;

namespace SceneDataInterface
{
    static public class ECSManager
    {
        static public void AttachComponentsToEntity(Entity entity, List<BaseComponent> components)
        {
            foreach (BaseComponent component in components)
            {
                AttachComponentToEntity(entity, component);
            }
        }

        static public void AttachComponentToEntity(Entity entity, BaseComponent component)
        {
            entity.components.Add(component.GetType(), component);
            component.entityOfComponent = entity;

            //GameScene.sceneEventManager.RaiseEvent(entity, new EventOnComponentAttachedArgs(component));
        }

        static public BaseComponent GetComponentOfEntityByType(Entity entity, Type type)
        {
            BaseComponent tempComponent = null;
            entity.components.TryGetValue(type, out tempComponent);
            return tempComponent;
        }

        static public T GetComponentOfEntity<T>(Entity entity) where T : BaseComponent
        {
            BaseComponent tempComponent = null;
            entity.components.TryGetValue(typeof(T), out tempComponent);
            return (T)tempComponent;
        }

        static public void SpawnEntity(List<BaseComponent> components, ISceneData scene)
        {
            Entity tempEntity = new Entity();
            tempEntity.components = new Dictionary<Type, BaseComponent>();//new BaseComponent[gameWorld.componentsByType.Count];
            AttachComponentsToEntity(tempEntity, components);
            scene.Entities.Add(tempEntity);
            tempEntity.Id = (uint)scene.Entities.Count - 1; //Надо бы подправить нумерацию. Чтобы путаницы не было в номерах

            foreach (BaseComponent component in components)
            {
                scene.ComponentsByType[component.GetType()].Item1.Add(component);
            }
        }

        static public List<T> GetAllComponentsList<T>(ISceneData scene) where T : BaseComponent
        {
            return scene.ComponentsByType[typeof(T)].Item1.Cast<T>().ToList();
        }

        static public T[] GetAllComponents<T>(ISceneData scene) where T : BaseComponent
        {
            return scene.ComponentsByType[typeof(T)].Item1.Cast<T>().ToArray();
        }

        static public void DestroyComponent(BaseComponent component, ISceneData scene)
        {
            if (component != null)
            {
                //component.entityOfComponent.components[GetComponentIDbyObject(component)] = null;
                component.entityOfComponent.components.Remove(component.GetType());
                scene.ComponentsByType[component.GetType()].Item1[scene.ComponentsByType[component.GetType()].Item1.FindIndex(f => f == component)] = scene.ComponentsByType[component.GetType()].Item1.Last();

                scene.ComponentsByType[component.GetType()].Item1.RemoveAt(scene.ComponentsByType[component.GetType()].Item1.Count - 1);

                //gameWorld.componentsByType[component.GetType()].Item1.Remove(component);
            }
        }

        static public void DestroyEntity(Entity entity, ISceneData scene)
        {
            if (entity != null)
            {
                //for (int i = 0; i < entity.components.Length; i++) 
                //{
                //    if (entity.components[i] != null) gameWorld.componentsByType[entity.components[i].GetType()].Item1.Remove(entity.components[i]);
                //}

                foreach (BaseComponent bc in entity.components.Values)
                {
                    scene.ComponentsByType[bc.GetType()].Item1.Remove(bc);
                }
                scene.EntitiesToDestroy.Add(entity);
            }
        }

        static public void DestroyEntities(ISceneData scene)
        {

            if (scene.EntitiesToDestroy.Count < 1) return;

            scene.Entities.RemoveAll(e => scene.EntitiesToDestroy.Contains(e));
            scene.EntitiesToDestroy.Clear();
        }

        static public T GetSceneDataContainer<T>(ISceneData scene) where T : SceneDataContainer
        {
            if (scene.SceneDataStorage.ContainsKey(typeof(T)))
            {
                return (T)scene.SceneDataStorage[typeof(T)];
            }
            else return null;
        }

        static public T CreateSceneDataContainer<T>(ISceneData scene) where T : SceneDataContainer, new()
        {
            if (!scene.SceneDataStorage.ContainsKey(typeof(T)))
            {
                scene.SceneDataStorage.Add(typeof(T), new T());
                return (T)scene.SceneDataStorage[typeof(T)];
            }
            else return null;
        }
    }
}
