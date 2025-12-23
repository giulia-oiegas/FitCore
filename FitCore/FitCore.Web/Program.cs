using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
//adaugam conexiunea la baza de date
builder.Services.AddDbContext<FitCore.Data.FitCoreContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("FitCoreContext") ?? throw new InvalidOperationException("Connection string 'FitCoreContext' not found.")));
// Add services to the container.
builder.Services.AddRazorPages();

//adaugam suport pt api si ignoram buclele infinite (ex: relatia many to many antrenor <-> clasa)
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {     
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.MapControllers();

app.Run();
