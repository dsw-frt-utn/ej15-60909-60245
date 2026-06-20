using Dsw2026Ej15.Api.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddHealthChecks();

builder.Services.AddSingleton<Dsw2026Ej15.Data.IPersistence>(Dsw2026Ej15.Data.PersistenceInMemory.Instance);

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.MapHealthChecks("/health-check");

app.Run();