using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vulcan.Models;
using Vulcan.Services;

namespace Vulcan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {

        private readonly GameService _gameService;
        private readonly PlayerService _playerService;

        public GamesController(GameService gameService, PlayerService playerService)
        {
            _gameService = gameService;
            _playerService = playerService;
            
        }


        [HttpGet("leaderboard/{id:length(24)}", Name ="Leaderboard")]
        public ActionResult<List<APIPlayer>> Leaderboard(string id)
        {
            var game = _gameService.Get(id);
            if (game == null)
            {
                return NotFound();
            }

            // Do logic to correlate game to player list
            // ...

            var players = _playerService.Get();

            if (players == null)
            {
                return NotFound();
            }

            return players.OrderByDescending(player => player.Rating.Mean).ToList();
        }

        [HttpGet]
        public ActionResult<List<Game>> Get() => _gameService.Get();

        [HttpGet("{id:length(24)}", Name = "GetGame")]
        public ActionResult<Game> Get(string id)
        {
            var game = _gameService.Get(id);

            if (game == null)
            {
                return NotFound();
            }

            return game;
        }

        [HttpPost]
        public ActionResult<Game> Create(Game game)
        {
            _gameService.Create(game);
            return CreatedAtRoute("GetGame", new { id = game.Id.ToString(), game });
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Game gameIn)
        {
            var game = _gameService.Get(id);

            if (game == null)
            {
                return NotFound();
            }

            _gameService.Update(id, gameIn);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var game = _gameService.Get(id);
            if (game == null)
            {
                return NotFound();
            }

            _gameService.Remove(game.Id);

            return NoContent();
        }
    }
}