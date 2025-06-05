using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using HSystemsCore;
using static SceneDataInterface.ECSManager;

namespace Input.ECS.Control
{
    [SystemsOrder("Input")]
    public class ControlSystem : BaseProcessingSystem
    {
        private List<Keys> pressedKeys = new List<Keys>();
        private List<Keys> lastPressedKeys = new List<Keys>();

        public override void Initialize()
        {
            //foreach (Controller c in gameWorld.gameInstance.controllers) c.controlSystem = this;
        }

        public override void Update(GameTime gameTime) 
        {
            ControlComponent[] components = GetAllComponents<ControlComponent>(gameScene);

            KeyboardState keyboardState = Keyboard.GetState();
            Keys[] currentPressedKeys = keyboardState.GetPressedKeys();

            MouseState mouseState = Mouse.GetState();

            List<Keys> keysToPress = new List<Keys>();
            List<Keys> keysToRelease = new List<Keys>();

            foreach (Keys key in currentPressedKeys)
            {
                if (!pressedKeys.Contains(key))
                {
                    pressedKeys.Add(key);
                    keysToPress.Add(key);
                }
            }

            foreach (Keys key in lastPressedKeys)
            {
                if (!currentPressedKeys.Contains(key))
                {
                    pressedKeys.Remove(key);
                    keysToRelease.Add(key);
                }
            }

            for (int i = 0; i < components.Length; i++)
            {
                foreach (Keys key in keysToPress)
                    RaiseEvent(components[i].entityOfComponent, new EventOnKeyboardInteractionArgs(key, KeyState.Down));

                foreach (Keys key in keysToRelease)
                    RaiseEvent(components[i].entityOfComponent, new EventOnKeyboardInteractionArgs(key, KeyState.Up));
            }

            lastPressedKeys = new List<Keys>(currentPressedKeys);
        }
    }
}
