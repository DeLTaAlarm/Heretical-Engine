using System.Collections.Generic;
using GraphicsData;
using HDataCore;

namespace Graphics.ECS.Rendering;

public class SpriteAnimationComponent : BaseComponent
{
    public SpriteAnimation animationData;

    public SpriteAnimationComponent(List<Sprite> animationFrames, int _framesPerSecond, bool play)
    {
        animationData = new SpriteAnimation(animationFrames, _framesPerSecond, play);
    }
}
