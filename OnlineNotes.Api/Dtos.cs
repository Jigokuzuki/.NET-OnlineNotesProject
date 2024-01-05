using System.ComponentModel.DataAnnotations;

namespace OnlineNotes.Api.Dtos;

public record NoteDto(
    int Id,
    string Title,
    string Content,
    DateTimeOffset CreatedDate,
    DateTimeOffset ModifiedDate,
    string Category
);

public record CreateNoteDto(
    [Required][StringLength(50)] string Title,
    [Required] string Content,
    DateTimeOffset CreatedDate,
    DateTimeOffset ModifiedDate,
    [Required][StringLength(20)] string Category
);

public record UpdateNoteDto(
    [Required][StringLength(50)] string Title,
    [Required] string Content,
    DateTimeOffset CreatedDate,
    DateTimeOffset ModifiedDate,
    [Required][StringLength(20)] string Category
);