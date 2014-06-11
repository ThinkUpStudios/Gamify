﻿using Gamify.Sdk.Contracts.Notifications;
using Gamify.Sdk.Contracts.Requests;
using Gamify.Sdk.Services;
using Gamify.Sdk.Setup.Definition;

namespace Gamify.Sdk.Components
{
    public class AcceptGameComponent : GameComponent
    {
        private readonly ISessionService sessionService;
        private readonly ISessionPlayerSetup playerSetup;
        private readonly ISerializer serializer;

        public AcceptGameComponent(ISessionService sessionService, INotificationService notificationService,
            ISessionPlayerSetup playerSetup, ISerializer serializer)
            : base(notificationService)
        {
            this.sessionService = sessionService;
            this.playerSetup = playerSetup;
            this.serializer = serializer;
        }

        public override bool CanHandleRequest(GameRequest request)
        {
            return request.Type == (int)GameRequestType.GameAccepted;
        }

        public override bool CanHandleNotification(GameNotification notification)
        {
            return notification.Type == (int)GameNotificationType.GameCreated;
        }

        public override void HandleRequest(GameRequest request)
        {
            var gameAcceptedObject = this.serializer.Deserialize<GameAcceptedRequestObject>(request.SerializedRequestObject);
            var pendingSession = this.sessionService.GetByName(gameAcceptedObject.SessionName);

            this.playerSetup.GetPlayerReady(gameAcceptedObject, pendingSession.Player2);

            pendingSession.Player1.PendingToMove = true;

            this.sessionService.Start(pendingSession);

            this.SendGameCreatedNotification(pendingSession);
        }

        private void SendGameCreatedNotification(IGameSession newSession)
        {
            var notification = new GameCreatedNotificationObject
            {
                SessionName = newSession.Name,
                Player1Name = newSession.Player1Name,
                Player2Name = newSession.Player2Name
            };

            this.notificationService.SendBroadcast(GameNotificationType.GameCreated, notification, newSession.Player1Name, newSession.Player2Name);
        }
    }
}
