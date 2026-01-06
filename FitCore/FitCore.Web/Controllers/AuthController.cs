namespace FitCore.Web.Controllers;

using FitCore.Data;
using FitCore.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

[ApiController]
[Route("api/[controller]")]
[AllowAnonymous]
public class AuthController : ControllerBase
{
    private readonly FitCoreContext _context;
    private readonly UserManager<IdentityUser> _userManager;

    public AuthController(
        FitCoreContext context,
        UserManager<IdentityUser> userManager
    ) {
        _context = context;
        _userManager = userManager;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Email) ||
            string.IsNullOrWhiteSpace(request.Password))
            return BadRequest("Email și parola sunt obligatorii");

        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user == null)
            return Unauthorized("Email sau parolă greșită");

        var isValid = await _userManager.CheckPasswordAsync(user, request.Password);

        if (!isValid)
            return Unauthorized("Email sau parolă greșită");

        var member = await _context.Members
            .Include(m => m.MembershipType)
            .FirstOrDefaultAsync(m => m.Email == request.Email);

        if (member == null)
            return Unauthorized("Cont invalid");

        return Ok(member);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest dto) {
        var user = new IdentityUser
        {
            UserName = dto.Email,
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber
        };

        var result = await _userManager.CreateAsync(user, dto.Password);

        if (!result.Succeeded)
            return BadRequest(result.Errors);

        var member = new Member
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber
        };

        _context.Members.Add(member);
        await _context.SaveChangesAsync(); 

        return Ok(member);
    }

    [HttpPost("select-membership")]
    public async Task<IActionResult> SelectMembership(int memberId, int membershipTypeId)
    {
        var member = await _context.Members.FindAsync(memberId);
        if (member == null) return NotFound();

        member.MembershipTypeId = membershipTypeId;
        member.MembershipStartDate = DateTime.Now;

        await _context.SaveChangesAsync();
        return Ok();
    }


}

public class LoginRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
}

public class RegisterRequest
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
}