using FuwaCards.Models;
using FuwaCards.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FuwaCards.Pages
{
    public class singlesModel : PageModel
    {
        private readonly AppDataContext _context;

        [BindProperty(SupportsGet = true)]
        public PokemonSinglesFilter Filter { get; set; } = new PokemonSinglesFilter();

        public List<string> RarityOptions { get; set; } = new List<string>();
        public List<string> SetNameOptions { get; set; } = new List<string>();


        public singlesModel(AppDataContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync()
        {
            await LoadDataAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await LoadDataAsync();
            return Page();
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

        private async Task LoadDataAsync()
        {
            IQueryable<PokemonSingles> query = _context.PokemonSingles;

            if (Filter.RaritySelection != null && Filter.RaritySelection.Any())
            {
                query = query.Where(s => Filter.RaritySelection.Contains(s.Rarity));
            }

            if (Filter.SetNameSelection != null && Filter.SetNameSelection.Any())
            {
                query = query.Where(s => Filter.SetNameSelection.Contains(s.SetName));
            }

            if (Filter.MinimumPriceFilter.HasValue)
            {
                query = query.Where(s => s.Price >= Filter.MinimumPriceFilter.Value);
            }

            if (Filter.MaximumPriceFilter.HasValue)
            {
                query = query.Where(s => s.Price <= Filter.MaximumPriceFilter.Value);
            }

            query = query.OrderBy(s => s.Name);
            Filter.PokemonSinglesList = await query.ToListAsync();

            RarityOptions = await _context.PokemonSingles
                .Select(s => s.Rarity)
                .Distinct()
                .OrderBy(s => s)
                .ToListAsync();

            Filter.RarityCounts = await _context.PokemonSingles
                .GroupBy(s => s.Rarity)
                .Select(g => new { Rarity = g.Key, Count = g.Count() })
                .ToDictionaryAsync(g => g.Rarity, g => g.Count);

            SetNameOptions = await _context.PokemonSingles
                .Select(s => s.SetName)
                .Distinct()
                .OrderBy(s => s)
                .ToListAsync();

            Filter.SetNameCounts = await _context.PokemonSingles
                .GroupBy (s => s.SetName)
                .Select(g => new { SetName = g.Key, Count = g.Count() })
                .ToDictionaryAsync(g => g.SetName, g => g.Count);
        }

    }
}
