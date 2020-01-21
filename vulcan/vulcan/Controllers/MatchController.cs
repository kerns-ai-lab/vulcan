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

        public MatchController(PlayerService playerService)
        {
            _playerService = playerService;
        }
        
        // PUT: api/Skills/UpdateRatings
        [HttpPut("UpdateRatings")]
        public IActionResult Update(Match match)
        {
            // Lookup Gameinfo
            // ... = _matchService.Get(match.GameId);
            GameInfo gameInfo = GameInfo.DefaultGameInfo;

            List<Team> moserTeams = new List<Team>();
            List<int> moserTeamRanks = new List<int>();
            match.Teams.ForEach(apiteam => {
                var moserTeam = new Team();
                apiteam.Players.ForEach(playerId => {
                    var apiplayer = _playerService.Get(playerId);
                    moserTeam.AddPlayer(new Player(apiplayer.Id), new Rating(apiplayer.Rating.Mean, apiplayer.Rating.Std));
                });
                moserTeams.Add(moserTeam);
                moserTeamRanks.Add(apiteam.MatchRank);
            });
            
            var teams = Teams.Concat(moserTeams.ToArray());

            var newRatings = TrueSkillCalculator.CalculateNewRatings(gameInfo, teams, moserTeamRanks.ToArray());


            foreach(KeyValuePair<Player, Rating> newRating in newRatings)
            {
                var player = new APIPlayer();
                player.Id = newRating.Key.Id.ToString();
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
