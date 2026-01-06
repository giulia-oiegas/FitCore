using FitCore.Data.Models;
using System.Text.Json;

public class GymClassesApiService
{
    private readonly HttpClient _http;

    public GymClassesApiService()
    {
        _http = new HttpClient
        {
            BaseAddress = new Uri("https://10.0.2.2:7132/")
        };
    }

    public async Task<List<GymClass>> GetClassesAsync()
    {
        var response = await _http.GetAsync("api/GymClasses");
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<List<GymClass>>(json,
            new JsonSerializerOptions {
                PropertyNameCaseInsensitive = true
            });
    }
}
