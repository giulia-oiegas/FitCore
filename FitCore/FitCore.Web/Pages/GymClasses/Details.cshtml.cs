using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FitCore.Data;
using FitCore.Data.Models;

namespace FitCore.Web.Pages.GymClasses
{
    public class DetailsModel : PageModel
    {
        private readonly FitCore.Data.FitCoreContext _context;

        public DetailsModel(FitCore.Data.FitCoreContext context)
        {
            _context = context;
        }

        public GymClass GymClass { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gymclass = await _context.GymClasses.FirstOrDefaultAsync(m => m.Id == id);
            if (gymclass == null)
            {
                return NotFound();
            }
            else
            {
                GymClass = gymclass;
            }
            return Page();
        }
    }
}
