/** 
 * ------------------------------------
 * Copyright (c) 2018 [Kronox]
 * See LICENSE file in the project root for full license information.
 * ------------------------------------
 * Created by Kronox on March 25, 2018
 * ------------------------------------
 **/

using Asphalt.Exceptions;
using Eco.Shared.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Asphalt.Events
{
    public static class EventManager
    {
        private static readonly EventHandlerComparer eventHandlerComparer = new EventHandlerComparer();

        //<type of parameter from registered event, List<registered event>>
        private static readonly Dictionary<Type, List<EventHandlerData>> handlers = new Dictionary<Type, List<EventHandlerData>>();

        private static object locker = new object();

        public static void RegisterListener(object pListener)
        {
            var methods = pListener.GetType().GetMethods(BindingFlags.Public | BindingFlags.Instance); //static ?!?

            foreach (var method in methods)
            {
                var attribute = method.GetCustomAttributes<EventHandlerAttribute>(false)?.FirstOrDefault();

                if (attribute == null)
                    continue;

                var parameters = method.GetParameters();
                if (parameters.Length != 1)
                    throw new EventHandlerArgumentException("Incorrect number of arguments in method with EventHandlerAttribute!");

                var parameterType = parameters[0].ParameterType;

                lock (locker)
                {
                    if (!handlers.ContainsKey(parameterType))
                    {
                        try
                        {
                            EventManagerHelper.injectEvent(parameterType);
                        }
                        catch (Exception e)
                        {
                            Log.WriteError(e.ToStringPretty());
#if DEBUG
                            throw;
#endif
                        }

                        handlers.Add(parameterType, new List<EventHandlerData>());
                    }

                    handlers[parameterType].Add(new EventHandlerData(pListener, method, attribute.Priority, attribute.RunIfEventCancelled));
                    handlers[parameterType].Sort(eventHandlerComparer);
                }
            }
        }

        public static void UnregisterListener(object pListener)
        {
            foreach (KeyValuePair<Type, List<EventHandlerData>> entry in handlers)
                entry.Value.RemoveAll(x => x.Listener.Equals(pListener));
        }
        
        public static void CallEvent(ref EventArgs pEvent)
        {
            if (!handlers.ContainsKey(pEvent.GetType()))
                return;

            var cancellable = pEvent as CancelEventArgs;
            foreach (var eventHandlerData in handlers[pEvent.GetType()])
            {
                try
                {
                    if (cancellable != null && cancellable.Cancel && !eventHandlerData.RunIfEventCancelled)
                        continue;

                    //Invoke EventHandler
                    eventHandlerData.Method.Invoke(eventHandlerData.Listener, new object[] { pEvent });
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
#if DEBUG
                    throw;
#endif
                }
            }
        }

    }
}
