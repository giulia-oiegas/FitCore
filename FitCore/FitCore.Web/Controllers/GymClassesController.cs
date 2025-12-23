using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FitCore.Data;
using FitCore.Data.Models;

namespace FitCore.Web.Controllers
{
    //asta e adresa la care va apela mobilul
    [Route("api/[controller]")]
    [ApiController]

    public class GymClassesController : ControllerBase
    {
        private readonly FitCoreContext _context;
        public GymClassesController(FitCoreContext context)
        {
            _context = context;
        }
        // GET: api/GymClassesApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GymClass>>> GetGymClasses()
        {
            return await _context.GymClasses
                .Include(c => c.Trainer) //incarcam si antrenorii asociati fiecarei clase
                .ToListAsync();
        }
    }
}