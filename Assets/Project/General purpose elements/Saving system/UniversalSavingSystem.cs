using System;

namespace Project
{
    public class UniversalSavingSystem<T> : BinaryFormatterSavingSystem<T>
    {
        private readonly Func<T> _factory;

        public UniversalSavingSystem(Func<T> emptyInstanceFactory)
        {
            _factory = emptyInstanceFactory;
        }

        protected override T CreateEmptyDataInstance() =>
            _factory();
    }
}