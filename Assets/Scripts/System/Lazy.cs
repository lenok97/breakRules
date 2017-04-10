using System;

namespace BreakRules
{
    internal class Lazy<T>
        where T: class 
    {
        private T value;
        private readonly Func<T> valueProvider;

        public T Value
        {
            get
            {
                if (value == null)
                {
                    value = valueProvider();
                }
                return value;
            }
        }

        public Lazy(Func<T> valueProvider)
        {
            this.valueProvider = valueProvider;
        }

        public static implicit operator T(Lazy<T> lazy)
        {
            return lazy.Value;
        }
    }
}