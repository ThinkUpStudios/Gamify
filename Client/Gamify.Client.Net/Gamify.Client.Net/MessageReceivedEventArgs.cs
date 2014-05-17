﻿using Gamify.Contracts.Notifications;
using System;

namespace Gamify.Client.Net
{
    public class MessageReceivedEventArgs : EventArgs
    {
        public GameNotification GameNotification { get; private set; }

        public MessageReceivedEventArgs(GameNotification notificationObject)
        {
            this.GameNotification = notificationObject;
        }
    }
}
