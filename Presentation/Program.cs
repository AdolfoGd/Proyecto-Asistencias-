using Application.Interface;
using Application.Service;
using Infraestructure.Repository;

var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddSingleton(new Infraestructure.DataBase.DBConnectionFactory(connectionString));
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
builder.Services.AddScoped<SesionDeClaseRepository>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Asistencias}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
