using System;

namespace BreakRules
{
    internal class ShootKeyCommand : IKeyCommand
    {
        private readonly Lazy<Character> player;

        public ShootKeyCommand(Func<Character> playerProvider)
        {
            player = new Lazy<Character>(playerProvider);
        }

        public void Execute(float value)
        {
            player.Value.Shoot();
        }
    }
}