using HDataCore;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace HPhysics
{
    public class CollisionsSceneDataContainer : SceneDataContainer
    {
        public Rectangle quadTreeBounds;
        public HashSet<(Entity, Entity)> collisionOriginTargetPairs = new HashSet<(Entity, Entity)>();
    }
}
