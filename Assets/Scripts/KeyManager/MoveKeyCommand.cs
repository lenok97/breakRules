using System;

namespace BreakRules
{
    internal class MoveKeyCommand : IKeyCommand
    {
        private readonly Lazy<Character> player;

        public MoveKeyCommand(Func<Character> playerProvider)
        {
            player = new Lazy<Character>(playerProvider);
        }

        public void Execute(float value)
        {
            player.Value.Move(value);
        }
    }
}