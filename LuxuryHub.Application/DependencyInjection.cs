using FluentValidation;
using LuxuryHub.Application.Interfaces;
using LuxuryHub.Application.Services;
using LuxuryHub.Application.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace LuxuryHub.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IPropertyService, PropertyService>();
        services.AddScoped<IOwnerService, OwnerService>();
        services.AddScoped<IPropertyImageService, PropertyImageService>();
        services.AddScoped<IPropertyTraceService, PropertyTraceService>();
        services.AddValidatorsFromAssemblyContaining<GetPropertiesRequestValidator>();

        return services;
    }
}
