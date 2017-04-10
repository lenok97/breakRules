using System;

namespace BreakRules
{
    internal class DisableJumpKeyCommand : IKeyCommand
    {
        private readonly Lazy<Character> player;

        public DisableJumpKeyCommand(Func<Character> playerProvider)
        {
            player = new Lazy<Character>(playerProvider);
        }

        public void Execute(float value)
        {
            player.Value.DisableJump();
        }
    }
}