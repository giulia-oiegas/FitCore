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
    public class IndexModel : PageModel
    {
        private readonly FitCore.Data.FitCoreContext _context;

        public IndexModel(FitCore.Data.FitCoreContext context)
        {
            _context = context;
        }

        public IList<MembershipType> MembershipType { get;set; } = default!;

        public async Task OnGetAsync()
        {
            MembershipType = await _context.MembershipTypes.ToListAsync();
        }
    }
}
