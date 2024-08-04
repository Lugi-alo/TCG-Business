using FuwaCards.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TCG_Business.Pages.Pokemon
{
    public class sealedModel : PageModel
    {
        private readonly AppDataContext _context;

        public List<PokemonSealed> PokemonSealedList { get; set; }

        public sealedModel(AppDataContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync()
        {
            PokemonSealedList = await _context.PokemonSealed.ToListAsync();
        }
    }
}



