using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OnlineNotes.Api.Entities;

public class User
{
    public int Id { get; set; }

    [Required]
    public required string FirstName { get; set; }

    [Required]
    public required string Surname { get; set; }

    [Required]
    public required string Email { get; set; }

    [Required]
    public required string Password { get; set; }

    public DateTimeOffset RegisterDate { get; set; }
}