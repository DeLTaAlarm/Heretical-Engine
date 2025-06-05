using Microsoft.Xna.Framework;
using HSystemsCore;
using static SceneDataInterface.ECSManager;
using HDataCore;
using System.Linq;

namespace HPhysics.ECS.Collision.CollisionProcessing;

[SystemsOrder("Physics", 2)]
public class CollisionProcessingSystem : BaseProcessingSystem
{
    public override void Initialize()
    {
        base.Initialize();
    }

    public override void Update(GameTime gameTime)
    {
        CollisionsSceneDataContainer collisionsContainer = GetSceneDataContainer<CollisionsSceneDataContainer>(gameScene);

        (Entity, Entity)[] ee = collisionsContainer.collisionOriginTargetPairs.ToArray();

        for (int i = 0; i < ee.Length; i++)
        {
            gameScene.SceneEventManager.RaiseEvent(ee[i].Item1, new EventOnCollideArgs(ee[i].Item1, ee[i].Item2));
        }
        collisionsContainer.collisionOriginTargetPairs.Clear();
    }
}