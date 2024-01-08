using Microsoft.EntityFrameworkCore;

namespace OnlineNotes.Api.Data;

public static class DataExtensions
{
    public static void InitalizeDb(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<OnlineNotesContext>();
        dbContext.Database.Migrate();
    }
}