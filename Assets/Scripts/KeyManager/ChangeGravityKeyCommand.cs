using System;

namespace BreakRules
{
    internal class ChangeGravityKeyCommand : IKeyCommand
    {
        private readonly Lazy<Character> player;

        public ChangeGravityKeyCommand(Func<Character> playerProvider)
        {
            player = new Lazy<Character>(playerProvider);
        }

        public void Execute(float value)
        {
            player.Value.ChangeGravity();
        }
    }
}