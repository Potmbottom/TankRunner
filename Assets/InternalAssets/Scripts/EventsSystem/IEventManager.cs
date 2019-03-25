using System;
using System.Collections;
using UnityEngine;

namespace Common.EventsSystem
{
    public interface IEventManager<TEventType> : IDisposable
        where TEventType : struct, IComparable, IConvertible, IFormattable
    {
        bool SaveAllEvents { get; set; }

        bool HasPersistentEvents { get; }

        void AddListener(TEventType eventName, Action handler);

        void AddListener<T>(TEventType eventName, Action<T> handler);

        void AddListener<T, U>(TEventType eventName, Action<T, U> handler);

        void AddListener<T, U, V>(TEventType eventName, Action<T, U, V> handler);

        void AddListener<T, U, V, S>(TEventType eventName, Action<T, U, V, S> handler);

        void RemoveListener(TEventType eventName, Action handler);

        void RemoveListener<T>(TEventType eventName, Action<T> handler);

        void RemoveListener<T, U>(TEventType eventName, Action<T, U> handler);

        void RemoveListener<T, U, V>(TEventType eventName, Action<T, U, V> handler);

        void RemoveListener<T, U, V, S>(TEventType eventName, Action<T, U, V, S> handler);

        bool HasListener(TEventType eventName, Action handler);

        bool HasListener<T>(TEventType eventName, Action<T> handler);

        bool HasListener<T, U>(TEventType eventName, Action<T, U> handler);

        bool HasListener<T, U, V>(TEventType eventName, Action<T, U, V> handler);

        void Broadcast(TEventType eventName);

        void Broadcast<T>(TEventType eventName, T arg1);

        void Broadcast<T, U>(TEventType eventName, T arg1, U arg2);

        void Broadcast<T, U, V>(TEventType eventName, T arg1, U arg2, V arg3);

        void Broadcast<T, U, V, S>(TEventType eventName, T arg1, U arg2, V arg3, S arg4);

        IEnumerator Broadcast(TEventType eventName, YieldInstruction[] delayInstrunctions);

        IEnumerator Broadcast<T>(TEventType eventName, T arg1, YieldInstruction[] delayInstrunctions);

        IEnumerator Broadcast<T, U>(TEventType eventName, T arg1, U arg2, YieldInstruction[] delayInstrunctions);

        IEnumerator Broadcast<T, U, V>(TEventType eventName, T arg1, U arg2, V arg3, YieldInstruction[] delayInstrunctions);
    }
}
