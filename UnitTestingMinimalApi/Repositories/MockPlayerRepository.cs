using UnitTestingMinimalApi.Models;

namespace UnitTestingMinimalApi.Repositories
{
    public class MockPlayerRepository : IPlayerRepository
    {
        private List<Player> _players;

        public MockPlayerRepository()
        {
            _players = new List<Player>();
        }

        public async Task SignPlayer(Player player)
        {
            _players.Add(player);
        }

        public async Task DeleteAsync(Guid id)
        {
            _players.Remove(_players.Find(p => p.ID == id));
        }

        public async Task<List<Player>> GetAllAsync()
        {
            return _players;
        }

        public async Task<Player?> GetByIdAsync(Guid id)
        {
            return _players.Find(p => p.ID == id);
        }

        public async Task<int> SaveChanges()
        {
            return 1;
        }
    }
}
