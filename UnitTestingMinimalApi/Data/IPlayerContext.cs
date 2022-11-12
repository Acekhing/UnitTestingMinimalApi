using Microsoft.EntityFrameworkCore;
using UnitTestingMinimalApi.Models;

namespace UnitTestingMinimalApi.Data
{

    /* This Interface abstract the CRUD functionality of a typical DbContext class */
    public interface IPlayerContext
    {
        public Task<Player?> Insert(Player player);
        public Task<List<Player>> Get();
        public Task<Player?> Find(Guid id);
        public Task<int> SaveChanges();
        public Task<int> Delete(Guid id);
    }
}