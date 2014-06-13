﻿using Gamify.Sdk.Contracts.Notifications;
using Gamify.Sdk.Contracts.Requests;
using Gamify.Sdk.Services;
using Gamify.Sdk.Setup.Definition;

namespace Gamify.Sdk.Components
{
    public class CreateGameComponent : GameComponent
    {
        private readonly IPlayerService playerService;
        private readonly ISessionService sessionService;
        private readonly ISessionPlayerFactory sessionPlayerFactory;
        private readonly ISessionPlayerSetup playerSetup;
        private readonly IGameInviteDecorator gameInviteDecorator;
        private readonly ISerializer serializer;

        public CreateGameComponent(IPlayerService playerService, ISessionService sessionService, 
            INotificationService notificationService, ISessionPlayerFactory sessionPlayerFactory, 
            ISessionPlayerSetup playerSetup, IGameInviteDecorator gameInviteDecorator, ISerializer serializer)
            : base(notificationService)
        {
            this.playerService = playerService;
            this.sessionService = sessionService;
            this.sessionPlayerFactory = sessionPlayerFactory;
            this.playerSetup = playerSetup;
            this.gameInviteDecorator = gameInviteDecorator;
            this.serializer = serializer;
        }

        public override bool CanHandleRequest(GameRequest request)
        {
            return request.Type == (int)GameRequestType.CreateGame;
        }

        public override bool CanHandleNotification(GameNotification notification)
        {
            return notification.Type == (int)GameNotificationType.GameInvite;
        }

        public override void HandleRequest(GameRequest request)
        {
            var createGameObject = this.serializer.Deserialize<CreateGameRequestObject>(request.SerializedRequestObject);
            var connectedPlayer1 = this.playerService.GetByName(createGameObject.PlayerName);
            var sessionPlayer1 = this.sessionPlayerFactory.Create(connectedPlayer1);
            var connectedPlayer2 = this.playerService.GetByName(createGameObject.InvitedPlayerName);
            var sessionPlayer2 = this.sessionPlayerFactory.Create(connectedPlayer2);

            this.playerSetup.GetPlayerReady(createGameObject, sessionPlayer1);

            var newSession = this.sessionService.Create(sessionPlayer1, sessionPlayer2);

            this.SendGameInviteNotification(newSession);
        }

        private void SendGameInviteNotification(IGameSession newSession)
        {
            var gameInviteNotificationObject = new GameInviteNotificationObject
            {
                SessionName = newSession.Name,
                Player1Name = newSession.Player1Name
            };

            this.gameInviteDecorator.Decorate(gameInviteNotificationObject, newSession);

            this.notificationService.Send(GameNotificationType.GameInvite, gameInviteNotificationObject, newSession.Player2Name);
        }
    }
}
