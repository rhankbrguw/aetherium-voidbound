using System;
using System.Collections.Generic;

namespace Aetherium.Core.Events
{
    public static class EventBus
    {
        private static readonly Dictionary<Type, List<Delegate>> subscribers = new Dictionary<Type, List<Delegate>>();

        public static void Subscribe<T>(Action<T> listener) where T : struct
        {
            Type eventType = typeof(T);
            if (!subscribers.TryGetValue(eventType, out List<Delegate> delegateList))
            {
                delegateList = new List<Delegate>();
                subscribers[eventType] = delegateList;
            }
            delegateList.Add(listener);
        }

        public static void Unsubscribe<T>(Action<T> listener) where T : struct
        {
            Type eventType = typeof(T);
            if (subscribers.TryGetValue(eventType, out List<Delegate> delegateList))
            {
                delegateList.Remove(listener);
            }
        }

        public static void Raise<T>(T eventData) where T : struct
        {
            Type eventType = typeof(T);
            if (!subscribers.TryGetValue(eventType, out List<Delegate> delegateList))
            {
                return;
            }

            for (int i = delegateList.Count - 1; i >= 0; i--)
            {
                if (delegateList[i] is Action<T> action)
                {
                    action.Invoke(eventData);
                }
            }
        }

        public static void Clear()
        {
            subscribers.Clear();
        }
    }
}
