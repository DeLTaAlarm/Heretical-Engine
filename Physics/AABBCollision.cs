using Microsoft.Xna.Framework;

namespace Physics
{
    public sealed class AABBCollision : Collision
    {
        public float xMin, xMax, yMin, yMax;
        public override Vector2 GetCenter { get { return new Vector2((xMin + xMax) / 2, (yMin + yMax) / 2); } }
        public float GetWidth { get { return xMax - xMin; } }
        public float GetHeight { get {  return yMax - yMin; } }

        public override bool CollidesWith(Collision other) => other.CollidesWithAABB(this);

        public override bool CollidesWithCircle(in CircleCollision circle)
        {
            float closestX = MathUtility.Clamp(circle.xOffset, this.xMin, this.xMax);
            float closestY = MathUtility.Clamp(circle.yOffset, this.yMin, this.yMax);

            float dx = circle.xOffset - closestX;
            float dy = circle.yOffset - closestY;

            return (dx * dx + dy * dy) <= (circle.radius * circle.radius);
        }

        public override bool CollidesWithAABB(in AABBCollision other)
        {
            return !(this.xMax < other.xMin || this.xMin > other.xMax ||
                     this.yMax < other.yMin || this.yMin > other.yMax);
        }

        public AABBCollision(float xMin, float xMax, float yMin, float yMax)
        {
            this.xMin = xMin;
            this.xMax = xMax;
            this.yMin = yMin;
            this.yMax = yMax;
        }
    }
}
