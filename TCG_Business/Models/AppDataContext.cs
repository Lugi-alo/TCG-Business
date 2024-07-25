using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FuwaCards.Models
{
	public class AppDataContext : IdentityDbContext<AppUser>
	{
		public AppDataContext(DbContextOptions <AppDataContext> options) : base(options) { }
		public DbSet<PokemonSingles> PokemonSingles { get; set; }
		public DbSet<PokemonSealed> PokemonSealed {get; set; }
	}
}
