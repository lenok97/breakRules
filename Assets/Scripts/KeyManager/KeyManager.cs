using System;
using UnityEngine;

namespace BreakRules
{
    internal class KeyManager : MonoBehaviour
    {
        private const string MoveKey = "Horizontal";

        public readonly InputHandler Handler = new InputHandler();

        private void Awake()
        {
            Func<Character> playerProvider = FindObjectOfType<Character>;
            Handler.AddCommand(MoveKey, new MoveKeyCommand(playerProvider));
            Handler.AddCommand(KeyCode.Space, KeyState.OnKeyDown, new JumpKeyCommand(playerProvider));
            Handler.AddCommand(KeyCode.LeftControl, KeyState.OnKeyDown, new ShootKeyCommand(playerProvider));
            Handler.AddCommand(KeyCode.Alpha1, KeyState.OnKeyDown, new ChangeGravityKeyCommand(playerProvider));
            Handler.AddCommand(KeyCode.Alpha2, KeyState.OnKeyDown, new DisableJumpKeyCommand(playerProvider));
            Handler.AddCommand(KeyCode.Alpha3, KeyState.OnKeyDown, new ActivateShellKeyCommand(playerProvider));
            Handler.AddCommand(KeyCode.Alpha4, KeyState.OnKeyDown, new DisableVisibleKeyCommand(playerProvider));
        }

        private void Update()
        {
            Handler.Handle();
        }
    }
}