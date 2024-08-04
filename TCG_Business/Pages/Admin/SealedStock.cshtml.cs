using FuwaCards.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Threading.Tasks;

namespace FuwaCards.Pages.Admin
{
    public class SealedStockModel : PageModel
    {
        private readonly AppDataContext _context;

        public SealedStockModel(AppDataContext context)
        {
            _context = context;
        }

        [BindProperty]
        public PokemonSealed PokemonSealed { get; set; }

        [BindProperty]
        public IFormFile ImageFile { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id.HasValue)
            {
                PokemonSealed = await _context.PokemonSealed.FindAsync(id.Value);

                if (PokemonSealed == null)
                {
                    return NotFound();
                }
            }
            else
            {
                PokemonSealed = new PokemonSealed();
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
                PokemonSealed.Image = "/images/" + ImageFile.FileName;
            }

            if (PokemonSealed.Id == 0)
            {
                _context.PokemonSealed.Add(PokemonSealed);
            }
            else
            {
                var existingEntity = await _context.PokemonSingles.AsNoTracking().FirstOrDefaultAsync(p => p.Id == PokemonSealed.Id);
                if (existingEntity == null)
                {
                    return NotFound();
                }

                _context.PokemonSealed.Update(PokemonSealed);
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
