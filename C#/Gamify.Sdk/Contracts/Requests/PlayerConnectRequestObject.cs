﻿namespace Gamify.Contracts.Requests
{
    public class PlayerConnectRequestObject : IRequestObject
    {
        public string PlayerName { get; set; }

        public string AccessToken { get; set; }
    }
}