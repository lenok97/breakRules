using System;

namespace BreakRules
{
    internal class BindedKeyCommand
    {
        private readonly IKeyCommand keyCommand;
        private readonly Func<float> valueProviderFunc;

        public BindedKeyCommand(Func<float> valueProviderFunc, IKeyCommand keyCommand)
        {
            this.keyCommand = keyCommand;
            this.valueProviderFunc = valueProviderFunc;
        }
        
        public void Execute()
        {
            var value = valueProviderFunc();
            if (Math.Abs(value) > 0.001)
            {
                keyCommand.Execute(value);
            }
        }
    }
}
