using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SceneDataInterface;

namespace HSystemsCore
{
    public abstract class BaseRenderingSystem
    {
        public ISceneData gameScene;
        public virtual void Initialize()
        {

        }
        public virtual void Draw(GameTime gameTime, GraphicsDevice graphicsDevice, SpriteBatch spriteBatch)
        {

        }

        protected BaseRenderingSystem()
        {

        }
    }
}
