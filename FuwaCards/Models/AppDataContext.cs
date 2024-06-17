using Microsoft.EntityFrameworkCore;

namespace FuwaCards.Models
{
	public class AppDataContext : DbContext
	{
		public AppDataContext(DbContextOptions < AppDataContext> options) : base(options) { }
		public DbSet<PokemonSingles> PokemonSingles { get; set; }
	}
}
