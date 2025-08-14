using FluentValidation.AspNetCore;
using LuxuryHub.API.Middleware;
using LuxuryHub.Application;
using LuxuryHub.Infrastructure;
using LuxuryHub.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

            // Add Application and Infrastructure services
            builder.Services.AddApplicationServices();
            builder.Services.AddInfrastructureServices(builder.Configuration);

            // Add Redis Cache
            builder.Services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = builder.Configuration.GetConnectionString("Redis") ?? "localhost:6379";
                options.InstanceName = "LuxuryHub_";
            });

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(LuxuryHub.Application.Mappings.AutoMapperProfile));

// Add FluentValidation
builder.Services.AddFluentValidationAutoValidation();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "LuxuryHub Real Estate API V1");
        c.RoutePrefix = string.Empty;
    });
}

// Configure HTTPS redirection only in production
if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

// Use CORS
app.UseCors("AllowAll");

app.UseAuthorization();

// Add global exception handling middleware
app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

app.MapControllers();

app.Run();
