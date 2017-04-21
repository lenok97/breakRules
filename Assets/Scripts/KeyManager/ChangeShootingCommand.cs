using System;

namespace BreakRules
{
    internal class ChangeShootingCommand : IKeyCommand
    {
        private readonly Lazy<Character> player;

        public ChangeShootingCommand(Func<Character> playerProvider)
        {
            player = new Lazy<Character>(playerProvider);
        }

        public void Execute(float value)
        {
            player.Value.ChangeShooting();
        }
    }
}
