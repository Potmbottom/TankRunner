using System;

namespace Common.EventsSystem
{
    public sealed class EventHolder<TEventType>
        where TEventType : struct, IComparable, IConvertible, IFormattable
    {
        private static IEventManager<TEventType> dispatcher = new EventManager<TEventType>();

        public static IEventManager<TEventType> Dispatcher
        {
            get { return dispatcher; }
        }

        public static void Clear()
        {
            dispatcher = new EventManager<TEventType>();
        }
    }
}
