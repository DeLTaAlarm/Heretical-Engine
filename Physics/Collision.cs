using Microsoft.Xna.Framework;

namespace Physics
{
    public abstract class Collision
    {
        public float locX;
        public float locY;
        public float xOffset;
        public float yOffset;

        public abstract Vector2 GetCenter { get;}
        public abstract bool CollidesWith(Collision other, float locX, float locY);
        public abstract bool CollidesWithCircle(in CircleCollision circle, float locX, float locY);
        public abstract bool CollidesWithAABB(in AABBCollision aabb, float locX, float locY);
    }
}
