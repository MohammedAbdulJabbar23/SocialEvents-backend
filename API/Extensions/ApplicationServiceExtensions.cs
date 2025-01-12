using Application.Activities;
using Application.Core;
using Application.Interfaces;
using FluentValidation;
using FluentValidation.AspNetCore;
using Infastructure.Security;
using Microsoft.EntityFrameworkCore;
using Npgsql.Replication;
using Persistence;

namespace API.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddAutoMapper(typeof(MappingProfiles).Assembly);
        services.AddDbContext<DataContext>(opt =>
        {
            opt.UseNpgsql(config.GetConnectionString("DefaultConnection"));
        });

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(List.Handler).Assembly));
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblyContaining<Create>();
        services.AddHttpContextAccessor();
        services.AddScoped<IUserAccessor, UserAccessor>();
        services.AddSignalR();
        return services;
    }
}