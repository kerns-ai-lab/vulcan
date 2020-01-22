using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vulcan.Models;
using Vulcan.Services;
using Moserware.Skills;

namespace Vulcan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchController : ControllerBase
    {
        private readonly PlayerService _playerService;
        private readonly GameService _gameService;

        public MatchController(PlayerService playerService, GameService gameService)
        {
            _playerService = playerService;
            _gameService = gameService;
        }
        
        // PUT: api/Skills/UpdateRatings
        [HttpPut("UpdateRatings")]
        public IActionResult Update(Match match)
        {
            // Lookup Gameinfo
            var game = _gameService.Get(match.GameId);

            if (game == null)
            {
                return NotFound();
            }

            GameInfo gameInfo = new GameInfo(game.InitialMean, game.InitialStd, game.Beta, game.DynamicsFactor, game.DrawProbability);

            Dictionary<string, APIPlayer> apiPlayers = new Dictionary<string, APIPlayer>();
            List<Team> moserTeams = new List<Team>();
            List<int> moserTeamRanks = new List<int>();
            match.Teams.ForEach(apiteam => {
                var moserTeam = new Team();
                apiteam.Players.ForEach(playerId => {
                    var apiplayer = _playerService.Get(playerId);
                    apiPlayers.Add(playerId, apiplayer);
                    moserTeam.AddPlayer(new Player(apiplayer.Id), new Rating(apiplayer.Rating.Mean, apiplayer.Rating.Std));
                });
                moserTeams.Add(moserTeam);
                moserTeamRanks.Add(apiteam.MatchRank);
            });
            
            var teams = Teams.Concat(moserTeams.ToArray());

            var newRatings = TrueSkillCalculator.CalculateNewRatings(gameInfo, teams, moserTeamRanks.ToArray());

            foreach(KeyValuePair<Player, Rating> newRating in newRatings)
            {
                APIPlayer player;
                apiPlayers.TryGetValue(newRating.Key.Id.ToString(), out player);
                player.Rating = new APIRating {
                    Mean = newRating.Value.Mean,
                    Std = newRating.Value.StandardDeviation,
                    Multiplier = newRating.Value.ConservativeRating
                };
                _playerService.Update(player.Id, player);
            }

            return NoContent();
        }

    }
}
