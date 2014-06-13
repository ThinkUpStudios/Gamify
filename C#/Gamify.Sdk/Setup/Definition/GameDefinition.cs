﻿using Gamify.Sdk.Components;
using System.Collections.Generic;

namespace Gamify.Sdk.Setup.Definition
{
    public abstract class GameDefinition<TMove, UResponse> : IGameDefinition<TMove, UResponse>
    {
        public virtual ISessionPlayerFactory GetSessionPlayerFactory()
        {
            return new DefaultSessionPlayerFactory();
        }

        public virtual ISessionPlayerSetup GetSessionPlayerSetup()
        {
            return new NullSessionPlayerSetup();
        }

        public abstract IMoveFactory<TMove> GetMoveFactory();

        public abstract IMoveProcessor<TMove, UResponse> GetMoveProcessor();

        public abstract IMoveResultNotificationFactory GetMoveResultNotificationFactory();

        public virtual IGameInviteDecorator GetGameInviteDecorator()
        {
            return new NullGameInviteDecorator();
        }

        public abstract IGameInformationNotificationFactory<TMove, UResponse> GetGameInformationNotificationFactory();

        public abstract IPlayerHistoryItemFactory<TMove, UResponse> GetPlayerHistoryItemfactory();

        public virtual IEnumerable<IGameComponent> GetCustomComponents()
        {
            return new List<IGameComponent>();
        }
    }
}