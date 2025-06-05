using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using static SceneDataInterface.ECSManager;
using StandartComponents.ECS;
using System.Xml.Linq;
using Physics;

namespace HPhysics.ECS.Collision.SpatialPartioning;

public class SpatialQuadTree
{
    public Vector2 centerLocation;
    public Rectangle mapBounds;
    public int numberBeforeDivide = 15999;
    public int dividesMax = 5;
    public QuadNode Root { get; private set; } = null!;

    public SpatialQuadTree(Vector2 centerLocation, Rectangle mapBounds)
    {
        Root = new QuadNode(numberBeforeDivide, 0, dividesMax, mapBounds, null);
        this.centerLocation = centerLocation;
        this.mapBounds = mapBounds;
    }

    public void Add(CollisionComponent value)
    {
        LocationComponent locationComponent = GetComponentOfEntity<LocationComponent>(value.entityOfComponent);

        Root.Add(value, locationComponent);

    }

    static public void RemoveObjectFromTree(CollisionComponent objectToRemove)
    {
        for (int i = 0; i < objectToRemove.currentQuadrants.Count; i++)
        {
            objectToRemove.currentQuadrants[i].RemoveCollision(objectToRemove);
        }
    }

    public void MinimizeTree()
    {
        Root.RecursiveCollisionsCounter();
        Root.MergeTreeBranch();
    }

    public List<QuadNode> GetAllLeafNodes()
    {
        List<QuadNode> leafNodes = new List<QuadNode>();
        if (Root != null)
        {
            CollectLeafNodes(Root, leafNodes);
        }
        return leafNodes;
    }

    private void CollectLeafNodes(QuadNode node, List<QuadNode> leafNodes)
    {
        if (node.LeftTop == null && node.RightTop == null && node.LeftBottom == null && node.RightBottom == null)
        {
            leafNodes.Add(node);
        }
        else
        {
            if (node.LeftTop != null) CollectLeafNodes(node.LeftTop, leafNodes);
            if (node.RightTop != null) CollectLeafNodes(node.RightTop, leafNodes);
            if (node.LeftBottom != null) CollectLeafNodes(node.LeftBottom, leafNodes);
            if (node.RightBottom != null) CollectLeafNodes(node.RightBottom, leafNodes);
        }
    }


    /*private Texture2D pixelTexture;

    public void InitializePixelTexture(GraphicsDevice graphicsDevice)
    {
        pixelTexture = new Texture2D(graphicsDevice, 1, 1);
        pixelTexture.SetData(new[] { Color.Red });
    }

    public void Draw(SpriteBatch spriteBatch, SpriteFont spriteFont)
    {
        if (Root != null)
        {
            DrawNode(spriteBatch, spriteFont, Root);
        }
    }

    private void DrawNode(SpriteBatch spriteBatch, SpriteFont spriteFont, QuadNode node)
    {
        DrawRectangle(spriteBatch, node.Bounds, new Color(0, 255, 0));
        DrawTextInCenter(spriteBatch, spriteFont, node.summaryCount.ToString(), node.Bounds, Color.Green);

        if (node.LeftTop != null) DrawNode(spriteBatch, spriteFont, node.LeftTop);
        if (node.RightTop != null) DrawNode(spriteBatch, spriteFont, node.RightTop);
        if (node.LeftBottom != null) DrawNode(spriteBatch, spriteFont, node.LeftBottom);
        if (node.RightBottom != null) DrawNode(spriteBatch, spriteFont, node.RightBottom);
    }

    private void DrawRectangle(SpriteBatch spriteBatch, Rectangle rectangle, Color color, int lineWidth = 3)
    {
        spriteBatch.Draw(pixelTexture, new Rectangle(rectangle.X, rectangle.Y, rectangle.Width, lineWidth), color);
        spriteBatch.Draw(pixelTexture, new Rectangle(rectangle.X, rectangle.Y + rectangle.Height - lineWidth, rectangle.Width, lineWidth), color);
        spriteBatch.Draw(pixelTexture, new Rectangle(rectangle.X, rectangle.Y, lineWidth, rectangle.Height), color);
        spriteBatch.Draw(pixelTexture, new Rectangle(rectangle.X + rectangle.Width - lineWidth, rectangle.Y, lineWidth, rectangle.Height), color);
    }


    private void DrawTextInCenter(SpriteBatch spriteBatch, SpriteFont spriteFont, string text, Rectangle rectangle, Color color)
    {
        Vector2 center = new Vector2(
            rectangle.X + rectangle.Width / 2,
            rectangle.Y + rectangle.Height / 2
        );

        Vector2 textSize = spriteFont.MeasureString(text);

        Vector2 textPosition = center - textSize / 2;

        spriteBatch.DrawString(spriteFont, text, textPosition, color);
    }

    public void DrawLine(SpriteBatch spriteBatch, Vector2 start, Vector2 end, Color color, int lineWidth = 1)
    {
        float distance = Vector2.Distance(start, end);

        float angle = (float)Math.Atan2(end.Y - start.Y, end.X - start.X);

        spriteBatch.Draw(
            pixelTexture,
            start,
            null,
            color,
            angle,
            Vector2.Zero,
            new Vector2(distance, lineWidth),
            SpriteEffects.None,
            110f
        );
    }*/
}

