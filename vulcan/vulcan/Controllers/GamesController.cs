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

        private readonly GameService _gamesService;

        public GamesController(GameService gamesService)
        {
            _gamesService = gamesService;
        }

        [HttpGet]
        public ActionResult<List<Game>> Get() => _gamesService.Get();

        [HttpGet("{id:length(24)}", Name = "GetGame")]
        public ActionResult<Game> Get(string id)
        {
            var game = _gamesService.Get(id);

            if (game == null)
            {
                return NotFound();
            }

            return game;
        }

        [HttpPost]
        public ActionResult<Game> Create(Game game)
        {
            _gamesService.Create(game);
            return CreatedAtRoute("GetGame", new { id = game.Id.ToString(), game });
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Game gameIn)
        {
            var game = _gamesService.Get(id);

            if (game == null)
            {
                return NotFound();
            }

            _gamesService.Update(id, gameIn);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var game = _gamesService.Get(id);
            if (game == null)
            {
                return NotFound();
            }

            _gamesService.Remove(game.Id);

            return NoContent();
        }
    }
}