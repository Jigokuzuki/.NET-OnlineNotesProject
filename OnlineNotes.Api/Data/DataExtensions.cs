using Microsoft.EntityFrameworkCore;
using OnlineNotes.Api.Repositories;

namespace OnlineNotes.Api.Data;

public static class DataExtensions
{
    public static async Task InitalizeDbAsync(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<OnlineNotesContext>();
        await dbContext.Database.MigrateAsync();
    }

    public static IServiceCollection AddRepo(this IServiceCollection services, IConfiguration configuration)
    {
        var conString = configuration.GetConnectionString("OnlineNotesContext");
        services.AddSqlServer<OnlineNotesContext>(conString).AddScoped<INotesRepository, EntityFrameworkNotesRepository>();

        return services;
    }

    public static IServiceCollection AddRepo2(this IServiceCollection services, IConfiguration configuration)
    {
        var conString = configuration.GetConnectionString("OnlineNotesContext");
        services.AddSqlServer<OnlineNotesContext>(conString).AddScoped<IUsersRepository, EntityFrameworkUsersRepository>();

        return services;
    }

    public static IServiceCollection AddRepo3(this IServiceCollection services, IConfiguration configuration)
    {
        var conString = configuration.GetConnectionString("OnlineNotesContext");
        services.AddSqlServer<OnlineNotesContext>(conString).AddScoped<IUserNotesRepository, EntityFrameworkUserNotesRepository>();

        return services;
    }
}