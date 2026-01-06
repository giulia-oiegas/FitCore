using FitCore.Data;
using FitCore.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class MembershipTypesController : ControllerBase
{
    private readonly FitCoreContext _context;

    public MembershipTypesController(FitCoreContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<List<MembershipType>> GetAll()
    {
        return await _context.MembershipTypes.ToListAsync();
    }
}
