/** 
 * ------------------------------------
 * Copyright (c) 2018 [Kronox]
 * See LICENSE file in the project root for full license information.
 * ------------------------------------
 * Created by Kronox on March 25, 2018
 * ------------------------------------
 **/

using System;

namespace Asphalt.Events
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class EventHandlerAttribute : Attribute
    {
        public bool AllowCancel { get; set; } = true;
        public EventPriority Priority { get; set; } = EventPriority.Normal;

        public EventHandlerAttribute() { }

        public EventHandlerAttribute(EventPriority priority)
        {
            Priority = priority;
        }

        public EventHandlerAttribute(EventPriority priority, bool allowCancel)
        {
            Priority = priority;
            AllowCancel = allowCancel;
        }
    }
}
