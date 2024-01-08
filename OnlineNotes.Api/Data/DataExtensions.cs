using Microsoft.EntityFrameworkCore;
using OnlineNotes.Api.Repositories;

namespace OnlineNotes.Api.Data;

public static class DataExtensions
{
    public static void InitalizeDb(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<OnlineNotesContext>();
        dbContext.Database.Migrate();
    }

    public static IServiceCollection AddRepo(this IServiceCollection services, IConfiguration configuration)
    {
        var conString = configuration.GetConnectionString("OnlineNotesContext");
        services.AddSqlServer<OnlineNotesContext>(conString).AddScoped<INotesRepository, EntityFrameworkNotesRepository>();

        return services;
    }
}