using Microsoft.EntityFrameworkCore;
using UnitTestingMinimalApi.Data;
using UnitTestingMinimalApi.Models;

namespace UnitTestingMinimalApi.Repositories
{
    public class PlayerRepository: IPlayerRepository
    {
        private readonly IPlayerContext _playerContext;

        public PlayerRepository(IPlayerContext playerContext)
        {
            _playerContext = playerContext;
        }

        public async Task SignPlayer(Player player)
        {
            await _playerContext.Insert(player);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _playerContext.Delete(id);
        }

        public async Task<List<Player>> GetAllAsync()
        {
            var players = await _playerContext.Get();
            if(players.Count() == 0)
            {
                // Returns empty list
                return new List<Player>();
            }
            // Returns players
            return players;
        }

        public async Task<Player?> GetByIdAsync(Guid id)
        {
            Player? player = await _playerContext.Find(id);
            
            if(player == null)
            {
                return null;
            }
            return player;
        }

        public async Task<int> SaveChanges()
        {
            await _playerContext.SaveChanges();
            return 1;
        }
    }
}
