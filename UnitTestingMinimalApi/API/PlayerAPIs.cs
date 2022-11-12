using Microsoft.AspNetCore.Mvc;
using UnitTestingMinimalApi.Models;
using UnitTestingMinimalApi.Repositories;

namespace UnitTestingMinimalApi.APIs
{
    public class PlayerAPIs
    {
        public void Register(WebApplication app)
        {
            app.MapGet("/", () => "Hello Manager!");
            app.MapGet("/api/team/players", GetAll);
            app.MapGet("/api/team/players/{id}", GetById);
            app.MapPost("/api/team/players", Post);
            app.MapDelete("/api/team/players/{id}", Delete);
        }

        // Request handler middlewares
        public async Task<IActionResult> GetAll(IPlayerRepository repository)
        {
            var result = await repository.GetAllAsync();
            if(result.Count == 0)
            {
                return new NoContentResult();
            }
            return new OkObjectResult(result);
        }

        public async Task<IActionResult> GetById(IPlayerRepository repository, Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    return new BadRequestObjectResult("Provide Id");
                }

                Player? player = await repository.GetByIdAsync(id);
                if (player == null) return new NotFoundObjectResult($"Player {id} do not exist");
                return new OkObjectResult(player);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new BadRequestObjectResult("Error getting player...");
            }
        }

        public async Task<IActionResult> Post(IPlayerRepository repository, Player player)
        {
            try
            {
                if (player == null) return new BadRequestObjectResult("Invalid request body...");
                await repository.SignPlayer(player);
                return new CreatedAtRouteResult(nameof(GetById), player);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new BadRequestObjectResult("Error signing player...");
            }
        }

        public async Task<IActionResult> Delete(IPlayerRepository repository, Guid id)
        {
            Player? player = await repository.GetByIdAsync(id);
            if (player == null) return new NotFoundObjectResult("Player do not exist");
            await repository.DeleteAsync(id);
            return new NoContentResult();
        }
    }
}
