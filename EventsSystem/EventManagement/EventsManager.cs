using HDataCore;
using System;
using System.Collections.Generic;

namespace EventsSystem.EventManage;

public class EventsManager
{
    public delegate void EntityEventHandler<TComponent, TEvent>(Entity entityOrigin, TComponent component, TEvent e) where TComponent : BaseComponent where TEvent : EntityEventArgs;
    private Dictionary<Type, Dictionary<Type, Delegate>> entityEvents = new Dictionary<Type, Dictionary<Type, Delegate>>();

    public delegate void LocalEventHandler<TEvent>(TEvent e) where TEvent : EntityEventArgs;
    private Dictionary<Type, Delegate> localEvents = new Dictionary<Type, Delegate>();

    public void SubscribeEvent<TComponent, TEvent>(EntityEventHandler<TComponent, TEvent> eventHandler) where TComponent : BaseComponent where TEvent : EntityEventArgs
    {
        if (!entityEvents.ContainsKey(typeof(TEvent)))
        {
            entityEvents[typeof(TEvent)] = new Dictionary<Type, Delegate>() { { typeof(TComponent), eventHandler } };
        }
        else
        {
            if (!entityEvents[typeof(TEvent)].ContainsKey(typeof(TComponent))) entityEvents[typeof(TEvent)][typeof(TComponent)] = eventHandler;
            else entityEvents[typeof(TEvent)][typeof(TComponent)] = Delegate.Combine(entityEvents[typeof(TEvent)][typeof(TComponent)], eventHandler);
        }
    }

    public void UnsubscribeEvent<TComponent, TEvent>(EntityEventHandler<TComponent, TEvent> eventHandler) where TComponent : BaseComponent where TEvent : EntityEventArgs
    {
        if (entityEvents.ContainsKey(typeof(TEvent)))
        {
            if(entityEvents[typeof(TEvent)].ContainsKey(typeof(TComponent)))
                entityEvents[typeof(TEvent)][typeof(TComponent)] = Delegate.Remove(entityEvents[typeof(TEvent)][typeof(TComponent)], eventHandler);
        }
    }

    public void RaiseEvent<TEvent>(Entity entity, TEvent e) where TEvent : EntityEventArgs
    {
        if (entityEvents.ContainsKey(typeof(TEvent)))
        {
            foreach(KeyValuePair<Type, Delegate> d in entityEvents[typeof(TEvent)])
            {
                if (d.Value != null)
                {
                    BaseComponent targetComponent = GetComponentOfEntityByType(entity, d.Key);
                    
                    if (targetComponent != null)
                        d.Value.DynamicInvoke(entity, targetComponent, e); //НЕОБХОДИМО ИСПРАВИТЬ В ОБОЗРИМОМ БУДУЩЕМ, DynamicInvoke СЛИШКОМ РЕСУРСОЗАТРАТНЫЙ
                }
            }
        }
    }

    public void SubscribeEvent<TEvent>(LocalEventHandler<TEvent> eventHandler) where TEvent : EntityEventArgs
    {
        if (!localEvents.ContainsKey(typeof(TEvent)))
        {
            localEvents[typeof(TEvent)] = eventHandler;
        }
        else
        {
            localEvents[typeof(TEvent)] = Delegate.Combine(localEvents[typeof(TEvent)], eventHandler);
        }
    }

    public void UnsubscribeEvent<TEvent>(LocalEventHandler<TEvent> eventHandler) where TEvent : EntityEventArgs
    {
        if (localEvents.ContainsKey(typeof(TEvent)))
        {
            localEvents[typeof(TEvent)] = Delegate.Remove(localEvents[typeof(TEvent)], eventHandler);
        }
    }

    public void RaiseEvent<TEvent>(TEvent e) where TEvent : EntityEventArgs
    {
        if (localEvents.ContainsKey(typeof(TEvent)))
        {
            Delegate eventDelegate = localEvents[typeof(TEvent)];

            if (eventDelegate != null)
            {
                eventDelegate.DynamicInvoke(e); //НЕОБХОДИМО ИСПРАВИТЬ В ОБОЗРИМОМ БУДУЩЕМ, DynamicInvoke СЛИШКОМ РЕСУРСОЗАТРАТНЫЙ
            }
        }
    }


    //Дубликат из ECSManager
    static private BaseComponent GetComponentOfEntityByType(Entity entity, Type type)
    {
        BaseComponent tempComponent = null;
        entity.components.TryGetValue(type, out tempComponent);
        return tempComponent;
    }
}
