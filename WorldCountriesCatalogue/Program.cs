using Microsoft.EntityFrameworkCore;
using WorldCountriesCatalogue.Model;
using WorldCountriesCatalogue.Storage;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Регистрация контекста базы данных
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("LocalSqlServerConnection");

    if (string.IsNullOrEmpty(connectionString))
    {
        throw new InvalidOperationException("Connection string 'LocalSqlServerConnection' not found in configuration.");
    }

    options.UseSqlServer(connectionString);
});

// Регистрация зависимостей
builder.Services.AddScoped<ICountryStorage, CountryStorage>();
builder.Services.AddScoped<CountryScenarios>();

var app = builder.Build();

app.UseRouting();
app.MapControllers();

app.Run();
