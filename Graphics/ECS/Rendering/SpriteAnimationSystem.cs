using Microsoft.Xna.Framework;
using HSystemsCore;
using Graphics.ECS.Camera;
using static SceneDataInterface.ECSManager;

namespace Graphics.ECS.Rendering;

[SystemsOrder("Rendering")]
public class SpriteAnimationSystem : BaseProcessingSystem
{
    public override void Initialize()
    {

    }

    public override void Update(GameTime gameTime)
    {
        SpriteAnimationComponent[] animationComponents = GetAllComponents<SpriteAnimationComponent>(gameScene);

        for (int i = 0; i < animationComponents.Length; i++)
        {
            SpriteComponent spriteComponent = GetComponentOfEntity<SpriteComponent>(animationComponents[i].entityOfComponent);
            animationComponents[i].animationData.Update();
            spriteComponent.sprite = animationComponents[i].animationData.animationFrames[animationComponents[i].animationData.currentAnimationFrame];
        }
    }
}
