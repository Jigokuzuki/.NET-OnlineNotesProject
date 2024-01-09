using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OnlineNotes.Api.Entities;

public class User
{
    public int Id { get; set; }

    [Required]
    [StringLength(20)]
    public required string FirstName { get; set; }

    [Required]
    [StringLength(20)]
    public required string Surname { get; set; }

    [Required]
    [PasswordPropertyText]
    public required string Password { get; set; }

    [Required]
    [EmailAddress]
    public required string Email { get; set; }

    [Required]
    [Url]
    public required string Avatar { get; set; }

    public DateTimeOffset RegisterDate { get; set; }
}