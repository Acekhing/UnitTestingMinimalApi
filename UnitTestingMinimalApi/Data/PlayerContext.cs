using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using UnitTestingMinimalApi.Models;

namespace UnitTestingMinimalApi.Data
{
    public class PlayerContext : DbContext, IPlayerContext
    {
        public PlayerContext(DbContextOptions<PlayerContext> options) : base(options) { }

        public DbSet<Player> Players => Set<Player>();

        public async Task<int> Delete(Guid id)
        {
            await this.Delete(id);
            await SaveChanges();
            return 1;
        }

        public async Task<Player?> Find(Guid id)
        {
            var result = await Players.FindAsync(id);
            return result;
        }

        public Task<List<Player>> Get()
        {
            //return this.Get(); ;
            return Players.ToListAsync(); ;
        }

        public async Task<Player?> Insert(Player player)
        {
            //var result = await this.AddAsync(player);
            var result = await Players.AddAsync(player);
            await SaveChanges();
            return result.Entity;
        }

        public async Task<int> SaveChanges()
        {
            return await this.SaveChangesAsync();
        }
    }
}
