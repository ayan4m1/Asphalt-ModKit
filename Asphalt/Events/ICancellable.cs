﻿/** 
 * ------------------------------------
 * Copyright (c) 2018 [Kronox]
 * See LICENSE file in the project root for full license information.
 * ------------------------------------
 * Created by Kronox on March 25, 2018
 * ------------------------------------
 **/

namespace Asphalt.Events
{
    public interface ICancellable
    {
        bool IsCancelled();

        void SetCancelled(bool cancel);
    }
}
