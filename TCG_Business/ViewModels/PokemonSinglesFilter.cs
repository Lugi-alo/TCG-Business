using FuwaCards.Models;
namespace FuwaCards.ViewModels
{
    public class PokemonSinglesFilter
    {
        public string NameFilter { get; set; }
        public Dictionary<string, int> RarityCounts { get; set; }
        public Dictionary<string, int> SetNameCounts { get; set; }
        public Dictionary<string, int> TypeCounts { get; set; }

        public List<string> RaritySelection { get; set; }
        public List<string> SetNameSelection { get; set; }
        public List<string> TypeSelection { get; set; }

        public decimal? MinimumPriceFilter { get; set; }
        public decimal? MaximumPriceFilter { get;set; }
        public List<PokemonSingles> PokemonSinglesList { get; set; }
        public int PageNumber { get; set; } 
        public int PageSize { get; set; } = 12;
        public int TotalItems { get; set; }
    }
}
