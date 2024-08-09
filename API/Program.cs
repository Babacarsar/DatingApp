using Microsoft.EntityFrameworkCore;
using DatingApp.Data;
using API.Services;
using API.Interfaces; // Assurez-vous d'importer le namespace correct pour TokenService
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Ajouter les services au conteneur.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Ajouter les services des contrôleurs
builder.Services.AddControllers(); // Ajouter les services de contrôleur

// Configurer DbContext avec MySQL
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DataContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Ajouter le service TokenService
builder.Services.AddScoped<ITokenService, TokenService>();

// Configurer CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder
            .WithOrigins("http://localhost:4200") // Remplacez par l'URL de votre application Angular
            .AllowAnyHeader()
            .AllowAnyMethod());
});

// Configurer l'authentification JWT
var tokenKey = builder.Configuration["TokenKey"];
var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JwtIssuer"],
            ValidAudience = builder.Configuration["JwtAudience"],
            IssuerSigningKey = key
        };
    });

// Construire l'application
var app = builder.Build();

// Configurer le pipeline de requêtes HTTP.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Utiliser CORS
app.UseCors("AllowSpecificOrigin");

// Utiliser l'authentification
app.UseAuthentication();

// Utiliser l'autorisation
app.UseAuthorization();

// Ajouter les routes des contrôleurs
app.MapControllers(); // Mapper les routes des contrôleurs

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
