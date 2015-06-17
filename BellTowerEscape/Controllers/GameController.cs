﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BellTowerEscape.Commands;
using BellTowerEscape.Server;

namespace BellTowerEscape.Controllers
{
    public class GameController : ApiController
    {
        private readonly GameManager _gameManager;

        public GameController()
        {
            _gameManager = GameManager.Instance;
        }

        /// <summary>
        /// Gets the list of current games
        /// </summary>
        /// <returns></returns>
        [Route("api/game")]
        public IList<Game> Get()
        {

            return _gameManager.Games.Values.Where(g => g.Running).ToList();
        }

        /// <summary>
        /// Initiates an agent logon with the simulation server by name. Once an agent is logged on, 
        /// a logon result is returned with the id and starting time of the next game.
        /// </summary>
        /// <param name="agentName"></param>
        /// <returns>LogonResult</returns>
        [HttpPost]
        [Route("api/game/logon")]
        public LogonResult Logon(LogonCommand logon)
        {
            if (IsValidLogonRequest(logon))
            {
                return _gameManager.Execute(logon);
            }
            return null;
        }
        [HttpPost]
        [Route("api/game/move")]
        public MoveResult Move(MoveCommand move)
        {
            if (IsValidMoveRequest(move))
            {
                return _gameManager.Execute(move);
            }
            return null;
        }

        [HttpPost]
        [Route("api/game/{id}/status/{authToken}")]
        public StatusResult Status(StatusCommand status, int id, string authToken)
        {
            if (ISValidStatusRequest(status))
            {
                return _gameManager.Execute(status);
            }
            return null;
        }

        private bool ISValidStatusRequest(StatusCommand status)
        {
            throw new NotImplementedException();
        }


        private bool IsValidMoveRequest(MoveCommand move)
        {
            throw new NotImplementedException();
        }

        private bool IsValidLogonRequest(LogonCommand logon)
        {
            if (logon != null && !string.IsNullOrWhiteSpace(logon.AgentName))
            {
                return true;
            }
            return false;

        }
    }
}
