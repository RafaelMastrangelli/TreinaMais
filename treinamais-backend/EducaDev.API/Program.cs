using EducaDev.API.Application.Services;
using EducaDev.API.Application.Services.Interfaces;
using EducaDev.API.Infrastructure.Integrations;
using EducaDev.API.Infrastructure.Integrations.Interfaces;
using EducaDev.API.Infrastructure.Integrations.Services;
using EducaDev.API.Infrastructure.Persistence;
using EducaDev.API.Infrastructure.Persistence.Repositories;
using EducaDev.API.Infrastructure.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddDbContext<EducaDevContext>(opt =>
    opt.UseInMemoryDatabase("educadev-db"));

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

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseCors("AllowFrontend");

app.UseAuthorization();

app.MapControllers();

app.Run();
