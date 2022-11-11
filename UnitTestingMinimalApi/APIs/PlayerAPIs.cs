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
        public async Task<IResult> GetAll(IPlayerRepository repository)
        {
            return Results.Ok(await repository.GetAllAsync());
        }

        public async Task<IResult> GetById(IPlayerRepository repository, Guid id)
        {
            try
            {
                Player? player = await repository.GetByIdAsync(id);
                if (player == null) return Results.NotFound($"Player {id} do not exist");
                return Results.Ok(player);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Results.BadRequest("Error getting player...");
            }
        }

        public async Task<IResult> Post(IPlayerRepository repository, Player player)
        {
            try
            {
                if (player == null) return Results.BadRequest("Invalid request body...");
                Player results = await repository.AddAsync(player);
                return Results.Ok(new { PlayerId = results.ID });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Results.BadRequest("Error signing player...");
            }
        }

        public async Task<IResult> Delete(IPlayerRepository repository, Guid id)
        {
            Player? player = await repository.GetByIdAsync(id);
            if (player == null) return Results.NotFound();
            await repository.DeleteAsync(id);
            return Results.NoContent();
        }
    }
}
