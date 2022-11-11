using UnitTestingMinimalApi.Models;

namespace UnitTestingMinimalApi.Repositories
{
    public interface IPlayerRepository
    {
        public Task<List<Player>> GetAllAsync();
        public Task<Player?> GetByIdAsync(Guid id);
        public Task SignPlayer(Player player);
        public Task DeleteAsync(Guid id);
        public Task<int> SaveChanges();
    }
}
