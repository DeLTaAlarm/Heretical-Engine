using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;

namespace GraphicsData
{
    public class InvertEffect : PostprocessingEffect
    {
        public byte time = 0;
        public bool isForward = true;

        public override void Update(GameTime gameTime)
        {
            if (isForward) time = Math.Clamp((byte)(time + 3), (byte)0, (byte)255);
            else time = Math.Clamp((byte)(time - 3), (byte)0, (byte)255);
            if (time == 255) isForward = false;
            else if (time == 0) isForward = true;
        }
        public override void Draw(SpriteBatch spriteBatch, RenderTarget2D source)
        {
            Color[] pixels = new Color[source.Width * source.Height];
            source.GetData(pixels);

            for (int i = 0; i < pixels.Length; i++)
            {
                float inversionFactor = time / 255f;

                byte originalR = pixels[i].R;
                byte originalG = pixels[i].G;
                byte originalB = pixels[i].B;

                byte invertedR = (byte)(255 - originalR);
                byte invertedG = (byte)(255 - originalG);
                byte invertedB = (byte)(255 - originalB);

                pixels[i] = new Color(
                    (byte)(originalR + (invertedR - originalR) * inversionFactor),
                    (byte)(originalG + (invertedG - originalG) * inversionFactor),
                    (byte)(originalB + (invertedB - originalB) * inversionFactor),
                    pixels[i].A
                );
            }

            Texture2D invertedTexture = new Texture2D(source.GraphicsDevice, source.Width, source.Height);
            invertedTexture.SetData(pixels);

            spriteBatch.Draw(
                invertedTexture,
                new Rectangle(0, 0, source.Width, source.Height),
                Color.White
            );

            invertedTexture.Dispose();
        }
    }
}
