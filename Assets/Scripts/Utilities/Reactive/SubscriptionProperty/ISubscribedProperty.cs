using System;

namespace Utilities.Reactive.SubscriptionProperty
{
    public interface ISubscribedProperty<out TValue>
    {
        TValue Value { get; }
        void Subscribe(Action<TValue> onChangeCallback);
        void Unsubscribe(Action<TValue> onChangeCallback);
    }
}