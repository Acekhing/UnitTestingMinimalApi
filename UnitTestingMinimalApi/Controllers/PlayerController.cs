using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using UnitTestingMinimalApi.Models;
using UnitTestingMinimalApi.Repositories;
using UnitTestingMinimalApi.Utils;

namespace UnitTestingMinimalApi.Controllers
{

    [ApiController]
    [Route("teams/1/players")]
    public class PlayerController : ControllerBase
    {
        private readonly IPlayerRepository _playerRepository;

        public PlayerController(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        [HttpPost]
        public async Task<IActionResult> SignPlayer(Player player)
        {
            try
            {
                if (player == null) throw new JsonException("Invalid Json");

                if (player.GetFullName() == null) throw new ArgumentNullException("Invalid name provided");

                if (!player.IsEliglePlayer()) throw new ArgumentException("Player must be between 15 and 20 years");

                if (!player.IsCitizen()) throw new ArgumentException("Country code can only be two letters eg: GH");

                if (player.IsEliglePlayer() && player.IsCitizen() && player.GetFullName() != null)
                {
                    await _playerRepository.SignPlayer(player);
                    return new CreatedAtRouteResult(nameof(GetPlayerById), player);
                }

                throw new NotImplementedException("Request could not be processed");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new BadRequestObjectResult(ex.Message);
            }

        }

        [HttpGet]
        public async Task<IActionResult> GetPlayers()
        {
            try
            {
                var players = await _playerRepository.GetAllAsync();
                if (players.Count() < 1) return new NoContentResult();
                return new OkObjectResult(players);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new BadRequestObjectResult("Something went wrong. Try again");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPlayerById(Guid id)
        {
            try
            {
                var player = await _playerRepository.GetByIdAsync(id);
                if (player == null) return new NotFoundResult();
                return new OkObjectResult(player);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new BadRequestObjectResult("Something went wrong. Try again");
            }
        }
    }
}
