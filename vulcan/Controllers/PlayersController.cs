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
    public class PlayersController : ControllerBase
    {

        private readonly PlayerService _playerService;

        public PlayersController(PlayerService playerService)
        {
            _playerService = playerService;
        }

        [HttpGet]
        public ActionResult<List<APIPlayer>> Get() => _playerService.Get(); 

        [HttpGet("{id:length(24)}", Name = "GetPlayer")]
        public ActionResult<APIPlayer> Get(string id)
        {
            var player = _playerService.Get(id);

            if (player == null)
            {
                return NotFound();
            }

            return player;
        }

        [HttpPost]
        public ActionResult<APIPlayer> Create(APIPlayer player)
        {
            _playerService.Create(player);

            return CreatedAtRoute("GetPlayer", new { id = player.Id.ToString() }, player);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, APIPlayer playerIn)
        {
            var player = _playerService.Get(id);

            if (player == null)
            {
                return NotFound();
            }

            _playerService.Update(id, playerIn);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var player = _playerService.Get(id);
            if (player == null)
            {
                return NotFound();
            }

            _playerService.Remove(player.Id);

            return NoContent();
        }
        
    }
}