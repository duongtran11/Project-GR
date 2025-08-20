using System;
using System.Collections.Generic;

public class StateFactory<Agent> where Agent : class
{
    private Dictionary<Type, StateBase<Agent>> _stateCache = new();
    public T GetOrCreate<T>(params object[] args) where T : StateBase<Agent>
    {
        if (_stateCache.TryGetValue(typeof(T), out var state))
        {
            return (T)state;
        }
        else
        {
            return (T)Activator.CreateInstance(typeof(T), args);
        }
    }
}