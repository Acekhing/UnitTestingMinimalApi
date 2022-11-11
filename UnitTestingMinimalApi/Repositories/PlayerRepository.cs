using Microsoft.EntityFrameworkCore;
using UnitTestingMinimalApi.Data;
using UnitTestingMinimalApi.Models;

namespace UnitTestingMinimalApi.Repositories
{
    public class PlayerRepository: IPlayerRepository
    {
        private readonly PlayerContext _playerContext;

        public PlayerRepository(PlayerContext playerContext)
        {
            _playerContext = playerContext;
        }

        public async Task<Player> AddAsync(Player player)
        {
            var result = await _playerContext.Players.AddAsync(player);
            await SaveChanges();
            return result.Entity;
        }

        public async Task DeleteAsync(Guid id)
        {
            Player? player = await _playerContext.Players.FindAsync(id);
            if (player != null) _playerContext.Players.Remove(player);
            await SaveChanges();
        }

        public async Task<List<Player>> GetAllAsync()
        {
            return await _playerContext.Players.ToListAsync();
        }

        public async Task<Player?> GetByIdAsync(Guid id)
        {
            Player? player = await _playerContext.Players.FindAsync(id);
            return player != null ? player : null;
        }

        public async Task<int> SaveChanges()
        {
            return await _playerContext.SaveChangesAsync();
        }
    }
}
