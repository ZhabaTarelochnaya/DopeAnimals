using System;

namespace BaCon
{
    public abstract class DIEntry : IDisposable
    {
        protected DIEntry()
        {
        }

        protected DIEntry(DIContainer container)
        {
            Container = container;
        }
        protected DIContainer Container { get; }
        protected bool IsSingleton { get; set; }

        public abstract void Dispose();

        public T Resolve<T>() => ((DIEntry<T>)this).Resolve();

        public DIEntry AsSingle()
        {
            IsSingleton = true;

            return this;
        }
    }

    public class DIEntry<T> : DIEntry
    {
        IDisposable _disposableInstance;
        T _instance;

        public DIEntry(DIContainer container, Func<DIContainer, T> factory) : base(container)
        {
            Factory = factory;
        }

        public DIEntry(T value)
        {
            _instance = value;

            if (_instance is IDisposable disposableInstance)
            {
                _disposableInstance = disposableInstance;
            }

            IsSingleton = true;
        }
        Func<DIContainer, T> Factory { get; }

        public T Resolve()
        {
            if (IsSingleton)
            {
                if (_instance == null)
                {
                    _instance = Factory(Container);

                    if (_instance is IDisposable disposableInstance)
                    {
                        _disposableInstance = disposableInstance;
                    }
                }

                return _instance;
            }

            return Factory(Container);
        }

        public override void Dispose()
        {
            _disposableInstance?.Dispose();
        }
    }
}