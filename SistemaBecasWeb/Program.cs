using Microsoft.EntityFrameworkCore;
using SistemaBecasWeb.Data;
using SistemaBecasWeb.Services;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication("CookieAuth")
    .AddCookie("CookieAuth", config => {
        config.Cookie.Name = "BecaCookie";
        config.LoginPath = "/Login/Index";
    });


// Add services to the container.
builder.Services.AddControllersWithViews();

// Fuerza a que el sistema siempre entienda el punto como decimal
var cultureInfo = new System.Globalization.CultureInfo("en-US");
System.Globalization.CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
System.Globalization.CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registrar la capa de servicios para la inyección de dependencias
builder.Services.AddScoped<IEvaluacionBecaService, EvaluacionBecaService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=SolicitudBecas}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
