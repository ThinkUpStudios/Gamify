﻿namespace Gamify.Sdk
{
    public interface ISessionGamePlayerBase
    {
        IGamePlayer Information { get; set; }

        bool IsPlaying { get; set; }

        bool NeedsToMove { get; set; }
    }

    public interface ISessionGamePlayer<TMove, UResponse> : ISessionGamePlayerBase
    {
        IGameMoveResponse<UResponse> ProcessMove(IGameMove<TMove> move);
    }
}