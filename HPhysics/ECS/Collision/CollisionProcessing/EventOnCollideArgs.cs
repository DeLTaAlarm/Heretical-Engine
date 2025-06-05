using EventsSystem.EventManage;
using HDataCore;

namespace HPhysics.ECS.Collision.CollisionProcessing;

public sealed class EventOnCollideArgs : EntityEventArgs
{
    public Entity first;
    public Entity second;

    public EventOnCollideArgs(Entity first, Entity second)
    {
        this.first = first;
        this.second = second;
    }
}
