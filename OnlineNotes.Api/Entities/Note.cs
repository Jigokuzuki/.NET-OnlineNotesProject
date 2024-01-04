using System.ComponentModel.DataAnnotations;

namespace OnlineNotes.Api.Entities;

public class Note
{
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public required string Title { get; set; }

    [Required]
    public required string Content { get; set; }
    public DateTimeOffset CreatedDate { get; set; }
    public DateTimeOffset ModifiedDate { get; set; }

    [Required]
    [StringLength(20)]
    public required string Category { get; set; }
}