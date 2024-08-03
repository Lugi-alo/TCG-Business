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
        public List<string> TypeOptions { get; set; } = new List<string>();

        public PokemonSingles SelectedPokemonSingle { get; set; }

        public singlesModel(AppDataContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync(int? id)
        {
            await LoadDataAsync();

            if (id.HasValue)
            {
                SelectedPokemonSingle = await _context.PokemonSingles.FindAsync(id.Value);
            }
        }

        private async Task LoadDataAsync()
        {
            IQueryable<PokemonSingles> query = _context.PokemonSingles;

            // Apply filters based on the Filter properties
            if (Filter.RaritySelection != null && Filter.RaritySelection.Any())
            {
                query = query.Where(s => Filter.RaritySelection.Contains(s.Rarity));
            }

            if (Filter.SetNameSelection != null && Filter.SetNameSelection.Any())
            {
                query = query.Where(s => Filter.SetNameSelection.Contains(s.SetName));
            }

            if (Filter.TypeSelection != null && Filter.TypeSelection.Any())
            {
                query = query.Where(s => Filter.TypeSelection.Contains(s.Type));
            }

            if (Filter.MinimumPriceFilter.HasValue)
            {
                query = query.Where(s => s.Price >= Filter.MinimumPriceFilter.Value);
            }

            if (Filter.MaximumPriceFilter.HasValue)
            {
                query = query.Where(s => s.Price <= Filter.MaximumPriceFilter.Value);
            }

            // Calculate total items for pagination
            Filter.TotalItems = await query.CountAsync();

            // Apply pagination
            query = query
                .OrderBy(s => s.Name)
                .Skip((Filter.PageNumber - 1) * Filter.PageSize)
                .Take(Filter.PageSize);

            Filter.PokemonSinglesList = await query.ToListAsync();

            // Load filter options
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

            var typeGroups = await _context.PokemonSingles
                .GroupBy(s => s.Type)
                .Select(g => new { Type = g.Key, Count = g.Count() })
                .OrderByDescending(g => g.Count)
                .ToListAsync();

            TypeOptions = typeGroups
                .Select(g => g.Type)
                .ToList();

            Filter.TypeCounts = typeGroups
                .ToDictionary(g => g.Type, g => g.Count);
        }
    }
}
