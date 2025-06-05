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

        public override bool CollidesWith(Collision other, float locX, float locY) => other.CollidesWithCircle(this, locX, locY);

        public override bool CollidesWithCircle(in CircleCollision other, float locX, float locY)
        {
            float dx = (other.locX + other.xOffset) - (locX + this.xOffset);
            float dy = (other.locY - other.yOffset) - (locY - this.yOffset);
            float combinedRadius = this.radius + other.radius;
            return (dx * dx + dy * dy) < (combinedRadius * combinedRadius);
        }

        public override bool CollidesWithAABB(in AABBCollision aabb, float locX, float locY)
        {
            float otherLocX = aabb.locX;
            float otherLocY = aabb.locY;

            float thisCenterX = this.xOffset + locX;
            float thisCenterY = this.yOffset - locY;

            float aabbMinX = aabb.xMin + otherLocX;
            float aabbMaxX = aabb.xMax + otherLocX;
            float aabbMinY = aabb.yMin - otherLocY;
            float aabbMaxY = aabb.yMax - otherLocY;

            float closestX = MathUtility.Clamp(thisCenterX, aabbMinX, aabbMaxX);
            float closestY = MathUtility.Clamp(thisCenterY, aabbMinY, aabbMaxY);

            float dx = thisCenterX - closestX;
            float dy = thisCenterY - closestY;

            return (dx * dx + dy * dy) <= (this.radius * this.radius);
        }

        public Rectangle CircleToRectangle() 
        {
            return new Rectangle((int)xOffset, (int)yOffset, (int)radius / 2, (int)radius / 2);
        }
    }
}
