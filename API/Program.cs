using API.Data;
using API.Interfaces;
using API.Interfaces.Repository;
using API.Models.User;
using API.Repository;
using API.Services;
using dotenv.net;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;


DotEnv.Load();

var builder = WebApplication.CreateBuilder(args);

// set environment to Test to seed test db
if (args.Length == 2 && args[0] == "seed" && args[1] == "test")
{
    Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Test");
}

// var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? builder.Environment.EnvironmentName;

builder.Configuration
       .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
    //    .AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: true);


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers()
                .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option => {
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});



// Add DBContext
builder.Services.AddDbContext<ApplicationDbContext>(options => {
    string? host = Environment.GetEnvironmentVariable("PGHOST");
    string? port = Environment.GetEnvironmentVariable("POSTGRES_PORT");
    string? password = Environment.GetEnvironmentVariable("POSTGRES_PASSWORD");
    string? db = Environment.GetEnvironmentVariable("POSTGRES_DATABASE");

    options.UseNpgsql($@"Server={host}; Port={port}; Database={db}; User Id=postgres; Password={password};",
        options => options.SetPostgresVersion(17, 2)            
    );
});



// Add Identity
builder.Services.AddIdentity<AppUser, IdentityRole>(options => {
    // Password settings.
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;

    // Lockout settings.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 1000;
    options.Lockout.AllowedForNewUsers = false;

    // User settings.
    options.User.AllowedUserNameCharacters =
        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>();




// Add CORS
const string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options => 
{
    options.AddPolicy(name: MyAllowSpecificOrigins, builder => {
        builder.WithOrigins("http://localhost:4000").AllowAnyMethod().AllowAnyHeader();
    });
});


// Add Authentication
builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultForbidScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options => {
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:Audience"],
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JWT:SigningKey"]!)
        )
    };
});

// Add Logging
builder.Services.AddLogging(loggingBuilder => {
    loggingBuilder.AddConsole()
        .AddFilter(DbLoggerCategory.Database.Command.Name, LogLevel.Information);
    loggingBuilder.AddDebug();
});

// Add Repositories
builder.Services.AddScoped<IPokemonRepository, PokemonRepository>();
builder.Services.AddScoped<IMoveRepository, MoveRepository>();
builder.Services.AddScoped<INatureRepository, NatureRepository>();
builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddScoped<IPkmnTypeRepository, PkmnTypeRepository>();
builder.Services.AddScoped<ITeamRepository, TeamRepository>();
builder.Services.AddScoped<IAbilityRepository, AbilityRepository>();
builder.Services.AddScoped<IGenderRepository, GenderRepository>();

// Add Db Seeder + DbToCSV
builder.Services.AddScoped<IDbInitializer, DbInitializer>();
builder.Services.AddScoped<DbToCSV>();

// Add Token Service
builder.Services.AddScoped<ITokenService, TokenService>();




WebApplication app = builder.Build();


// Migrate and seed database
using (var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
{
    var logger = serviceScope.ServiceProvider.GetRequiredService<ILogger<Program>>();
    var db = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>().Database;

    logger.LogInformation("Migrating database...");
    serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>().Database.Migrate();
    logger.LogInformation("Database migrated successfully.");

    logger.LogInformation("Seeding database...");
    serviceScope.ServiceProvider.GetRequiredService<IDbInitializer>().SeedAll();
    logger.LogInformation("Database seeded successfully.");
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }