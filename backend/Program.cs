using System.Text;
using Mappa.Db;
using Mappa.Entities;
using Mappa.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Mappa.Services;
using Mappa.Dtos;
using Microsoft.IdentityModel.Logging;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddScoped(typeof(IEntityService<,>), typeof(EntityService<,>));
// 1. DbContext
builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(
    configuration.GetConnectionString("db")));

// 2. Identity
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

var audience = configuration["JWT:ValidAudience"];
if (string.IsNullOrEmpty(audience))
{
    throw new ArgumentNullException(nameof(audience), "JWT audience cannot be null or empty.");
}

var issuer = configuration["JWT:ValidIssuer"];
if (string.IsNullOrEmpty(issuer))
{
    throw new ArgumentNullException(nameof(issuer), "JWT issuer cannot be null or empty.");
}

var secret = configuration["JWT:Secret"];
if (string.IsNullOrEmpty(secret))
{
    throw new ArgumentNullException(nameof(secret), "JWT secret cannot be null or empty.");
}

// 3. Adding Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})

// 4. Adding Jwt Bearer
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidAudience = audience,
            ValidIssuer = issuer,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    });

// Add scoped services
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IEntityService<Ethnicity, EthnicityDto>, EthnicityService>();
builder.Services.AddScoped<IEntityService<Gender, GenderDto>, GenderService>();
builder.Services.AddScoped<IEntityService<Genre, GenreDto>, GenreService>();
builder.Services.AddScoped<IEntityService<Language, LanguageDto>, LanguageService>();
builder.Services.AddScoped<IEntityService<Profession, ProfessionDto>, ProfessionService>();
builder.Services.AddScoped<IEntityService<Religion, ReligionDto>, ReligionService>();
builder.Services.AddScoped<IEntityService<Mappa.Entities.Type, TypeDto>, TypeService>();
builder.Services.AddScoped<IComplexEntityService<City, CityGeneralDto, CityDetailDto, CityCreateRequest, CityUpdateRequest>, CityService>();
builder.Services.AddScoped<IComplexEntityService<WrittenSource, WrittenSourceGeneralDto, WrittenSourceDetailDto, WrittenSourceCreateRequest, WrittenSourceUpdateRequest>, WrittenSourceService>();
builder.Services.AddScoped<IComplexEntityService<SecondarySource, SecondarySourceGeneralDto, SecondarySourceDetailDto, SecondarySourceCreateRequest, SecondarySourceUpdateRequest>, SecondarySourceService>();
builder.Services.AddScoped<IComplexEntityService<OrdinaryPerson, OrdinaryPersonGeneralDto, OrdinaryPersonDetailDto, OrdinaryPersonCreateRequest, OrdinaryPersonUpdateRequest>, OrdinaryPersonService>();
builder.Services.AddScoped<IComplexEntityService<UnordinaryPerson, UnordinaryPersonGeneralDto, UnordinaryPersonDetailDto, UnordinaryPersonCreateRequest, UnordinaryPersonUpdateRequest>, UnordinaryPersonService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// 5. Swagger authentication
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Mappa Anatolicorum API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});

// 6. Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddAutoMapper(typeof(MappingProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    IdentityModelEventSource.ShowPII = true;
    IdentityModelEventSource.LogCompleteSecurityArtifact = true;
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mappa Anatolicorum API v1"); // Path to the Swagger JSON
        c.RoutePrefix = string.Empty;  // Set the Swagger UI at the root URL (optional)
    });
}

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.EnsureCreated();
    // if (dbContext.Database.GetPendingMigrations().Any())
    // {
    //     dbContext.Database.Migrate();
    // }
}

app.UseHttpsRedirection();


// In Configure method
//app.UseCors("AllowAll");

// 8. Authentication
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();