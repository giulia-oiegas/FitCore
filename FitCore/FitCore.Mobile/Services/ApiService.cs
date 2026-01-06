using System.Net.Http.Json;
using FitCore.Data.Models;

namespace FitCore.Mobile.Services;

public class ApiService
{
    private readonly HttpClient _http;

    public ApiService(HttpClient http)
    {
        _http = http;
    }

    public Task<List<MembershipType>> GetMembershipTypes() =>
    _http.GetFromJsonAsync<List<MembershipType>>("api/MembershipTypes");

    public Task<List<GymClass>> GetClasses() =>
        _http.GetFromJsonAsync<List<GymClass>>("api/GymClasses");

    public Task<List<GymClass>> GetClassesByDate(DateTime? date = null)
    {
        var url = date == null
            ? "api/GymClasses/by-date"
            : $"api/GymClasses/by-date?date={date:yyyy-MM-dd}";

        return _http.GetFromJsonAsync<List<GymClass>>(url);
    }

    public Task<List<GymClass>> GetFutureClasses() =>
        _http.GetFromJsonAsync<List<GymClass>>("api/GymClasses/future");


    public Task<List<Booking>> GetMyBookings(int memberId) =>
        _http.GetFromJsonAsync<List<Booking>>($"api/Bookings/member/{memberId}");


    public Task BookClass(int gymClassId, int memberId) =>
        _http.PostAsJsonAsync("api/Bookings", new
        {
            GymClassId = gymClassId,
            MemberId = memberId
        });


    public async Task<Member> Login(string email, string password)
    {
        var response = await _http.PostAsJsonAsync(
            "api/Auth/login",
            new
            {
                Email = email,
                Password = password
            });

        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Member>();
    }

    public Task<Member> GetMember(int id) =>
    _http.GetFromJsonAsync<Member>($"api/Members/{id}");

    public async Task<Member> SelectMembership(int memberId, int membershipTypeId)
    {
        var response = await _http.PostAsync(
            $"api/Members/select-membership?memberId={memberId}&membershipTypeId={membershipTypeId}",
            null);

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<Member>();
    }




    public async Task<Member> Register(object data)
    {
        var response = await _http.PostAsJsonAsync(
            "api/Auth/register",
            data);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new Exception(error);
        }

        return await response.Content.ReadFromJsonAsync<Member>();
    }

    public async Task RemoveBooking(int bookingId)
    {
        var response = await _http.DeleteAsync($"api/Bookings/{bookingId}");
        response.EnsureSuccessStatusCode();
    }

}
