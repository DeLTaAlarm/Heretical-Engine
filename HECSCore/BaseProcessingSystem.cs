using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HDataCore;
using static EventsSystem.EventManage.EventsManager;
using EventsSystem.EventManage;
using SceneDataInterface;

namespace HSystemsCore
{
    public abstract class BaseProcessingSystem
    {
        public ISceneData gameScene;
        public virtual void Initialize()
        {

        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public void SubscribeEvent<TComponent, TEvent>(EntityEventHandler<TComponent, TEvent> eventHandler) where TComponent : BaseComponent where TEvent : EntityEventArgs
        {
            gameScene.SceneEventManager.SubscribeEvent(eventHandler);
        }

        public void UnsubscribeEvent<TComponent, TEvent>(EntityEventHandler<TComponent, TEvent> eventHandler) where TComponent : BaseComponent where TEvent : EntityEventArgs
        {
            gameScene.SceneEventManager.UnsubscribeEvent(eventHandler);
        }

        public void RaiseEvent<TEvent>(Entity entity, TEvent e) where TEvent : EntityEventArgs
        {
            gameScene.SceneEventManager.RaiseEvent(entity, e);
        }

        public void SubscribeEvent<TEvent>(LocalEventHandler<TEvent> eventHandler) where TEvent : EntityEventArgs
        {
            gameScene.SceneEventManager.SubscribeEvent(eventHandler);
        }

        public void UnsubscribeEvent<TEvent>(LocalEventHandler<TEvent> eventHandler) where TEvent : EntityEventArgs
        {
            gameScene.SceneEventManager.UnsubscribeEvent(eventHandler);
        }

        public void RaiseEvent<TEvent>(TEvent e) where TEvent : EntityEventArgs
        {
            gameScene.SceneEventManager.RaiseEvent(e);
        }

        protected BaseProcessingSystem()
        {

        }
    }
}
