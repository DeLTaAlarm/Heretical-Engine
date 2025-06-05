using Microsoft.Xna.Framework;

namespace Physics
{
    public abstract class Collision
    {
        public float xOffset;
        public float yOffset;

        public abstract Vector2 GetCenter { get;}
        public abstract bool CollidesWith(Collision other);
        public abstract bool CollidesWithCircle(in CircleCollision circle);
        public abstract bool CollidesWithAABB(in AABBCollision aabb);
    }
}
