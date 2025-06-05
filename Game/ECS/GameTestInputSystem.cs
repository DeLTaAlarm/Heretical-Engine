using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using GraphicsData;
using Graphics.ECS.Camera;
using HPhysics.ECS.Collision.CollisionProcessing;
using HPhysics.ECS.Movement;
using Graphics.ECS.Rendering;
using HDataCore;
using HSystemsCore;
using Input.ECS.Control;
using static SceneDataInterface.ECSManager;

namespace HCore.ECS.Control
{
    [SystemsOrder("PostInput")]
    public class GameTestInputSystem : BaseProcessingSystem
    {
        public override void Initialize()
        {
            SubscribeEvent<ControlComponent, EventOnKeyboardInteractionArgs>(OnKeyboardInteraction);
            SubscribeEvent<ControlComponent, EventOnKeyboardInteractionArgs>(OnKeyboardInteractionSpriteSelect);
        }

        public void OnKeyboardInteraction(Entity entity, ControlComponent controlComponent, EventOnKeyboardInteractionArgs args)
        {
            MovementComponent mc = GetComponentOfEntity<MovementComponent>(entity);
            AnimationSelectorComponent asc = GetComponentOfEntity<AnimationSelectorComponent>(entity);

            if (mc != null)
            {
                if (args.key == Keys.Left)
                {
                    if (args.state == KeyState.Down)
                    {
                        mc.velocity.X = -1.5f;
                    }
                    else mc.velocity.X = 0;
                }

                if (args.key == Keys.Right)
                {
                    if (args.state == KeyState.Down)
                    {
                        mc.velocity.X = 1.5f;
                    }
                    else mc.velocity.X = 0;
                }

                if (args.key == Keys.Up)
                {
                    if (args.state == KeyState.Down)
                    {
                        mc.velocity.Y = 1.5f;
                    }
                    else mc.velocity.Y = 0;
                }

                if (args.key == Keys.Down)
                {
                    if (args.state == KeyState.Down)
                    {
                        mc.velocity.Y = -1.5f;
                    }
                    else mc.velocity.Y = 0;
                }

                if (args.key == Keys.Space)
                {
                    if (args.state == KeyState.Up) return;

                    CameraComponent cc = GetComponentOfEntity<CameraComponent>(entity);
                    if (cc.postprocessingEffects.Count == 0) cc.postprocessingEffects.Add(new InvertEffect());
                    else cc.postprocessingEffects.Clear();
                }
            }
        }

        public void OnKeyboardInteractionSpriteSelect(Entity entity, ControlComponent controlComponent, EventOnKeyboardInteractionArgs args)
        {
            MovementComponent mc = GetComponentOfEntity<MovementComponent>(entity);
            AnimationSelectorComponent asc = GetComponentOfEntity<AnimationSelectorComponent>(entity);
            SpriteAnimationComponent sac = GetComponentOfEntity<SpriteAnimationComponent>(entity);

            if (mc != null)
            {

                if (args.key == Keys.Up)
                {
                    if (args.state == KeyState.Down)
                    {
                        sac.animationData = asc.animations["MoveUp"];
                        sac.animationData.Play();
                    }
                    else sac.animationData.Reset();
                }

                if (args.key == Keys.Down)
                {
                    if (args.state == KeyState.Down)
                    {
                        sac.animationData = asc.animations["MoveDown"];
                        sac.animationData.Play();
                    }
                    else sac.animationData.Reset();
                }
            }
        }

        public override void Update(GameTime gameTime)
        {

        }
    }
}
