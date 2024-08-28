using FuwaCards.Models;

namespace TCG_Business.ViewModels
{
    public class PokemonSealedFilter
    {
        public string NameFilter { get; set; }
        public Dictionary<string, int> SetNameCounts { get; set; }
        public Dictionary<string, int> TypeCounts { get; set; }

        public string SortOrder { get; set; }
        public List<string> SetNameSelection { get; set; }
        public List<string> TypeSelection { get; set; }

        public decimal? MinimumPriceFilter { get; set; }
        public decimal? MaximumPriceFilter { get; set; }
        public List<PokemonSealed> PokemonSealedList { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; } = 12;
        public int TotalItems { get; set; }
    }
}
