using Microsoft.EntityFrameworkCore;
using UnitTestingMinimalApi.Models;

namespace UnitTestingMinimalApi.Data
{
    public interface IPlayerContext
    {
        DbSet<Player> Players { get; }
    }
}