using FuwaCards.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FuwaCards.Pages
{
    public class singlesModel : PageModel
    {
		private readonly AppDataContext _context;
		public IList<PokemonSingles> PokemonSinglesList { get; set; }


		public singlesModel(AppDataContext context)
		{
			_context = context;
		}
		
		public void OnGet()
        {
			PokemonSinglesList = _context.PokemonSingles.ToList();
		}

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var pokemonSingle = await _context.PokemonSingles.FindAsync(id);
            if (pokemonSingle != null)
            {
                _context.PokemonSingles.Remove(pokemonSingle);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("/Index");
        }

    }
}
