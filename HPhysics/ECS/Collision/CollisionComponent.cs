using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using HPhysics.ECS.Collision.SpatialPartioning;
using HDataCore;
using Physics;


namespace HPhysics.ECS.Collision;

public class CollisionComponent : BaseComponent
{
    public Physics.Collision collision;
    //public bool isStatic = true;
    public Vector2 lastLocation = new Vector2(float.MinValue, float.MinValue);
    public bool wasChangedFromLastFrame;
    public CollisionInteractionType collisionType;
    public Vector2 collisionOffset;
    public List<QuadNode> currentQuadrants = new List<QuadNode>();



    public CollisionComponent(Physics.Collision _collision, CollisionInteractionType _collisionType, Vector2 _collisionOffset)
    {
        collision = _collision;
        collisionType = _collisionType;
        collisionOffset = _collisionOffset;
    }
}
