using System;
using System.Collections.Generic;

namespace Helpers
{
    public interface IEvent<out T>
    {
        void Add(string    key, Action<T> action);
        void Remove(string key);
    }
    
    public class Event<T> : IEvent<T>
    {
        private readonly Dictionary<string, Action<T>> actions;
        private readonly string                        name;

        public Event(string name)
        {
            this.name = name;
            actions   = new Dictionary<string, Action<T>>();
        }

        public void Add(string key, Action<T> action)
        {
            actions[key] = action;
        }

        public void Remove(string key)
        {
            actions.Remove(key);
        }

        public void Invoke(T v)
        {
            foreach (var action in actions.Values)
                action?.Invoke(v);
        }
    }
}