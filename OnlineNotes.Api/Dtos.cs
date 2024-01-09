using System.ComponentModel.DataAnnotations;

namespace OnlineNotes.Api.Dtos;

public record NoteDto(
    int Id,
    string Title,
    string Content,
    DateTimeOffset CreatedDate,
    DateTimeOffset ModifiedDate,
    string Category,
    bool IsFavorite,
    string Color
);

public record CreateNoteDto(
    [Required][StringLength(50)] string Title,
    [Required] string Content,
    DateTimeOffset CreatedDate,
    DateTimeOffset ModifiedDate,
    [Required][StringLength(20)] string Category,
    [Required] bool IsFavorite,
    [Required] [StringLength(20)] string Color
);

public record UpdateNoteDto(
    [Required][StringLength(50)] string Title,
    [Required] string Content,
    DateTimeOffset CreatedDate,
    DateTimeOffset ModifiedDate,
    [Required][StringLength(20)] string Category,
    [Required] bool IsFavorite,
    [Required] [StringLength(20)] string Color
);