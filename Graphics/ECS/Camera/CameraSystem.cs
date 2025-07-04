using Microsoft.Xna.Framework;
using StandartComponents.ECS;
using HSystemsCore;
using static SceneDataInterface.ECSManager;

namespace HGraphics.ECS.Camera;

[SystemsOrder("Rendering", 0)]
public class CameraSystem : BaseProcessingSystem
{
    public override void Initialize()
    {
        
    }

    public override void Update(GameTime gameTime)
    {
        CameraComponent[] cameraComponents = GetAllComponents<CameraComponent>(gameScene);

        for (int i = 0; i < cameraComponents.Length; i++)
        {
            if (cameraComponents[i].isActive)
            {
                LocationComponent locationComponent = (LocationComponent)GetComponentOfEntityByType(cameraComponents[i].entityOfComponent, typeof(LocationComponent));

                if (locationComponent != null)
                {
                    cameraComponents[i].camera.location.X = locationComponent.location.X;
                    cameraComponents[i].camera.location.Y = locationComponent.location.Y;
                    cameraComponents[i].camera.UpdateTransform();
                }
            }
        }
    }
}