using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using HSystemsCore;
using StandartComponents.ECS;
using GraphicsData;
using static SceneDataInterface.ECSManager;

namespace HGraphics.ECS.Rendering
{
    [SystemsRenderingOrder("Rendering", 128)]
    public class SpriteRenderingSystem : BaseRenderingSystem
    {

        public override void Draw(GameTime gameTime, GraphicsDevice graphicsDevice, SpriteBatch spriteBatch)//, CameraComponent currentCamera)
        {
            SpriteComponent[] spriteComponents = GetAllComponents<SpriteComponent>(gameScene);



            for (int i = 0; i < spriteComponents.Length; i++)
            {
                LocationComponent lc = GetComponentOfEntity<LocationComponent>(spriteComponents[i].entityOfComponent);

                if (lc is null) continue;   //Объекты не должны рисоваться в игровом мире, если они не имеют присутствия в игровом мире (отсутствуют координаты)

                spriteBatch.Draw(
                        spriteComponents[i].sprite.Atlas.Texture,
                        lc.location + spriteComponents[i].offset,
                        spriteComponents[i].sprite.BoundsOfSprite,
                        Color.White,
                        spriteComponents[i].angle,
                        new Vector2(spriteComponents[i].sprite.BoundsOfSprite.Width / 2, spriteComponents[i].sprite.BoundsOfSprite.Height / 2),
                        new Vector2(spriteComponents[i].scale, spriteComponents[i].scale),
                        SpriteEffects.None,
                        spriteComponents[i].layer);

            }
        }
    }
}
