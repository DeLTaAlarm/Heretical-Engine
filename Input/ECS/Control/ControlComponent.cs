using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HDataCore;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Input.ECS.Control
{
    public class ControlComponent : BaseComponent
    {
        //public Vector2 lastFrameMoveDirection = new Vector2(0, 0);
        //public Vector2 moveDirection = new Vector2(0, 0);
        public List<Keys> pressedKeys = new List<Keys>();
        //public Controller controller = null;
    }
}
