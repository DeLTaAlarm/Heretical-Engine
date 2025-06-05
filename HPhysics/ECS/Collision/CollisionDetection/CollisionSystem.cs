using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using HPhysics.ECS.Collision.SpatialPartioning;
using HSystemsCore;
using static SceneDataInterface.ECSManager;
using StandartComponents.ECS;
using Physics;

namespace HPhysics.ECS.Collision.CollisionDetection;

[SystemsOrder("Physics", 1)]
public class CollisionSystem : BaseProcessingSystem
{
    private Type locationComponentType = typeof(LocationComponent);

    public override void Update(GameTime gameTime)
    {
        //var sw = new Stopwatch();
        //sw.Start();

        CheckAllIntersections();
        //sw.Stop();
        //Debug.WriteLine(sw.ElapsedMilliseconds + "ms with collisions amount - " + gameWorld.componentsByType[typeof(CollisionComponent)].Item1.Count);
    }

    private void CheckAllIntersections()
    {
        CollisionsSceneDataContainer collisionsContainer = GetSceneDataContainer<CollisionsSceneDataContainer>(gameScene);
        CollisionComponent[] collisionsArray = GetAllComponentsList<CollisionComponent>(gameScene).ToArray();

        for (int i = 0; i < collisionsArray.Length; i++)
        {
            if (!collisionsArray[i].wasChangedFromLastFrame || collisionsArray[i].collisionType == CollisionInteractionType.Block)
                continue;

            LocationComponent locationComponent = (LocationComponent)GetComponentOfEntityByType(collisionsArray[i].entityOfComponent, locationComponentType);

            List<QuadNode> quadrantlist = collisionsArray[i].currentQuadrants;

            for (int j = 0; j < quadrantlist.Count; j++)
            {
                for (int k = 0; k < quadrantlist[j].Collisions.Count; k++)
                {
                    if (collisionsArray[i] == quadrantlist[j].Collisions[k])
                        continue;

                    LocationComponent locationComponent2 = (LocationComponent)GetComponentOfEntityByType(quadrantlist[j].Collisions[k].entityOfComponent, locationComponentType);

                    collisionsArray[i].collision.locX = locationComponent.location.X;
                    collisionsArray[i].collision.locY = locationComponent.location.Y;

                    if (collisionsArray[i].collision.CollidesWith(quadrantlist[j].Collisions[k].collision, locationComponent2.location.X, locationComponent2.location.Y))
                    {
                        Collide(collisionsArray[i], quadrantlist[j].Collisions[k], collisionsContainer);
                    }
                }
            }
        }
    }
    static private void Collide(CollisionComponent collision1, CollisionComponent collision2, CollisionsSceneDataContainer collisionsContainer)
    {
        collisionsContainer.collisionOriginTargetPairs.Add((collision1.entityOfComponent, collision2.entityOfComponent));
    }

