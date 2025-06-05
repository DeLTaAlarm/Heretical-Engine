using Microsoft.Xna.Framework.Input;
using EventsSystem.EventManage;

namespace Input.ECS.Control
{
    public class EventOnKeyboardInteractionArgs : EntityEventArgs
    {
        public Keys key;
        public KeyState state;

        public EventOnKeyboardInteractionArgs(Keys key, KeyState state)
        {
            this.key = key;
            this.state = state;
        }
    }
}
