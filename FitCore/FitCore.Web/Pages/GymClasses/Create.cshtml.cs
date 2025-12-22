using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using FitCore.Data;
using FitCore.Data.Models;

namespace FitCore.Web.Pages.GymClasses
{
    public class CreateModel : PageModel
    {
        private readonly FitCore.Data.FitCoreContext _context;

        public CreateModel(FitCore.Data.FitCoreContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["TrainerId"] = new SelectList(_context.Trainers, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public GymClass GymClass { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.GymClasses.Add(GymClass);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
