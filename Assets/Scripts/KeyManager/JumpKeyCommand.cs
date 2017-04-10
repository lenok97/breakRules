using System;

namespace BreakRules
{
    internal class JumpKeyCommand : IKeyCommand
    {
        private readonly Lazy<Character> player;

        public JumpKeyCommand(Func<Character> playerProvider)
        {
            player = new Lazy<Character>(playerProvider);
        }

        public void Execute(float value)
        {
            player.Value.Jump();
        }
    }
}
