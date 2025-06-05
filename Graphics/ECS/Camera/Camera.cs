using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;

namespace Graphics.ECS.Camera;

public class Camera
{
    public Matrix Transform { get; private set; }
    public Viewport Viewport { get; private set; }
    public Vector2 location = Vector2.Zero;
    public float rotation  = 0f;
    public float zoom = 1f;
    public RenderTarget2D RenderTarget { get; private set; }

    public Camera(Viewport viewport, GraphicsDevice graphicsDevice, float rotation = 0, float zoom = 1f)
    {
        Viewport = viewport;
        this.rotation = rotation;
        this.zoom = zoom;
        RenderTarget = new RenderTarget2D(graphicsDevice, graphicsDevice.PresentationParameters.BackBufferWidth, graphicsDevice.PresentationParameters.BackBufferHeight);//graphicsDevice, viewport.Width, viewport.Height);
        UpdateTransform();
    }

    public void UpdateTransform()
    {
        Transform =
            Matrix.CreateTranslation(new Vector3(-location, 0f)) *
            Matrix.CreateRotationZ(rotation) *
            Matrix.CreateScale(new Vector3(zoom, zoom, 1f)) *
            Matrix.CreateTranslation(new Vector3(Viewport.Width / 2f, Viewport.Height / 2f, 0f));
    }

    public void SetViewport(Viewport viewport)
    {
        Viewport = viewport;
        UpdateTransform();
    }

    public void Render(SpriteBatch spriteBatch, Action<SpriteBatch> drawAction)
    {
        drawAction?.Invoke(spriteBatch);
    }

    public void RenderToTarget(GraphicsDevice graphicsDevice, SpriteBatch spriteBatch, Action<SpriteBatch> drawAction, bool clearRenderTarget = true)
    {
        graphicsDevice.SetRenderTarget(RenderTarget);
        if (clearRenderTarget) graphicsDevice.Clear(Color.Transparent);

        Render(spriteBatch, drawAction);

        graphicsDevice.SetRenderTarget(null);
    }

    public void SaveRenderTargetToFile(GraphicsDevice graphicsDevice, string filePath)
    {
        using (Texture2D texture = new Texture2D(graphicsDevice, RenderTarget.Width, RenderTarget.Height))
        {
            Color[] data = new Color[RenderTarget.Width * RenderTarget.Height];
            RenderTarget.GetData(data);

            texture.SetData(data);

            using (var stream = System.IO.File.Create(filePath))
            {
                texture.SaveAsPng(stream, texture.Width, texture.Height);
            }
        }
    }
}
