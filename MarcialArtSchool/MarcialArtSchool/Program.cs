using MartialArtSchool.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddInfrastructure();

var app = builder.Build();
app.UseStaticFiles();
app.MapControllers();
app.Run();
