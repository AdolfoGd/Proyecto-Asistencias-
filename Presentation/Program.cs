using Application.Interface;
using Application.Service;
using Infraestructure.DataBase;
using Infraestructure.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Cadena de conexión y DB Factory
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;
builder.Services.AddSingleton(new DBConnectionFactory(connectionString));

builder.Services.AddScoped<IAsistenciaRepository, AsistenciaRepository>();
builder.Services.AddScoped<AsistenciaService>();

builder.Services.AddScoped<IDocentesRepository, DocentesRepository>();
builder.Services.AddScoped<DocentesServices>();

builder.Services.AddScoped<IEmpleadoRepository, EmpleadoRepository>();
builder.Services.AddScoped<EmpleadoService>();

builder.Services.AddScoped<IEstudianteRepository, EstudianteRepository>();
builder.Services.AddScoped<EstudianteService>();

builder.Services.AddScoped<IGruposRepository, GruposRepository>();
builder.Services.AddScoped<GrupoService>();

builder.Services.AddScoped<IMatriculaRepository, MatriculasRepository>();
builder.Services.AddScoped<MatriculasService>();

builder.Services.AddScoped<ISeseionDeClaseRepository, SesionDeClaseRepository>();
builder.Services.AddScoped<SesionDeClaseService>();

builder.Services.AddControllersWithViews();

// Autenticación por Cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
        options.AccessDeniedPath = "/Account/Login";
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Asistencias}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();