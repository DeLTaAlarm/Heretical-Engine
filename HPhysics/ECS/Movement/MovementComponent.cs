using HDataCore;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPhysics.ECS.Movement;

public class MovementComponent : BaseComponent
{
    public Vector2 velocity;
    public float movementSpeed;
    public float mass;

    public MovementComponent(Vector2 _velocity, float speed = 1, float _mass = 1)
    {
        velocity = _velocity;
        movementSpeed = speed;
        mass = _mass;
    }
}
