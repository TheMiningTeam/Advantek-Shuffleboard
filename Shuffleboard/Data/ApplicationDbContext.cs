using Microsoft.EntityFrameworkCore;
using Shuffleboard.Models;

namespace Shuffleboard.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }

    public DbSet<Player> Players { get; set; }
    public DbSet<Player_Match> Player_Matches { get; set; }
    public DbSet<Match> Matches { get; set; }
}

