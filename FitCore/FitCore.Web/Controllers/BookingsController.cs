using Microsoft.EntityFrameworkCore;

using FitCore.Data;
using FitCore.Data.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class BookingsController : ControllerBase
{
    private readonly FitCoreContext _context;

    public BookingsController(FitCoreContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> BookClass(Booking booking)
    {
        bool alreadyBooked = await _context.Bookings.AnyAsync(b =>
            b.MemberId == booking.MemberId &&
            b.GymClassId == booking.GymClassId);

        if (alreadyBooked)
            return BadRequest("Clasa este deja rezervată");

        _context.Bookings.Add(booking);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpGet("member/{memberId}")]
    public async Task<List<Booking>> GetMyBookings(int memberId)
    {
        return await _context.Bookings
            .Where(b =>
                b.MemberId == memberId &&
                b.GymClass.Schedule > DateTime.Now)
            .Include(b => b.GymClass)
            .ThenInclude(c => c.Trainer)
            .OrderBy(b => b.GymClass.Schedule)
            .ToListAsync();
    }

    [HttpDelete("{bookingId}")]
    public async Task<IActionResult> RemoveBooking(int bookingId)
    {
        var booking = await _context.Bookings.FindAsync(bookingId);

        if (booking == null)
            return NotFound();

        _context.Bookings.Remove(booking);
        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpGet("by-date")]
    public async Task<List<GymClass>> GetClassesByDate(
    [FromQuery] DateTime? date)
    {
        var targetDate = date?.Date ?? DateTime.Today;
        var nextDay = targetDate.AddDays(1);

        return await _context.GymClasses
            .Include(c => c.Trainer)
            .Where(c =>
                c.Schedule >= targetDate &&
                c.Schedule < nextDay)
            .OrderBy(c => c.Schedule)
            .ToListAsync();
    }


    [HttpGet("future")]
    public async Task<List<GymClass>> GetFutureClasses()
    {
        return await _context.GymClasses
            .Include(c => c.Trainer)
            .Where(c => c.Schedule > DateTime.Now)
            .OrderBy(c => c.Schedule)
            .ToListAsync();
    }
}
