using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GraphicsData
{
    public class SpriteAtlas
    {
        public Texture2D Texture { get; }
        public int Rows { get; }
        public int Columns { get; }
        public int TotalFrames => Rows * Columns;

        public SpriteAtlas(Texture2D texture, int rows, int columns)
        {
            Texture = texture;
            Rows = rows;
            Columns = columns;
        }

        public Rectangle GetFrameSource(int frameID)
        {
            int widthOfFrame = Texture.Width / Columns;
            int heightOfFrame = Texture.Height / Rows;

            int row = frameID / Columns;
            int column = frameID % Columns;

            return new Rectangle(widthOfFrame * column, heightOfFrame * row, widthOfFrame, heightOfFrame);
        }
    }
}
