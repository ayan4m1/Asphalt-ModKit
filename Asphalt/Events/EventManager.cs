/** 
 * ------------------------------------
 * Copyright (c) 2018 [Kronox]
 * See LICENSE file in the project root for full license information.
 * ------------------------------------
 * Created by Kronox on March 25, 2018
 * ------------------------------------
 **/

using System;
using System.Linq;
using System.Reflection;

namespace Asphalt.Events
{
    public static class EventManager
    {
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
                    throw new ArgumentException("Incorrect number of arguments in method with EventHandlerAttribute!");

                var parameterType = parameters[0].ParameterType;
            }
        }
    }
}
