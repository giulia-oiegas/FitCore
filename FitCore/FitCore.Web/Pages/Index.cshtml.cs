using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FitCore.Data;
using Microsoft.EntityFrameworkCore;

namespace FitCore.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly FitCoreContext _context;

        public IndexModel(FitCoreContext context)
        {
            _context = context;
        }

        //variabile pt stats
        public int TotalMembers { get; set; }
        public int TotalTrainers { get; set; }
        public int TotalClasses { get; set; }

        public async Task OnGetAsync()
        {
            //numaram inregistrarile din bd
            TotalMembers = await _context.Members.CountAsync();
            TotalTrainers = await _context.Trainers.CountAsync();
            TotalClasses = await _context.GymClasses.CountAsync();
        }
    }
}
