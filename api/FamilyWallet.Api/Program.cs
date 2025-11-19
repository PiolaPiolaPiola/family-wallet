using FamilyWallet.Api.Configurations;
using Q10.TaskManager.Api.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddServices();

builder.Services.AddDatabaseConfiguration();

builder.Services.AddSwaggerConfiguration();
builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        policy.WithOrigins("http://localhost:4200", "http://localhost:80")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

// Add Authentication & Authorization
builder.Services.AddAuthConfiguration(builder.Configuration);

var app = builder.Build();

//await builder.Services.DatabaseCreatedAsync();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerConfiguration();
}

app.UseHttpsRedirection();
app.MapControllers();

// Use CORS
app.UseCors("AllowAngularApp");

// Use Authentication & Authorization
app.UseAuthentication();
app.UseAuthorization();

app.Run();