public class QuadNode
{
    public Rectangle Bounds { get; set; }
    public List<CollisionComponent> Collisions { get; private set; }
    public QuadNode Parent { get; private set; } = null!;
    public QuadNode LeftTop { get; private set; } = null!;
    public QuadNode RightTop { get; private set; } = null!;
    public QuadNode LeftBottom { get; private set; } = null!;
    public QuadNode RightBottom { get; private set; } = null!;

    public int summaryCount;
    public int maxCount;
    public int depth;
    public int maxDepth;

    public QuadNode(int _maxCount, int _depth, int _maxDepth, Rectangle _bounds, QuadNode _parent)
    {
        maxCount = _maxCount;
        Collisions = new List<CollisionComponent>();
        depth = _depth;
        maxDepth = _maxDepth;
        Bounds = _bounds;
        Parent = _parent;
    }

    public void Add(CollisionComponent collisionToAdd, LocationComponent locationComponent)
    {
        if (LeftTop is not null)
        {
            float x = locationComponent.location.X + collisionToAdd.collision.xOffset;
            float y = locationComponent.location.Y - collisionToAdd.collision.yOffset;

            if(collisionToAdd.collision is AABBCollision)// if ((collisionToAdd.collision as AABBCollision).GetWidth > 0 || collisionToAdd.rectangleCollision.Height > 0)
            {
                x -= collisionToAdd.collision.GetCenter.X;
                y += collisionToAdd.collision.GetCenter.Y;
            }

            if (IntersectQuadrant(x, y, collisionToAdd, LeftTop.Bounds)) TransferCollision(LeftTop, collisionToAdd, locationComponent);
            if (IntersectQuadrant(x, y, collisionToAdd, RightTop.Bounds)) TransferCollision(RightTop, collisionToAdd, locationComponent);
            if (IntersectQuadrant(x, y, collisionToAdd, LeftBottom.Bounds)) TransferCollision(LeftBottom, collisionToAdd, locationComponent);
            if (IntersectQuadrant(x, y, collisionToAdd, RightBottom.Bounds)) TransferCollision(RightBottom, collisionToAdd, locationComponent);

            return;
        }

        AddCollision(collisionToAdd);

        if (Collisions.Count > maxCount && depth < maxDepth)
        {
            (Rectangle, Rectangle, Rectangle, Rectangle) newBounds = DivideIntoQuadrants(Bounds);

            LeftTop = new QuadNode(maxCount, depth + 1, maxDepth, newBounds.Item1, this);
            RightTop = new QuadNode(maxCount, depth + 1, maxDepth, newBounds.Item2, this);
            LeftBottom = new QuadNode(maxCount, depth + 1, maxDepth, newBounds.Item3, this);
            RightBottom = new QuadNode(maxCount, depth + 1, maxDepth, newBounds.Item4, this);

            CollisionComponent[] tempCollisionsArray = Collisions.ToArray();
            Collisions.Clear();

            for (int i = 0; i < tempCollisionsArray.Length; i++)
            {
                Add(tempCollisionsArray[i], GetComponentOfEntity<LocationComponent>(tempCollisionsArray[i].entityOfComponent));
            }
        }
    }

    private void AddCollision(CollisionComponent collisionToAdd)
    {
        if (!Collisions.Contains(collisionToAdd)) Collisions.Add(collisionToAdd);

        if (!collisionToAdd.currentQuadrants.Contains(this)) collisionToAdd.currentQuadrants.Add(this);
    }

    private void TransferCollision(QuadNode transferTo, CollisionComponent collisionToTransfer, LocationComponent locationComponent)
    {
        RemoveCollision(collisionToTransfer);
        transferTo.Add(collisionToTransfer, locationComponent);
    }

    public void RemoveCollision(CollisionComponent collisionToRemove)
    {
        Collisions.Remove(collisionToRemove);
        collisionToRemove.currentQuadrants.Remove(this);
    }

