using GraphicsData;
using System.Collections.Generic;
using HDataCore;

namespace Graphics.ECS.Rendering
{
    public class AnimationSelectorComponent : BaseComponent
    {
        public Dictionary<string, SpriteAnimation> animations;

        public AnimationSelectorComponent(Dictionary<string, SpriteAnimation> animations) 
        {
            this.animations = animations;
        }
    }
}
