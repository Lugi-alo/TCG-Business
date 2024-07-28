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

            Filter.TotalItems = await query.CountAsync();

            query = query
                .OrderBy(s => s.Name)
                .Skip((Filter.PageNumber - 1) * Filter.PageSize)
                .Take(Filter.PageSize);

            Filter.PokemonSinglesList = await query.ToListAsync();

            var rarityGroups = await _context.PokemonSingles
                .GroupBy(s => s.Rarity)
                .Select(g => new { Rarity = g.Key, Count = g.Count() })
                .OrderByDescending(g => g.Count)
                .ToListAsync();

            RarityOptions = rarityGroups
                .Select(g => g.Rarity)
                .ToList();

            Filter.RarityCounts = rarityGroups
                .ToDictionary(g => g.Rarity, g => g.Count);

            var nameGroups = await _context.PokemonSingles
                .GroupBy(s => s.SetName)
                .Select(g => new { SetName = g.Key, Count = g.Count() })
                .OrderByDescending(g => g.Count)
                .ToListAsync();

            SetNameOptions = nameGroups
                .Select(g => g.SetName)
                .ToList();

            Filter.SetNameCounts = nameGroups
                .ToDictionary(g => g.SetName, g => g.Count);
        }

    }
}