    /*private void CheckAllIntersections()
    {
        CollisionComponent[] collisionsArray = GetAllComponentsList<CollisionComponent>(gameScene).ToArray();

        for (int i = 0; i < collisionsArray.Length; i++)
        {
            if (!collisionsArray[i].wasChangedFromLastFrame || collisionsArray[i].collisionType == CollisionInteractionType.Block) { continue; }

            List<QuadNode> quadrantlist = collisionsArray[i].currentQuadrants;
            LocationComponent locationComponent = (LocationComponent)GetComponentOfEntityByType(collisionsArray[i].entityOfComponent, locationComponentType);

            float x = locationComponent.location.X + collisionsArray[i].collisionOffset.X;
            float y = locationComponent.location.Y - collisionsArray[i].collisionOffset.Y;

            if (collisionsArray[i].rectangleCollision.Width > 0 || collisionsArray[i].rectangleCollision.Height > 0)
            {
                x -= collisionsArray[i].rectangleCollision.Center.X;
                y += collisionsArray[i].rectangleCollision.Center.Y;
            }

            for (int j = 0; j < quadrantlist.Count; j++)
            {
                for (int k = 0; k < quadrantlist[j].Collisions.Count; k++)
                {
                    if (collisionsArray[i] == quadrantlist[j].Collisions[k]) continue;

                    LocationComponent locationComponent2 = (LocationComponent)GetComponentOfEntityByType(quadrantlist[j].Collisions[k].entityOfComponent, locationComponentType);

                    float x2 = locationComponent2.location.X + quadrantlist[j].Collisions[k].collisionOffset.X;
                    float y2 = locationComponent2.location.Y - quadrantlist[j].Collisions[k].collisionOffset.Y;

                    if (quadrantlist[j].Collisions[k].rectangleCollision.Width > 0 || quadrantlist[j].Collisions[k].rectangleCollision.Height > 0)
                    {
                        x2 -= quadrantlist[j].Collisions[k].rectangleCollision.Center.X;
                        y2 += quadrantlist[j].Collisions[k].rectangleCollision.Center.Y;
                    }

                    if (CheckIntersections(x, y, collisionsArray[i].rectangleCollision.Width, collisionsArray[i].rectangleCollision.Height, collisionsArray[i].circleCollision.radius,
                                       x2, y2, quadrantlist[j].Collisions[k].rectangleCollision.Width, quadrantlist[j].Collisions[k].rectangleCollision.Height, quadrantlist[j].Collisions[k].circleCollision.radius))
                    {
                        Collide(collisionsArray[i], quadrantlist[j].Collisions[k], gameScene);
                    }
                }
            }
        }*/
    /*CollisionComponent[] ccListRef = GetAllComponents<CollisionComponent>().ToArray();
    List<LocationComponent> locComponentsTemp = new List<LocationComponent>();
    List<float> locationXlist = new List<float>();
    List<float> locationYlist = new List<float>();
    List<float> rectangleWidthList = new List<float>();
    List<float> rectangleHeightList = new List<float>();
    List<float> circleRadiusList = new List<float>();

    for (int i = 0; i < ccListRef.Length; i++)
    {
        LocationComponent tlc = GetComponentOfEntity<LocationComponent>(ccListRef[i].entityOfComponent);

        locComponentsTemp.Add(tlc);

        locationXlist.Add(tlc.location.X + ccListRef[i].collisionOffset.X);
        locationYlist.Add(tlc.location.Y - ccListRef[i].collisionOffset.Y);
        rectangleWidthList.Add(ccListRef[i].rectangleCollision.Width);
        rectangleHeightList.Add(ccListRef[i].rectangleCollision.Height);
        circleRadiusList.Add(ccListRef[i].circleCollision.radius);

        if (ccListRef[i].rectangleCollision.Width > 0 || ccListRef[i].rectangleCollision.Height > 0)
        {
            locationXlist[i] = locationXlist[i] - ccListRef[i].rectangleCollision.Center.X;
            locationYlist[i] = locationYlist[i] + ccListRef[i].rectangleCollision.Center.Y;
        }
    }
    float[] locationX = locationXlist.ToArray();
    float[] locationY = locationYlist.ToArray();
    float[] rectangleWidth = rectangleWidthList.ToArray();
    float[] rectangleHeight = rectangleHeightList.ToArray();
    float[] circleRadius = circleRadiusList.ToArray();

    for (int i = 0; i < ccListRef.Length; i++)
    {
        if (ccListRef[i].isStatic || ccListRef[i].collisionType == CollisionInteractionType.Block) { continue; } //Если объект статичный, он ни с чем не пересечется сам по себе. Если объект только блокирует, то это событие никогда не будет вызвано, т.к. блокировка не дает пересекаться
        CheckIntersectionsFor(i, ccListRef, locationX, locationY, rectangleWidth, rectangleHeight, circleRadius);
    }*/
}

    //private void CheckIntersectionsFor(int componentID, CollisionComponent[] ccListRef, float[] locationX, float[] locationY, float[] rectangleWidth, float[] rectangleHeight, float[] circleRadius)
    //{
    //    for (int i = 0; i < ccListRef.Length; i++)
    //    {
    //        if (ccListRef[componentID] == ccListRef[i]) continue;
    //        if (CheckIntersections(locationX[componentID], locationY[componentID], rectangleWidth[componentID], rectangleHeight[componentID], circleRadius[componentID], locationX[i], locationY[i], rectangleWidth[i], rectangleHeight[i], circleRadius[i]))
    //            Collide(ccListRef[componentID], ccListRef[i], gameWorld);
    //    }
    //}

    /*static public bool CheckIntersections(float locationXFirst, float locationYFirst, float rectangleWidthFirst, float rectangleHeightFirst, float circleRadiusFirst, float locationXSecond, float locationYSecond, float rectangleWidthSecond, float rectangleHeightSecond, float circleRadiusSecond)
    {
        if (circleRadiusFirst != 0 && circleRadiusSecond != 0)
        {
            //return CircleCollision.IntersectsStatic(locationXFirst, locationYFirst, circleRadiusFirst, locationXSecond, locationYSecond, circleRadiusSecond);
            //return CollisionChecker.CheckCollision();
        }

        if (!(rectangleWidthFirst == 0 && rectangleHeightFirst == 0) && !(rectangleWidthSecond == 0 && rectangleHeightSecond == 0))
        {
            return IntersectsRectangle(locationXFirst, locationYFirst, rectangleWidthFirst, rectangleHeightFirst, locationXSecond, locationYSecond, rectangleWidthSecond, rectangleHeightSecond);
        }

        if (circleRadiusFirst != 0 && !(rectangleWidthSecond == 0 && rectangleHeightSecond == 0))
        {
            return CircleCollision.IntersectsWithRectangleStatic(locationXFirst, locationYFirst, circleRadiusFirst, locationXSecond, locationYSecond, rectangleWidthSecond, rectangleHeightSecond);
        }

        if (!(rectangleWidthFirst == 0 && rectangleHeightFirst == 0) && circleRadiusSecond != 0)
        {
            return CircleCollision.IntersectsWithRectangleStatic(locationXSecond, locationYSecond, circleRadiusSecond, locationXFirst, locationYFirst, rectangleWidthFirst, rectangleHeightFirst);
        }

        return false;

        bool IntersectsRectangle(float locationXFirst, float locationYFirst, float rectangleWidthFirst, float rectangleHeightFirst, float locationXSecond, float locationYSecond, float rectangleWidthSecond, float rectangleHeightSecond)
        {

            if (locationXFirst < locationXSecond + rectangleWidthSecond && locationXSecond < locationXFirst + rectangleWidthFirst && locationYFirst < locationYSecond + rectangleHeightSecond)
            {
                return locationYSecond < locationYFirst + rectangleHeightFirst;
            }

            return false;
        }
    }*/

    //static public bool IntersectWithAnything(CollisionComponent collisionComponent, LocationComponent locationComponent, GameScene gameWorld)
    //{

    //    if (collisionComponent.collisionType == CollisionInteractionType.Overlap) return false; //Если объект не может быть заблокирован, то не нужно проверять его на пересечение здесь

    //    float x1;
    //    float y1;
    //    float w1 = 0;
    //    float h1 = 0;
    //    float r1 = 0;
    //    float x2;
    //    float y2;
    //    float w2 = 0;
    //    float h2 = 0;
    //    float r2 = 0;

    //    List<LocationComponent> locationComponentsList = new List<LocationComponent>();
    //    CollisionComponent[] collisionComponents = GetAllComponentsList<CollisionComponent>(gameWorld).ToArray();

    //    for (int i = 0; i < collisionComponents.Length; i++) locationComponentsList.Add(GetComponentOfEntity<LocationComponent>(collisionComponents[i].entityOfComponent));

    //    LocationComponent[] locationComponents = locationComponentsList.ToArray();

    //    x1 = locationComponent.location.X + collisionComponent.collisionOffset.X;
    //    y1 = locationComponent.location.Y - collisionComponent.collisionOffset.Y;
    //    w1 = collisionComponent.rectangleCollision.Width;
    //    h1 = collisionComponent.rectangleCollision.Height;
    //    r1 = collisionComponent.circleCollision.radius;

    //    if (collisionComponent.rectangleCollision.Width > 0 || collisionComponent.rectangleCollision.Height > 0)
    //    {
    //        x1 = x1 - collisionComponent.rectangleCollision.Center.X;
    //        y1 = y1 + collisionComponent.rectangleCollision.Center.Y;
    //    }


    //    for (int i = 0; i < collisionComponents.Length; i++)
    //    {
    //        if (collisionComponents[i] == collisionComponent || collisionComponents[i].collisionType == CollisionInteractionType.Overlap) continue;

    //        x2 = locationComponents[i].location.X + collisionComponents[i].collisionOffset.X;
    //        y2 = locationComponents[i].location.Y - collisionComponents[i].collisionOffset.Y;
    //        w2 = collisionComponents[i].rectangleCollision.Width;
    //        h2 = collisionComponents[i].rectangleCollision.Height;
    //        r2 = collisionComponents[i].circleCollision.radius;

    //        if (collisionComponents[i].rectangleCollision.Width > 0 || collisionComponents[i].rectangleCollision.Height > 0)
    //        {
    //            x2 = x2 - collisionComponents[i].rectangleCollision.Center.X;
    //            y2 = y2 + collisionComponents[i].rectangleCollision.Center.Y;
    //        }

    //        if (CheckIntersections(x1, y1, w1, h1, r1, x2, y2, w2, h2, r2))
    //        {
    //            if (collisionComponents[i].collisionType == CollisionInteractionType.Block)
    //            {
    //                return true;
    //            }
    //            else
    //            {
    //                Collide(collisionComponent, collisionComponents[i], gameWorld);
    //            }
    //        }
    //    }

    //    return false;
    //}
//}
