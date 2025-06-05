using Microsoft.Xna.Framework;

namespace GraphicsData
{
    public class Sprite
    {
        private SpriteAtlas atlas;
        private int currentFrameID;
        private Rectangle boundsOfSprite;

        public SpriteAtlas Atlas
        {
            get
            {
                return atlas;
            }
        }

        public int FrameID
        {
            get
            {
                return currentFrameID;
            }
            set
            {
                currentFrameID = value;
                boundsOfSprite = atlas.GetFrameSource(value);
            }
        }

        public Rectangle BoundsOfSprite
        {
            get
            {
                return boundsOfSprite;
            }
        }

        public Sprite(SpriteAtlas spriteAtlas, int frameOfAtlas)
        {
            atlas = spriteAtlas;
            currentFrameID = frameOfAtlas;
            boundsOfSprite = atlas.GetFrameSource(frameOfAtlas);
        }
    }
}
