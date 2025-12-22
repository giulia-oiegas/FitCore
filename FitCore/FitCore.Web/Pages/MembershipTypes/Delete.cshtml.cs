using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FitCore.Data;
using FitCore.Data.Models;

namespace FitCore.Web.Pages.MembershipTypes
{
    public class DeleteModel : PageModel
    {
        private readonly FitCore.Data.FitCoreContext _context;

        public DeleteModel(FitCore.Data.FitCoreContext context)
        {
            _context = context;
        }

        [BindProperty]
        public MembershipType MembershipType { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var membershiptype = await _context.MembershipTypes.FirstOrDefaultAsync(m => m.Id == id);

            if (membershiptype == null)
            {
                return NotFound();
            }
            else
            {
                MembershipType = membershiptype;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var membershiptype = await _context.MembershipTypes.FindAsync(id);
            if (membershiptype != null)
            {
                MembershipType = membershiptype;
                _context.MembershipTypes.Remove(MembershipType);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
