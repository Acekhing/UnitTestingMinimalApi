using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using UnitTestingMinimalApi.Models;

namespace UnitTestingMinimalApi.Data
{
    public class PlayerContext : DbContext, IPlayerContext
    {
        public PlayerContext(DbContextOptions<PlayerContext> options) : base(options) { }

        public DbSet<Player> Players => Set<Player>();

    }
}