    public void MergeTreeBranch()
    {
        if (LeftTop is not null)
        {
            LeftTop.MergeTreeBranch();
            RightTop.MergeTreeBranch();
            LeftBottom.MergeTreeBranch();
            RightBottom.MergeTreeBranch();

            if (LeftTop.LeftTop is null && RightTop.LeftTop is null && LeftBottom.LeftTop is null && RightBottom.LeftTop is null && summaryCount < maxCount)
            {
                CollisionComponent[] tempCollisionsArray = LeftTop.Collisions.ToArray();
                LeftTop.Collisions.Clear();
                for (int i = 0; i < tempCollisionsArray.Length; i++)
                {
                    LeftTop.TransferCollision(this, tempCollisionsArray[i], GetComponentOfEntity<LocationComponent>(tempCollisionsArray[i].entityOfComponent));
                }
                LeftTop = null;

                tempCollisionsArray = RightTop.Collisions.ToArray();
                RightTop.Collisions.Clear();
                for (int i = 0; i < tempCollisionsArray.Length; i++)
                {
                    RightTop.TransferCollision(this, tempCollisionsArray[i], GetComponentOfEntity<LocationComponent>(tempCollisionsArray[i].entityOfComponent));
                }
                RightTop = null;

                tempCollisionsArray = LeftBottom.Collisions.ToArray();
                LeftBottom.Collisions.Clear();
                for (int i = 0; i < tempCollisionsArray.Length; i++)
                {
                    LeftBottom.TransferCollision(this, tempCollisionsArray[i], GetComponentOfEntity<LocationComponent>(tempCollisionsArray[i].entityOfComponent));
                }
                LeftBottom = null;

                tempCollisionsArray = RightBottom.Collisions.ToArray();
                RightBottom.Collisions.Clear();
                for (int i = 0; i < tempCollisionsArray.Length; i++)
                {
                    RightBottom.TransferCollision(this, tempCollisionsArray[i], GetComponentOfEntity<LocationComponent>(tempCollisionsArray[i].entityOfComponent));
                }
                RightBottom = null;
            }
        }
    }

    public int RecursiveCollisionsCounter()
    {
        int count = 0;

        if (LeftTop is not null)
        {
            count += LeftTop.RecursiveCollisionsCounter();
            count += RightTop.RecursiveCollisionsCounter();
            count += LeftBottom.RecursiveCollisionsCounter();
            count += RightBottom.RecursiveCollisionsCounter();
        }
        else
        {
            count = Collisions.Count;
        }

        summaryCount = count;

        return count;
    }


    public bool IntersectQuadrant(float x, float y, CollisionComponent component, Rectangle quadrantBounds)
    {
        /*        float qMinX = quadrantBounds.Left;
                float qMaxX = quadrantBounds.Right;
                float qMinY = quadrantBounds.Top;
                float qMaxY = quadrantBounds.Bottom;

                if (component.collision is AABBCollision aabb)
                {
                    float objMinX = aabb.xMin + x;
                    float objMaxX = aabb.xMax + x;
                    float objMinY = aabb.yMin - y;
                    float objMaxY = aabb.yMax - y;

                    return !(objMaxX < qMinX || objMinX > qMaxX ||
                             objMaxY < qMinY || objMinY > qMaxY);
                }

                if (component.collision is CircleCollision circle)
                {
                    float centerX = x + circle.xOffset;
                    float centerY = y - circle.yOffset;

                    float closestX = Math.Clamp(centerX, qMinX, qMaxX);
                    float closestY = Math.Clamp(centerY, qMinY, qMaxY);

                    float dx = centerX - closestX;
                    float dy = centerY - closestY;

                    return (dx * dx + dy * dy) <= (circle.radius * circle.radius);
                }

                return false;*/
        if (component.collision is AABBCollision)//(width > 0 || height > 0)
        {
            float width = (component.collision as AABBCollision).GetWidth;
            float height = (component.collision as AABBCollision).GetHeight;

            return !(x + width <= quadrantBounds.Left ||
                     x >= quadrantBounds.Right ||
                     y + height <= quadrantBounds.Top ||
                     y >= quadrantBounds.Bottom);
        }

        if (component.collision is CircleCollision)//(radius > 0)
        {
            float radius = (component.collision as CircleCollision).radius;

            float closestX = Math.Clamp(x, quadrantBounds.Left, quadrantBounds.Right);
            float closestY = Math.Clamp(y, quadrantBounds.Top, quadrantBounds.Bottom);

            float distanceX = x - closestX;
            float distanceY = y - closestY;

            return (distanceX * distanceX + distanceY * distanceY) <= (radius * radius);
        }

        return false;
    }

    static private (Rectangle, Rectangle, Rectangle, Rectangle) DivideIntoQuadrants(Rectangle rectangle)
    {
        int midX = rectangle.X + rectangle.Width / 2;
        int midY = rectangle.Y + rectangle.Height / 2;

        Rectangle leftTop = new Rectangle(rectangle.X, rectangle.Y, rectangle.Width / 2, rectangle.Height / 2);
        Rectangle rightTop = new Rectangle(midX, rectangle.Y, rectangle.Width / 2, rectangle.Height / 2);
        Rectangle leftBottom = new Rectangle(rectangle.X, midY, rectangle.Width / 2, rectangle.Height / 2);
        Rectangle rightBottom = new Rectangle(midX, midY, rectangle.Width / 2, rectangle.Height / 2);

        return (leftTop, rightTop, leftBottom, rightBottom);
    }
}