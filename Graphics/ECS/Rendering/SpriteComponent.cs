using Microsoft.Xna.Framework;
using HDataCore;
using GraphicsData;

namespace HGraphics.ECS.Rendering
{
    public class SpriteComponent : BaseComponent
    {
        public Sprite sprite;
        public Vector2 offset;
        public float angle;
        public float scale;
        public float layer;

        public SpriteComponent(Sprite _sprite, Vector2 spriteOffset, float spriteLayer, float spriteScale = 1, float spriteAngle = 0) 
        {
            sprite = _sprite;
            offset = spriteOffset;
            layer = spriteLayer;
            angle = spriteAngle;
            scale = spriteScale;
        }
    }
}
