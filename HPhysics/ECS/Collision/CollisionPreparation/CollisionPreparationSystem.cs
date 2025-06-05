using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using HPhysics.ECS.Collision.SpatialPartioning;
using HSystemsCore;
using static SceneDataInterface.ECSManager;
using StandartComponents.ECS;
using Physics;

namespace HPhysics.ECS.Collision.CollisionPreparation;

[SystemsOrder("Physics", 0)]
public class CollisionPreparationSystem : BaseProcessingSystem
{
    public SpatialQuadTree quadTree;
    private Type locationComponentType = typeof(LocationComponent);
    public override void Initialize()
    {
        base.Initialize();


        quadTree = new SpatialQuadTree(new Vector2(0, 0), new Rectangle(-128 * 16, -128 * 16, 128 * 16, 128 * 16));
        //quadTree.InitializePixelTexture(gameScene.graphicsDevice);
    }

    public override void Update(GameTime gameTime)
    {

        UpdateTree();
    }

    private void UpdateTree()
    {
        CollisionComponent[] collisionsArray = GetAllComponents<CollisionComponent>(gameScene);
        
        for (int i = 0; i < collisionsArray.Length; i++)
        {
            Vector2 tempLocation = ((LocationComponent)GetComponentOfEntityByType(collisionsArray[i].entityOfComponent, locationComponentType)).location;
            if (collisionsArray[i].lastLocation != tempLocation)
            {
                collisionsArray[i].lastLocation = tempLocation;
                collisionsArray[i].wasChangedFromLastFrame = true;
                SpatialQuadTree.RemoveObjectFromTree(collisionsArray[i]);
            }
        }

        quadTree.MinimizeTree();

        for (int i = 0; i < collisionsArray.Length; i++)
        {
            if (collisionsArray[i].wasChangedFromLastFrame) quadTree.Add(collisionsArray[i]);
        }
    }
}
