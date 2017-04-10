﻿using System;

namespace BreakRules
{
    internal class DisableVisibleKeyCommand : IKeyCommand
    {
        private readonly Lazy<Character> player;

        public DisableVisibleKeyCommand(Func<Character> playerProvider)
        {
            player = new Lazy<Character>(playerProvider);
        }

        public void Execute(float value)
        {
            player.Value.DisableVisible();
        }
    }
}