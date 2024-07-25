using FuwaCards.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id.HasValue)
            {
                PokemonSingles = await _context.PokemonSingles.FindAsync(id.Value);

                if (PokemonSingles == null)
                {
                    return NotFound();
                }
            }
            else
            {
                PokemonSingles = new PokemonSingles();
            }

            return Page();
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

            if (PokemonSingles.Id == 0)
            {
                _context.PokemonSingles.Add(PokemonSingles);
            }
            else
            {
                var existingEntity = await _context.PokemonSingles.AsNoTracking().FirstOrDefaultAsync(p => p.Id == PokemonSingles.Id);
                if (existingEntity == null)
                {
                    return NotFound();
                }

                _context.PokemonSingles.Update(PokemonSingles);
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while saving the changes. Please try again.");
                return Page();
            }

            return RedirectToPage("/Index");
        }
    }
}
