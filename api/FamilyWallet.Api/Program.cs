using Q10.TaskManager.Api.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerConfiguration();
builder.Services.AddControllers();

var app = builder.Build();

//await builder.Services.DatabaseCreatedAsync();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerConfiguration();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
