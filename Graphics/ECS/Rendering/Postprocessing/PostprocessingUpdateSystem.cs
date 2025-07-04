using Microsoft.Xna.Framework;
using HSystemsCore;
using HGraphics.ECS.Camera;
using static SceneDataInterface.ECSManager;

namespace Graphics.ECS.Rendering.Postprocessing
{
    [SystemsOrder("Rendering")]
    public class PostprocessingUpdateSystem : BaseProcessingSystem
    {
        public override void Update(GameTime gameTime)
        {
            CameraComponent[] cameraComponents = GetAllComponents<CameraComponent>(gameScene);

            for (int i = 0; i < cameraComponents.Length; i++)
            {
                if (cameraComponents[i].postprocessingEffects != null && cameraComponents[i].postprocessingEffects.Count > 0)
                {
                    for (int j = 0; j < cameraComponents[i].postprocessingEffects.Count; j++)
                    {
                        cameraComponents[i].postprocessingEffects[j].Update(gameTime);
                    }
                }
            }
        }
    }
}
