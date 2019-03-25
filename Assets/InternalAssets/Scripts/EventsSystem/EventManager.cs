using System;
using UnityEngine;
using System.Collections;

namespace Common.EventsSystem
{
    public sealed class EventManager<TEventType> : IEventManager<TEventType> 
        where TEventType : struct, IComparable, IConvertible, IFormattable
    {
        #region Fields

        private int numOfEventsOfThisType;
        private Delegate[] eventHandlers;
        private Func<object, int> ConvertEnumToInt;
        private bool saveAllEvents;
        private bool hasPersistentEvents;

        #endregion

        #region Properties

        public bool SaveAllEvents
        {
            get { return saveAllEvents; }
            set { saveAllEvents = value; }
        }

        public bool HasPersistentEvents
        {
            get { return hasPersistentEvents; }
        }

        #endregion
        
        #region Constructors
        
        public EventManager()
        {
            if (!typeof (TEventType).IsEnum)
                throw new InvalidGenericTypeException("TEventType for generic class 'Event Manager' must be an enum!");

            hasPersistentEvents = false;
            saveAllEvents = false;
            ConvertEnumToInt = Convert.ToInt32;
            numOfEventsOfThisType = Enum.GetNames(typeof (TEventType)).Length;
            eventHandlers = new Delegate[numOfEventsOfThisType];
        }

        #endregion

        #region Add Listener

        private void OnListenerAdding(int eventID, Delegate listenerBeingAdded)
        {
            Delegate tempDel = eventHandlers[eventID];
            if (tempDel != null && tempDel.GetType() != listenerBeingAdded.GetType())
            {
                throw new ListenerException(
                    string.Format(
                        "Attempting to add listener with inconsistent signature for event type {0}. Current listeners have type {1} and listener being added has type {2}",
                        (TEventType) (object) eventID, tempDel.GetType().Name, listenerBeingAdded.GetType().Name));
            }
        }

        private void OnListenerRemoving(int eventID, Delegate listenerBeingRemoved)
        {
            Delegate tempDel = eventHandlers[eventID];
            if (tempDel == null)
            {
            }
            else if (tempDel.GetType() != listenerBeingRemoved.GetType())
            {
            }
        }

        public void AddListener(TEventType eventName, Action handler)
        {
            int eventID = ConvertEnumToInt(eventName);
            OnListenerAdding(eventID, handler);
            eventHandlers[eventID] = (Action) eventHandlers[eventID] + handler;
        }

        public void AddListener<T>(TEventType eventName, Action<T> handler)
        {
            int eventID = ConvertEnumToInt(eventName);
            OnListenerAdding(eventID, handler);
            eventHandlers[eventID] = (Action<T>) eventHandlers[eventID] + handler;
        }

        public void AddListener<T, U>(TEventType eventName, Action<T, U> handler)
        {
            int eventID = ConvertEnumToInt(eventName);
            OnListenerAdding(eventID, handler);
            eventHandlers[eventID] = (Action<T, U>) eventHandlers[eventID] + handler;
        }

        public void AddListener<T, U, V>(TEventType eventName, Action<T, U, V> handler)
        {
            int eventID = ConvertEnumToInt(eventName);
            OnListenerAdding(eventID, handler);
            eventHandlers[eventID] = (Action<T, U, V>) eventHandlers[eventID] + handler;
        }

        public void AddListener<T, U, V, S>(TEventType eventName, Action<T, U, V, S> handler)
        {
            int eventID = ConvertEnumToInt(eventName);
            OnListenerAdding(eventID, handler);
            eventHandlers[eventID] = (Action<T, U, V, S>)eventHandlers[eventID] + handler;
        }

        #endregion

        #region Remove Listener
        
        public void RemoveListener(TEventType eventName, Action handler)
        {
            int eventID = ConvertEnumToInt(eventName);
            OnListenerRemoving(eventID, handler);
            eventHandlers[eventID] = (Action) eventHandlers[eventID] - handler;
        }

        public void RemoveListener<T>(TEventType eventName, Action<T> handler)
        {
            int eventID = ConvertEnumToInt(eventName);
            OnListenerRemoving(eventID, handler);
            eventHandlers[eventID] = (Action<T>) eventHandlers[eventID] - handler;
        }

        public void RemoveListener<T, U>(TEventType eventName, Action<T, U> handler)
        {
            int eventID = ConvertEnumToInt(eventName);
            OnListenerRemoving(eventID, handler);
            eventHandlers[eventID] = (Action<T, U>) eventHandlers[eventID] - handler;
        }

        public void RemoveListener<T, U, V>(TEventType eventName, Action<T, U, V> handler)
        {
            int eventID = ConvertEnumToInt(eventName);
            OnListenerRemoving(eventID, handler);
            eventHandlers[eventID] = (Action<T, U, V>) eventHandlers[eventID] - handler;
        }

        public void RemoveListener<T, U, V, S>(TEventType eventName, Action<T, U, V, S> handler)
        {
            int eventID = ConvertEnumToInt(eventName);
            OnListenerRemoving(eventID, handler);
            eventHandlers[eventID] = (Action<T, U, V, S>)eventHandlers[eventID] - handler;
        }

        public bool HasListener(TEventType eventName, Action handler)
        {
            return CheckListener(eventName, handler.GetType());
        }

        public bool HasListener<T>(TEventType eventName, Action<T> handler)
        {
            return CheckListener(eventName, handler.GetType());
        }

        public bool HasListener<T, U>(TEventType eventName, Action<T, U> handler)
        {
            return CheckListener(eventName, handler.GetType());
        }

        public bool HasListener<T, U, V>(TEventType eventName, Action<T, U, V> handler)
        {
            return CheckListener(eventName, handler.GetType());
        }

        public bool HasListener<T, U, V, S>(TEventType eventName, Action<T, U, V, S> handler)
        {
            return CheckListener(eventName, handler.GetType());
        }

        private bool CheckListener(TEventType eventName, Type handlerType)
        {
            int eventID = ConvertEnumToInt(eventName);
            Delegate tempDel = eventHandlers[eventID];
            if (tempDel == null)
                return false;

            return (tempDel.GetType() == handlerType);
        }
        
        #endregion

        #region Broadcast
        
        private static BroadcastException CreateBroadcastSignatureException(int eventID)
        {
            return
                new BroadcastException(
                    string.Format(
                        "Broadcasting message for event \"{0}\" but listeners have a different signature than the broadcaster.",
                        (TEventType) (object) eventID));
        }

        public void Broadcast(TEventType eventName)
        {
            int eventID = ConvertEnumToInt(eventName);

            Delegate tempDel = eventHandlers[eventID];
            if (tempDel != null)
            {
                Action broadcastEvent = tempDel as Action;

                if (broadcastEvent != null)
                {
                    broadcastEvent();
                }
                else
                {
                    throw CreateBroadcastSignatureException(eventID);
                }
            }
        }

        public void Broadcast<T>(TEventType eventName, T arg1)
        {
            int eventID = ConvertEnumToInt(eventName);
            Delegate tempDel = eventHandlers[eventID];
            if (tempDel != null)
            {
                Action<T> broadcastEvent = tempDel as Action<T>;

                if (broadcastEvent != null)
                {
                    broadcastEvent(arg1);
                }
                else
                {
                    throw CreateBroadcastSignatureException(eventID);
                }
            }
        }

        public void Broadcast<T, U>(TEventType eventName, T arg1, U arg2)
        {
            int eventID = ConvertEnumToInt(eventName);
            Delegate tempDel = eventHandlers[eventID];
            if (tempDel != null)
            {
                Action<T, U> broadcastEvent = tempDel as Action<T, U>;

                if (broadcastEvent != null)
                {
                    broadcastEvent(arg1, arg2);
                }
                else
                {
                    throw CreateBroadcastSignatureException(eventID);
                }
            }
        }

        public void Broadcast<T, U, V>(TEventType eventName, T arg1, U arg2, V arg3)
        {
            int eventID = ConvertEnumToInt(eventName);
            Delegate tempDel = eventHandlers[eventID];
            if (tempDel != null)
            {
                Action<T, U, V> broadcastEvent = tempDel as Action<T, U, V>;

                if (broadcastEvent != null)
                {
                    broadcastEvent(arg1, arg2, arg3);
                }
                else
                {
                    throw CreateBroadcastSignatureException(eventID);
                }
            }
        }

        public void Broadcast<T, U, V, S>(TEventType eventName, T arg1, U arg2, V arg3, S arg4)
        {
            int eventID = ConvertEnumToInt(eventName);
            Delegate tempDel = eventHandlers[eventID];
            if (tempDel != null)
            {
                Action<T, U, V, S> broadcastEvent = tempDel as Action<T, U, V, S>;

                if (broadcastEvent != null)
                {
                    broadcastEvent(arg1, arg2, arg3, arg4);
                }
                else
                {
                    throw CreateBroadcastSignatureException(eventID);
                }
            }
        }

        public IEnumerator Broadcast(TEventType eventName, YieldInstruction[] delayInstrunctions)
        {
            int eventID = ConvertEnumToInt(eventName);
            Delegate tempDel = eventHandlers[eventID];
            if (tempDel != null)
            {
                Action broadcastEvent = tempDel as Action;

                if (broadcastEvent != null)
                {
                    foreach (YieldInstruction instruction in delayInstrunctions)
                        yield return instruction;
                    broadcastEvent();
                }
                else
                {
                    throw CreateBroadcastSignatureException(eventID);
                }
            }
        }

        public IEnumerator Broadcast<T>(TEventType eventName, T arg1, YieldInstruction[] delayInstrunctions)
        {
            int eventID = ConvertEnumToInt(eventName);
            Delegate tempDel = eventHandlers[eventID];
            if (tempDel != null)
            {
                Action<T> broadcastEvent = tempDel as Action<T>;

                if (broadcastEvent != null)
                {
                    foreach (YieldInstruction instruction in delayInstrunctions)
                        yield return instruction;
                    broadcastEvent(arg1);
                }
                else
                {
                    throw CreateBroadcastSignatureException(eventID);
                }
            }
        }

        public IEnumerator Broadcast<T, U>(TEventType eventName, T arg1, U arg2, YieldInstruction[] delayInstrunctions)
        {
            int eventID = ConvertEnumToInt(eventName);
            Delegate tempDel = eventHandlers[eventID];
            if (tempDel != null)
            {
                Action<T, U> broadcastEvent = tempDel as Action<T, U>;

                if (broadcastEvent != null)
                {
                    foreach (YieldInstruction instruction in delayInstrunctions)
                        yield return instruction;
                    broadcastEvent(arg1, arg2);
                }
                else
                {
                    throw CreateBroadcastSignatureException(eventID);
                }
            }
        }

        public IEnumerator Broadcast<T, U, V>(TEventType eventName, T arg1, U arg2, V arg3, YieldInstruction[] delayInstrunctions)
        {
            int eventID = ConvertEnumToInt(eventName);
            Delegate tempDel = eventHandlers[eventID];
            if (tempDel != null)
            {
                Action<T, U, V> broadcastEvent = tempDel as Action<T, U, V>;

                if (broadcastEvent != null)
                {
                    foreach (YieldInstruction instruction in delayInstrunctions)
                        yield return instruction;
                    broadcastEvent(arg1, arg2, arg3);
                }
                else
                {
                    throw CreateBroadcastSignatureException(eventID);
                }
            }
        }

        #endregion

        #region IDisposable

        void IDisposable.Dispose()
        {
            eventHandlers = new Delegate[numOfEventsOfThisType];
        }

        #endregion
    }
}