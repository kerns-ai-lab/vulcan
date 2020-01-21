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
            // 
            Player p1 = new Player("user1");
            Player p2 = new Player("user2");

            GameInfo gameInfo = GameInfo.DefaultGameInfo;

            var t1_players = new Player[] { };
            // For team in teams
            //      Construct team object
            //      for player in players
            //          Add player to team object
            //
            var t1 = new Team()
                .AddPlayer(p1, new Rating(25, 7));

            var t2 = new Team()
                .AddPlayer(p2, new Rating(25, 7));

            var teams = Teams.Concat(t1, t2);

            var newRatings = TrueSkillCalculator.CalculateNewRatings(gameInfo, teams, 1, 2);

            return NoContent();
        }

    }
}
