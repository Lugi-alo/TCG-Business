using FuwaCards.Models;
namespace FuwaCards.ViewModels
{
    public class PokemonSinglesFilter
    {
        public string NameFilter { get; set; }
        public List<string> RaritySelection { get; set; }
        public List<string> SetNameSelection { get; set; }
        public decimal? MinimumPriceFilter { get; set; }
        public decimal? MaximumPriceFilter { get;set; }
        public List<PokemonSingles> PokemonSinglesList { get; set; }


    }
}
