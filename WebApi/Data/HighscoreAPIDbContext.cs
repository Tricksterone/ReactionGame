using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Data
{
    public class HighscoreAPIDbContext : DbContext
    {
        public HighscoreAPIDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Highscore> Highscores { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source=Highscores.db");
        }
    }
}