using apiexamen;
using apiexamen.Config;
using apiexamen.Interfaces;
using BansiFront.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

//Se agrega para peticiones js a clases modelos
builder.Services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");

//Se agrega la referencia a la api
builder.Services.AddHttpClient<ExamenService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7227/");
});

// Configuración global de acceso a datos
var config = new ConfiguracionAccesoDatos(); 
builder.Services.AddSingleton(config);

// Fábrica de acceso a datos
builder.Services.AddScoped<clsExamen>();

// Servicio de acceso a datos según la configuración actual
builder.Services.AddScoped<IDataAccess>(provider =>
    provider.GetRequiredService<clsExamen>().CrearAccesoDatos());

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

app.Run();
