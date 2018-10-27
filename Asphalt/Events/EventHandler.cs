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
    /// <summary>
    /// Indicates that an event binding should be established on the targeted method.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class EventHandler : Attribute
    {
        public bool AllowCancel { get; set; } = true;
        public EventPriority Priority { get; set; } = EventPriority.Normal;

        public EventHandler() { }

        public EventHandler(EventPriority priority)
        {
            Priority = priority;
        }

        public EventHandler(EventPriority priority, bool allowCancel)
        {
            Priority = priority;
            AllowCancel = allowCancel;
        }
    }
}
