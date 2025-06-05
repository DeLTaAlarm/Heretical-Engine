/*using HSystemsCore;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using StandartComponents.ECS;
using static SceneDataInterface.ECSManager;
using Physics;
using HPhysics.ECS.Collision;
using HPhysics;
using System.Collections;

namespace DebugTools
{
    [SystemsRenderingOrder("Rendering", 128)]
    public class DebugCollisionsRenderingSystem : BaseRenderingSystem
    {
        private Texture2D whitePixel;

        private Texture2D circleTexture;

        private Texture2D GenerateCircleTexture(GraphicsDevice device, int diameter = 32)
        {
            Texture2D texture = new Texture2D(device, diameter, diameter);
            Color[] data = new Color[diameter * diameter];

            float radius = diameter / 2f;
            float radiusSq = radius * radius;
            float thickness = 1.5f;

            for (int y = 0; y < diameter; y++)
            {
                for (int x = 0; x < diameter; x++)
                {
                    float dx = x - radius;
                    float dy = y - radius;
                    float distSq = dx * dx + dy * dy;

                    if (distSq <= radiusSq && distSq >= (radius - thickness) * (radius - thickness))
                        data[y * diameter + x] = Color.White;
                    else
                        data[y * diameter + x] = Color.Transparent;
                }
            }

            texture.SetData(data);
            return texture;
        }
        public void DrawAABBBorder(SpriteBatch spriteBatch, AABBCollision aabb, float locX, float locY, bool isColliding)
        {
            Color color = isColliding ? Color.Red : Color.Green;

            float x = aabb.xMin + locX + aabb.xOffset;
            float y = aabb.yMin - locY - aabb.yOffset;
            float width = aabb.xMax - aabb.xMin;
            float height = aabb.yMax - aabb.yMin;
            int thickness = 2;

            // Верх
            spriteBatch.Draw(whitePixel, new Rectangle((int)x, (int)y, (int)width, thickness), color);
            // Низ
            spriteBatch.Draw(whitePixel, new Rectangle((int)x, (int)(y + height - thickness), (int)width, thickness), color);
            // Лево
            spriteBatch.Draw(whitePixel, new Rectangle((int)x, (int)y, thickness, (int)height), color);
            // Право
            spriteBatch.Draw(whitePixel, new Rectangle((int)(x + width - thickness), (int)y, thickness, (int)height), color);
        }

        public void DrawCircleBorder(SpriteBatch spriteBatch, CircleCollision circle, float locX, float locY, bool isColliding)
        {
            Color color = isColliding ? Color.Red : Color.Green;

            float radius = circle.radius;
            float centerX = locX + circle.xOffset;
            float centerY = locY - circle.yOffset;

            float scale = 1;

            spriteBatch.Draw(circleTexture,
                new Vector2(centerX - radius, centerY - radius),
                new Rectangle(0, 0, 32, 32),
                color * 1f,
                0,
                circle.GetCenter,
                scale,
                SpriteEffects.None,
                0);
        }

        public override void Initialize()
        {
            Texture2D whitePixel = new Texture2D(gameScene.GraphicsDevice, 1, 1);
            whitePixel.SetData(new[] { Color.White });
            circleTexture = GenerateCircleTexture(gameScene.GraphicsDevice);
        }

        public override void Draw(GameTime gameTime, GraphicsDevice graphicsDevice, SpriteBatch spriteBatch)
        {
            CollisionComponent[] cc = GetAllComponents<CollisionComponent>(gameScene);
            CollisionsSceneDataContainer container = GetSceneDataContainer<CollisionsSceneDataContainer>(gameScene);
            for (int i = 0; i < cc.Length; i++)
            {
                LocationComponent lc = GetComponentOfEntity<LocationComponent>(cc[i].entityOfComponent);

                if (lc is null) continue;   //Объекты не должны рисоваться в игровом мире, если они не имеют присутствия в игровом мире (отсутствуют координаты)

                if (cc[i].collision is AABBCollision)
                {
                    bool isColliding = false;
                    foreach (var pair in container.collisionOriginTargetPairs)
                    {
                        if (ReferenceEquals(pair.Item1, cc[i]) || ReferenceEquals(pair.Item2, cc[i]))
                        {
                            isColliding = true;
                            break;
                        }
                    }
                    DrawAABBBorder(spriteBatch, cc[i].collision as AABBCollision, lc.location.X, lc.location.Y, isColliding);
                }

                if (cc[i].collision is CircleCollision)
                {
                    bool isColliding = false;
                    foreach (var pair in container.collisionOriginTargetPairs)
                    {
                        if (ReferenceEquals(pair.Item1, cc[i]) || ReferenceEquals(pair.Item2, cc[i]))
                        {
                            isColliding = true;
                            break;
                        }
                    }
                    DrawCircleBorder(spriteBatch, cc[i].collision as CircleCollision, lc.location.X, lc.location.Y, isColliding);
                }
            }
        }
    }
}
*/