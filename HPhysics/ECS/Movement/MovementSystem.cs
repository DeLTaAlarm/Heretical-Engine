using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using HPhysics.ECS.Collision;
using HPhysics.ECS.Collision.CollisionDetection;
using HPhysics.ECS.Collision.CollisionProcessing;
using static SceneDataInterface.ECSManager;
using HSystemsCore;
using HDataCore;
using StandartComponents.ECS;
using Physics;

namespace HPhysics.ECS.Movement;

[SystemsOrder("PrePhysics")]
public class MovementSystem : BaseProcessingSystem
{
    private Type locationComponentType = typeof(LocationComponent);
    private Type collisionComponentType = typeof(CollisionComponent);
    //private Type controlComponentType = typeof(ControlComponent);

    public override void Initialize()
    {
        base.Initialize();
        //gameScene.sceneEventManager.SubscribeEvent<MovementComponent, EventOnCollideArgs>(OnCollide);
    }

    public override void Update(GameTime gameTime)
    {
        MovementComponent[] movementComponents = GetAllComponents<MovementComponent>(gameScene);
        List<LocationComponent> locationComponentsList = new List<LocationComponent>();
        List<CollisionComponent> collisionComponentsList = new List<CollisionComponent>();

        for (int i = 0; i < movementComponents.Length; i++)
        {
            locationComponentsList.Add((LocationComponent)GetComponentOfEntityByType(movementComponents[i].entityOfComponent, locationComponentType));
            collisionComponentsList.Add((CollisionComponent)GetComponentOfEntityByType(movementComponents[i].entityOfComponent, collisionComponentType));
        }

        LocationComponent[] locationComponents = locationComponentsList.ToArray();
        CollisionComponent[] collisionComponents = collisionComponentsList.ToArray();

        for (int i = 0; i < movementComponents.Length; i++)
        {
            //ControlComponent inputComponent = (ControlComponent)GetComponentOfEntityByType(movementComponents[i].entityOfComponent, controlComponentType);

            //Debug.WriteLine(movementComponents[i].velocity.X + " " + -movementComponents[i].velocity.Y);

            Vector2 controlVelocity = new Vector2(0, 0);

            //if (inputComponent is not null)
            //{
            //    if (inputComponent.moveDirection.X != 0 || inputComponent.moveDirection.Y != 0)
            //    {
            //        Vector2 normalizedInput = Vector2.Normalize(inputComponent.moveDirection);
            //        controlVelocity = normalizedInput * movementComponents[i].movementSpeed;
            //    }
            //}

            Vector2 tempLoc = new Vector2(locationComponents[i].location.X, locationComponents[i].location.Y);

            locationComponents[i].location.X += movementComponents[i].velocity.X;// + controlVelocity.X;
                                                                                 //if (collisionComponents[i] is not null && CollisionSystem.IntersectWithAnything(collisionComponents[i], locationComponents[i], gameWorld)) 
                                                                                 //    locationComponents[i].location.X = tempLoc.X;


            locationComponents[i].location.Y += -movementComponents[i].velocity.Y;// - controlVelocity.Y;
            //if (collisionComponents[i] is not null && CollisionSystem.IntersectWithAnything(collisionComponents[i], locationComponents[i], gameWorld))
            //    locationComponents[i].location.Y = tempLoc.Y;


            //movementComponents[i].velocity.X = 0;
            //movementComponents[i].velocity.Y = 0;
        }
    }

    //void OnCollide(Entity entity, MovementComponent movementComponent, EventOnCollideArgs args)
    //{
    //    CollisionComponent cc1 = (CollisionComponent)GetComponentOfEntityByType(args.first, typeof(CollisionComponent));
    //    CollisionComponent cc2 = (CollisionComponent)GetComponentOfEntityByType(args.second, typeof(CollisionComponent));

    //    if (cc1.collisionType == CollisionInteractionType.SoftBlock && movementComponent is not null)
    //    {
    //        LocationComponent lc1 = (LocationComponent)GetComponentOfEntityByType(args.first, typeof(LocationComponent));
    //        LocationComponent lc2 = (LocationComponent)GetComponentOfEntityByType(args.second, typeof(LocationComponent));

    //        Vector2 v1 = lc1.location + new Vector2(cc1.collisionOffset.X, -cc1.collisionOffset.Y);
    //        Vector2 v2 = lc2.location + new Vector2(cc2.collisionOffset.X, -cc2.collisionOffset.Y);

    //        if (cc1.rectangleCollision.Width > 0 || cc1.rectangleCollision.Height > 0)
    //        {
    //            v1 = v1 + new Vector2(0, cc1.rectangleCollision.Height);
    //        }

    //        if (cc2.rectangleCollision.Width > 0 || cc2.rectangleCollision.Height > 0)
    //        {
    //            v2 = v2 + new Vector2(0, cc2.rectangleCollision.Height);
    //        }

    //        Vector2 direction = Vector2.Normalize(v1 - v2);
    //        MovementComponent movementComponentSecond = (MovementComponent)GetComponentOfEntityByType(args.second, typeof(MovementComponent));
    //        float m1 = movementComponent.mass;
    //        float m2 = (movementComponentSecond is null) ? float.MaxValue : movementComponentSecond.mass;

    //        float massRatio = Math.Clamp(m2 / m1, 0, 3.5f);
    //        movementComponent.velocity.X = movementComponent.velocity.X + direction.X * massRatio * 0.1f;
    //        movementComponent.velocity.Y = movementComponent.velocity.Y - direction.Y * massRatio * 0.1f;
    //    }
    //}
}
