using Microsoft.EntityFrameworkCore;
using ViolationWebApplication.Interfaces;
using ViolationWebApplication.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string connection = builder.Configuration.GetConnectionString("PostgreSql");
builder.Services.AddTransient<IViolationRepository, ViolationRepository>();
builder.Services.AddTransient<ICarRepository, CarRepository>();
builder.Services.AddTransient<IOwnerRepository, OwnerRepository>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddDbContext<AppDbContext>(options=>options.UseNpgsql(connection));
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
