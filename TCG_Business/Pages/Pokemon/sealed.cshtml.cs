using FuwaCards.Models;
using FuwaCards.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TCG_Business.ViewModels;

namespace TCG_Business.Pages.Pokemon
{
    public class sealedModel : PageModel
    {
        private readonly AppDataContext _context;

        [BindProperty(SupportsGet = true)]
        public PokemonSealedFilter Filter { get; set; } = new PokemonSealedFilter();

        private static PokemonSealedFilter PreviousFilter { get; set; } = new PokemonSealedFilter();
        public List<string> SetNameOptions { get; set; } = new List<string>();
        public List<string> TypeOptions { get; set; } = new List<string>();

        public PokemonSealed SelectedPokemonSealed { get; set; }

        public sealedModel(AppDataContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync(int? id)
        {
            var filterSetName = Request.Query["Filter.SetNameSelection"].ToString().Split(',').Where(s => !string.IsNullOrWhiteSpace(s)).ToList();
            var filterType = Request.Query["Filter.TypeSelection"].ToString().Split(',').Where(s => !string.IsNullOrWhiteSpace(s)).ToList();

            Filter.SetNameSelection = filterSetName;
            Filter.TypeSelection = filterType;
            Filter.SortOrder = Request.Query["Filter.SortOrder"];

            Filter.PageNumber = int.TryParse(Request.Query["Filter.PageNumber"], out var pageNumber) ? pageNumber : 1;
            Filter.PageSize = int.TryParse(Request.Query["Filter.PageSize"], out var pageSize) ? pageSize : 12;

            await LoadDataAsync();

            if (id.HasValue)
            {
                SelectedPokemonSealed = await _context.PokemonSealed.FindAsync(id.Value);
            }
        }

        private async Task LoadDataAsync()
        {
            IQueryable<PokemonSealed> query = _context.PokemonSealed;

            Filter.SetNameSelection ??= new List<string>();
            Filter.TypeSelection ??= new List<string>();

            if (Filter.SetNameSelection.Any())
            {
                query = query.Where(s => Filter.SetNameSelection.Contains(s.Set));
            }

            if (Filter.TypeSelection.Any())
            {
                query = query.Where(s => Filter.TypeSelection.Contains(s.Type));
            }

            if (Filter.SortOrder == "alphabetical")
            {
                query = query.OrderBy(s => s.Name);
            }
            else if (Filter.SortOrder == "highestPrice")
            {
                query = query.OrderByDescending(s => (double)s.Price);
            }
            else if (Filter.SortOrder == "lowestPrice")
            {
                query = query.OrderBy(s => (double)s.Price);
            }
            else if (Filter.SortOrder == "newlyListed")
            {
                query = query.OrderByDescending(s => s.Id);
            }
            else
            {
                query = query.OrderBy(s => s.Name);
            }


            Filter.TotalItems = await query.CountAsync();

            query = query
                .Skip((Filter.PageNumber - 1) * Filter.PageSize)
                .Take(Filter.PageSize);

            Filter.PokemonSealedList = await query.ToListAsync();

            var nameGroups = await _context.PokemonSealed
                .GroupBy(s => s.Set)
                .Select(g => new { Set = g.Key, Count = g.Count() })
                .OrderByDescending(g => g.Count)
                .ToListAsync();

            SetNameOptions = nameGroups
                .Select(g => g.Set)
                .ToList();

            Filter.SetNameCounts = nameGroups
                .ToDictionary(g => g.Set, g => g.Count);

            var typeGroups = await _context.PokemonSealed
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
