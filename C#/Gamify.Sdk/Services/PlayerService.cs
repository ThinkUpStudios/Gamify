﻿using Gamify.Sdk.Data;
using Gamify.Sdk.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gamify.Sdk.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly IRepository<GamePlayer> playerRepository;

        public PlayerService(IRepository<GamePlayer> playerRepository)
        {
            this.playerRepository = playerRepository;
        }

        public IEnumerable<IGamePlayer> GetAll(string playerNameToExclude = null)
        {
            return this.playerRepository.GetAll(p => p.Name != playerNameToExclude);
        }

        public IGamePlayer GetByName(string playerName)
        {
            return this.playerRepository.Get(p => p.Name == playerName);
        }

        public IGamePlayer GetRandom(string playerNameToExclude = null)
        {
            return this.GetAll(playerNameToExclude)
                .OrderBy(p => Guid.NewGuid())
                .FirstOrDefault();
        }

        public bool Exist(string playerName)
        {
            return this.playerRepository.Exist(p => p.Name == playerName);
        }

        public void Create(string userName, string name)
        {
            if (this.playerRepository.Exist(p => p.Name == userName))
            {
                var errorMessage = string.Format("The player {0} cannot be created because it's already registered", userName);

                throw new GameDataException(errorMessage);
            }

            var newPlayer = new GamePlayer
            {
                Name = userName,
                DisplayName = name
            };

            this.playerRepository.Create(newPlayer);
        }
    }
}
