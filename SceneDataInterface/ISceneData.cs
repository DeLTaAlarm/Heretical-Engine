using EventsSystem.EventManage;
using HDataCore;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using HInfrastructure;

namespace SceneDataInterface
{
    public interface ISceneData
    {
        public List<Entity> Entities { get; }
        public Dictionary<string, (List<BaseComponent>, int)> ComponentsByType { get; }
        public Dictionary<string, SceneDataContainer> SceneDataStorage { get; }
        public EventsManager SceneEventManager { get; }
        public SpriteBatch SpriteBatch { get; }
        public List<Entity> EntitiesToDestroy { get; set; }
        public List<BaseComponent> ComponentsToDestroy { get; set; }
        public GraphicsDevice GraphicsDevice { get; }
        public ResourceManager ResourceManager { get; }
    }
}
