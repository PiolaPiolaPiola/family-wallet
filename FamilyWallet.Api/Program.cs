using Q10.TaskManager.Api.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerConfiguration();

var app = builder.Build();

//await builder.Services.DatabaseCreatedAsync();
app.UseSwaggerConfiguration(app.Environment);

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();
