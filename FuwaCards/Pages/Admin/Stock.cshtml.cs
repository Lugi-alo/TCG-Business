using FuwaCards.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;
using System.Threading.Tasks;

namespace FuwaCards.Pages.Admin
{
    public class StockModel : PageModel
    {
        private readonly AppDataContext _context;

        public StockModel(AppDataContext context)
        {
            _context = context;
        }

        [BindProperty]
        public PokemonSingles PokemonSingles { get; set; }

        [BindProperty]
        public IFormFile ImageFile { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
          

            if (ImageFile != null)
            {
                var filePath = Path.Combine("wwwroot/images", ImageFile.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(stream);
                }
                PokemonSingles.Image = "/images/" + ImageFile.FileName;
            }

            _context.PokemonSingles.Add(PokemonSingles);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Index");
        }
    }
}

