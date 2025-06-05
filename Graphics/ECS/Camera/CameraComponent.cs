using GraphicsData;
using HDataCore;
using System.Collections.Generic;

namespace Graphics.ECS.Camera;

public class CameraComponent : BaseComponent
{
    public Camera camera;
    public List<PostprocessingEffect> postprocessingEffects;
    public bool isActive;

    public CameraComponent(Camera camera, bool isActive, List<PostprocessingEffect> postprocessingEffects)
    {
        this.camera = camera;
        this.isActive = isActive;
        this.postprocessingEffects = postprocessingEffects;
    }
}
