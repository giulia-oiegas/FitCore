using FitCore.Data;
using FitCore.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitCore.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MembersController : ControllerBase
    {
        private readonly FitCoreContext _context;

        public MembersController(FitCoreContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Member>> Get(int id)
        {
            var member = await _context.Members
                .Include(m => m.MembershipType)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (member == null)
                return NotFound();

            return member;
        }

        [HttpPost("select-membership")]
        public async Task<ActionResult<Member>> SelectMembership(
            int memberId,
            int membershipTypeId)
        {
            var member = await _context.Members
                .Include(m => m.MembershipType)
                .FirstOrDefaultAsync(m => m.Id == memberId);

            if (member == null)
                return NotFound();

            member.MembershipTypeId = membershipTypeId;
            member.MembershipStartDate = DateTime.Now;

            await _context.SaveChangesAsync();

            return await _context.Members
                .Include(m => m.MembershipType)
                .FirstAsync(m => m.Id == memberId);
        }
    }
}
