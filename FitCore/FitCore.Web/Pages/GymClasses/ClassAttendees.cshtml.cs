using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FitCore.Data;
using FitCore.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace FitCore.Web.Pages.GymClasses
{
    public class ClassAttendeesModel : PageModel
    {
        private readonly FitCoreContext _context;

        public ClassAttendeesModel(FitCoreContext context)
        {
            _context = context;
        }

        //lista de rezervari pe care o vom afisa
        public IList<Booking> AttendeesList { get; set; } = default!;

        //numele clasei (ca sa il punem in titlu)
        public String ClassName { get; set; }

        public async Task OnGetAsync(int? id)
        {
            if (id == null) return;

            //aflam numele clasei pt titlu
            var gymClass = await _context.GymClasses.FirstOrDefaultAsync(c => c.Id == id);
            if (gymClass != null)
            {
                ClassName = gymClass.Name;
            }

            //luam toate rezervarile pentru clasa cu id-ul dat, incluzand si datele membrilor
            AttendeesList = await _context.Bookings
                .Where(b => b.GymClassId == id)
                .Include(b => b.Member)
                .ToListAsync();
        }
    }
}
