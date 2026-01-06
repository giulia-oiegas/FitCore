using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
//adaugam conexiunea la baza de date
var connectionString = builder.Configuration.GetConnectionString("FitCoreContext") ?? throw new InvalidOperationException("Connection string 'FitCoreContext' not found.");

builder.Services.AddDbContext<FitCore.Data.FitCoreContext>(options =>
    options.UseSqlServer(connectionString));
// Add services to the container.
builder.Services.AddRazorPages(options =>
{
    //acces restrictionat la foldere daca nu esti logat
    options.Conventions.AuthorizeFolder("/Trainers");
    options.Conventions.AuthorizeFolder("/Members");
    options.Conventions.AuthorizeFolder("/GymClasses");
    options.Conventions.AuthorizeFolder("/MembershipTypes");
});

//adaugam suport pt api si ignoram buclele infinite (ex: relatia many to many antrenor <-> clasa)
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<FitCore.Data.FitCoreContext>();

builder.Services.AddControllers()
    .AddJsonOptions(options => {
        options.JsonSerializerOptions.ReferenceHandler =
            System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
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

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.MapControllers();

app.Run();
