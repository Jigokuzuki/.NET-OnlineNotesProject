using Microsoft.EntityFrameworkCore;
using OnlineNotes.Api.Endpoints;
using OnlineNotes.Api.Entities;

namespace OnlineNotes.Api.Data;

public class OnlineNotesContext : DbContext
{
    public OnlineNotesContext(DbContextOptions<OnlineNotesContext> options) : base(options)
    {
    }

    public DbSet<Note> Notes => Set<Note>();
    public DbSet<User> Users => Set<User>();

}