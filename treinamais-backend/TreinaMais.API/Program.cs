using EducaDev.API.Application.Services;
using EducaDev.API.Application.Services.Interfaces;
using EducaDev.API.Infrastructure.Integrations;
using EducaDev.API.Infrastructure.Integrations.Interfaces;
using EducaDev.API.Infrastructure.Integrations.Services;
using EducaDev.API.Infrastructure.Persistence;
using EducaDev.API.Infrastructure.Persistence.Repositories;
using EducaDev.API.Infrastructure.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddDbContext<TreinaMaisContext>(opt =>
    opt.UseInMemoryDatabase("treinamais-db"));

// Configure AI integrations
builder.Services.Configure<OpenAiConfigurations>(
    builder.Configuration.GetSection("OpenAi"));
builder.Services.Configure<LeonardoAiConfigurations>(
    builder.Configuration.GetSection("LeonardoAi"));

// HTTP clients
builder.Services.AddHttpClient("openai");
builder.Services.AddHttpClient("leonardo");

// AI Services
builder.Services.AddScoped<IOpenAiService, OpenAiService>();
builder.Services.AddScoped<ILeonardoAiService, LeonardoAiService>();
builder.Services.AddScoped<ISentimentAnalysisService, SentimentAnalysisService>();

// Repositories
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();

// Application Services
builder.Services.AddScoped<ICourseApplicationService, CourseApplicationService>();
builder.Services.AddScoped<IReviewApplicationService, ReviewApplicationService>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Configure Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "TreinaMais API",
        Version = "v1",
        Description = "API para gerenciamento de cursos e avaliações do TreinaMais",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "TreinaMais Team",
            Email = "contato@treinamais.com"
        }
    });

    // Include XML comments
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }

    // Configure JWT Authentication in Swagger
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = "JWT Authorization header usando o esquema Bearer. Exemplo: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

// JWT Authentication
var jwtSection = builder.Configuration.GetSection("Jwt");
var jwtKey = jwtSection["Key"]!;
var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSection["Issuer"],
        ValidAudience = jwtSection["Audience"],
        IssuerSigningKey = signingKey,
        ClockSkew = TimeSpan.Zero
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "TreinaMais API v1");
        c.RoutePrefix = string.Empty; // Swagger UI at root
    });
    app.MapOpenApi();
}

app.UseCors("AllowFrontend");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
