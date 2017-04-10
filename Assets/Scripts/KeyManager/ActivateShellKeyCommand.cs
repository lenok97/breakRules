using System;

namespace BreakRules
{
    internal class ActivateShellKeyCommand : IKeyCommand
    {
        private readonly Lazy<Character> player;

        public ActivateShellKeyCommand(Func<Character> playerProvider)
        {
            player = new Lazy<Character>(playerProvider);
        }

        public void Execute(float value)
        {
            player.Value.ActivateShell();
        }
    }
}
