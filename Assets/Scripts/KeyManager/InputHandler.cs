using System;
using System.Collections.Generic;
using UnityEngine;

namespace BreakRules
{
    internal class InputHandler
    {

        private readonly List<BindedKeyCommand> commands = new List<BindedKeyCommand>();

        public void AddCommand(KeyCode code, KeyState state, IKeyCommand keyCommand)
        {
            AddCommand(() => GetKey(code, state), keyCommand);
        }
        
        public void AddCommand(string axisName, IKeyCommand keyCommand)
        {
            AddCommand(() => Input.GetAxis(axisName), keyCommand);
        }

        public void AddCommand(Func<float> canExecuteFunc, IKeyCommand keyCommand)
        {
            commands.Add(new BindedKeyCommand(canExecuteFunc, keyCommand));
        }

        public void Handle()
        {
            foreach (var command in commands)
            {
                command.Execute();
            }
        }

        private static float GetKey(KeyCode code, KeyState state)
        {
            return ((state & KeyState.OnPress) != 0 && Input.GetKey(code)) ||
                   ((state & KeyState.OnKeyDown) != 0 && Input.GetKeyDown(code)) ||
                   ((state & KeyState.OnKeyUp) != 0 && Input.GetKeyUp(code))
                   ? 1 : 0;
        }
    }
}