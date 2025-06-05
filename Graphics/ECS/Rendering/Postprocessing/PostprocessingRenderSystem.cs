using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Graphics.ECS.Camera;
using System.Linq;
using HSystemsCore;
using static SceneDataInterface.ECSManager;
using HCore;

namespace Graphics.ECS.Rendering.Postprocessing
{
    [SystemsRenderingOrder("Postprocessing", 128)]
    internal class PostprocessingRenderSystem : BaseRenderingSystem
    {

        public override void Draw(GameTime gameTime, GraphicsDevice graphicsDevice, SpriteBatch spriteBatch)//, CameraComponent currentCamera)
        {
            CameraComponent[] cameraComponents = GetAllComponents<CameraComponent>(gameScene);

            CameraComponent currentCamera = GetSceneDataContainer<CurrentCameraSceneContainer>(gameScene).currentCamera;

            if (currentCamera.postprocessingEffects != null && currentCamera.postprocessingEffects.Count > 0)
            {
                for (int j = 0; j < currentCamera.postprocessingEffects.Count; j++)
                {
                    currentCamera.postprocessingEffects[j].Draw(spriteBatch, currentCamera.camera.RenderTarget);
                }
            }
        }
    }
}
