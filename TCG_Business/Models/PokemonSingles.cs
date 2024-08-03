using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FuwaCards.Models
{
	public class PokemonSingles
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Rarity { get; set; }
		public string SetName { get; set; }
		public string SetNumber { get; set; }
		public string Type { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        public string Condition { get; set; }
		public string Image { get; set; }
		public string AltTex { get; set; }
		public int Quantity { get; set; }

	}
}
