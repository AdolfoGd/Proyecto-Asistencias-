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

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login"; // A dónde va si intenta entrar a una vista protegida
        options.AccessDeniedPath = "/Account/AccesoDenegado"; // A dónde va si no tiene permisos
        options.ExpireTimeSpan = TimeSpan.FromHours(8); // Tiempo que dura la sesión
    });

var app = builder.Build();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// 2. ACTIVAR AUTENTICACIÓN Y AUTORIZACIÓN (El orden es obligatorio)
app.UseAuthentication(); // Siempre va PRIMERO
app.UseAuthorization();  // Siempre va DESPUÉS

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();