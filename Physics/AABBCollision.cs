using Microsoft.Xna.Framework;

namespace Physics
{
    public sealed class AABBCollision : Collision
    {
        public float xMin, xMax, yMin, yMax;
        public override Vector2 GetCenter { get { return new Vector2((xMin + xMax) / 2, (yMin + yMax) / 2); } }
        public float GetWidth { get { return xMax - xMin; } }
        public float GetHeight { get {  return yMax - yMin; } }

        public override bool CollidesWith(Collision other, float locX, float locY) => other.CollidesWithAABB(this, locX, locY);

        public override bool CollidesWithCircle(in CircleCollision circle, float locX, float locY)
        {
            float thisMinX = this.xMin + locX;
            float thisMaxX = this.xMax + locX;
            float thisMinY = this.yMin - locY;
            float thisMaxY = this.yMax - locY;

            float circleX = circle.xOffset + circle.locX;
            float circleY = circle.yOffset - circle.locY;

            float closestX = MathUtility.Clamp(circleX, thisMinX, thisMaxX);
            float closestY = MathUtility.Clamp(circleY, thisMinY, thisMaxY);

            float dx = circleX - closestX;
            float dy = circleY - closestY;

            return (dx * dx + dy * dy) <= (circle.radius * circle.radius);
        }

        public override bool CollidesWithAABB(in AABBCollision other, float locX, float locY)
        {
            float thisMinX = this.xMin + locX;
            float thisMaxX = this.xMax + locX;
            float thisMinY = this.yMin - locY;
            float thisMaxY = this.yMax - locY;

            float otherMinX = other.xMin + other.locX;
            float otherMaxX = other.xMax + other.locX;
            float otherMinY = other.yMin - other.locY;
            float otherMaxY = other.yMax - other.locY;

            return !(thisMaxX < otherMinX || thisMinX > otherMaxX ||
                     thisMaxY < otherMinY || thisMinY > otherMaxY);
        }

        public AABBCollision(float xOffset, float yOffset, float xMin, float xMax, float yMin, float yMax)
        {
            this.xOffset = xOffset;
            this.yOffset = yOffset;
            this.xMin = xMin;
            this.xMax = xMax;
            this.yMin = yMin;
            this.yMax = yMax;
        }
    }
}
