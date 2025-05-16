using Microsoft.EntityFrameworkCore;
using WsApiexamen.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ExamenContextoDb>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("ExamenConnString")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//Operaciones HTTP Sobre la base de datos Examen
app.MapPost("/AgregarExamen", async (ExamenContextoDb contexto, TblExaman examen) =>
{
    using var transaction = await contexto.Database.BeginTransactionAsync();

    try
    {
        contexto.TblExamen.Add(examen);
        await contexto.SaveChangesAsync();

      
        await contexto.SaveChangesAsync();

        await transaction.CommitAsync();
        return Results.Created($"/ConsultarExamen/{examen.IdExamen}", examen);
    }
    catch (Exception ex)
    {
        await transaction.RollbackAsync(); 
        return Results.BadRequest($"Error: {ex.Message}");
    }
}).WithOpenApi();



app.MapGet("/ConsultarExamen/{IdExamen?}",async (ExamenContextoDb contexto, int? IdExamen) =>
{
    if (IdExamen is null)
    {
        var examenes = await contexto.TblExamen.ToListAsync();
        return examenes.Count > 0 ? Results.Ok(examenes) : Results.NotFound("No hay exámenes registrados.");
    }

    var examen = await contexto.TblExamen.Where(t => t.IdExamen == IdExamen).ToListAsync();
    return examen is not null ? Results.Ok(examen) : Results.NotFound("Examen no encontrado.");


}).WithOpenApi();

app.MapGet("/ConsultarExamenes", async (ExamenContextoDb contexto) =>
{
    var examenes = await contexto.TblExamen.ToListAsync();
    return examenes.Count > 0 ? Results.Ok(examenes) : Results.NotFound("No hay exámenes registrados.");

}).WithOpenApi();


app.MapPatch("/ActualizarExamen", async (ExamenContextoDb contexto, TblExaman examen) =>
{
    using var transaction = await contexto.Database.BeginTransactionAsync();

    try
    {
        var examenExistente = await contexto.TblExamen.FindAsync(examen.IdExamen);
        if (examenExistente is null)
            return Results.NotFound("Examen no encontrado.");

        examenExistente.Nombre = examen.Nombre;
        examenExistente.Descripcion = examen.Descripcion;

        contexto.TblExamen.Update(examenExistente);
        await contexto.SaveChangesAsync();

        await transaction.CommitAsync(); 
        return Results.Ok("Examen actualizado correctamente.");
    }
    catch (Exception ex)
    {
        await transaction.RollbackAsync();
        return Results.BadRequest($"Error al actualizar: {ex.Message}");
    }

}).WithOpenApi();


app.MapDelete("/EliminarExamen/{IdExamen}", async(ExamenContextoDb contexto,int IdExamen) =>
{
    using var transaction = await contexto.Database.BeginTransactionAsync();

    try
    {
        var examen = await contexto.TblExamen.FindAsync(IdExamen);
        if (examen is null)
            return Results.NotFound("Examen no encontrado.");

        contexto.TblExamen.Remove(examen);
        await contexto.SaveChangesAsync();

        await transaction.CommitAsync(); 
        return Results.Ok("Examen eliminado correctamente.");
    }
    catch (Exception ex)
    {
        await transaction.RollbackAsync(); 
        return Results.BadRequest($"Error al eliminar: {ex.Message}");
    }

}).WithOpenApi();




app.Run();
