using Microsoft.Xna.Framework;

namespace Physics
{
    public sealed class CircleCollision : Collision
    {
        public float radius;

        public override Vector2 GetCenter { get { return new Vector2(xOffset, yOffset); } }
        public CircleCollision(float _radius)
        {
            xOffset = 0;
            yOffset = 0;
            radius = _radius;
        }

        public CircleCollision(float _x, float _y, float _radius)
        {
            xOffset = _x;
            yOffset = -_y;
            radius = _radius;
        }

        public CircleCollision(Vector2 _loc, float _radius)
        {
            xOffset = _loc.X;
            yOffset = -_loc.Y;
            radius = _radius;
        }

        public override bool CollidesWith(Collision other) => other.CollidesWithCircle(this);

        public override bool CollidesWithCircle(in CircleCollision other)
        {
            float dx = other.xOffset - this.xOffset;
            float dy = other.yOffset - this.yOffset;
            float combinedRadius = this.radius + other.radius;
            return (dx * dx + dy * dy) < (combinedRadius * combinedRadius);
        }

        public override bool CollidesWithAABB(in AABBCollision aabb)
        {
            float closestX = MathUtility.Clamp(this.xOffset, aabb.xMin, aabb.xMax);
            float closestY = MathUtility.Clamp(this.yOffset, aabb.yMin, aabb.yMax);

            float dx = this.xOffset - closestX;
            float dy = this.yOffset - closestY;

            return (dx * dx + dy * dy) <= (this.radius * this.radius);
        }

        public Rectangle CircleToRectangle() 
        {
            return new Rectangle((int)xOffset, (int)yOffset, (int)radius / 2, (int)radius / 2);
        }
    }
}
