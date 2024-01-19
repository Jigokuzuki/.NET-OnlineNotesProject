using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
    [Required][StringLength(10)] string Title,
    [Required] string Content,
    DateTimeOffset CreatedDate,
    DateTimeOffset ModifiedDate,
    [Required][StringLength(20)] string Category,
    [Required] bool IsFavorite,
    [Required][StringLength(20)] string Color
);

public record UpdateNoteDto(
    [Required][StringLength(10)] string Title,
    [Required] string Content,
    DateTimeOffset CreatedDate,
    DateTimeOffset ModifiedDate,
    [Required][StringLength(20)] string Category,
    [Required] bool IsFavorite,
    [Required][StringLength(20)] string Color
);


public record UserDto(
    int Id,
    string FirstName,
    string Surname,
    string Email,
    string Password,
    DateTimeOffset RegisterDate
);

public record LoginUserDto(
    string Email,
    string Password
);

public record CreateUserDto(
    [Required][StringLength(20)] string FirstName,
    [Required][StringLength(20)] string Surname,
    [Required][StringLength(20)] string Email,
    [Required][StringLength(20)] string Password,
    DateTimeOffset RegisterDate
);

public record UpdateUserDto(
    [Required][StringLength(20)] string FirstName,
    [Required][StringLength(20)] string Surname,
    [Required][StringLength(20)] string Email,
    [Required][StringLength(20)] string Password,
    DateTimeOffset RegisterDate
);


public record UserNotesDto(
    int Id,
    int UserId,
    int NoteId
);

public record CreateUserNotesDto(
    int UserId,
    int NoteId
);

public record UpdateUserNotesDto(
    int UserId,
    int NoteId
);