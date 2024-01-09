using System.ComponentModel;
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
    [Required][StringLength(20)] string Color
);

public record UpdateNoteDto(
    [Required][StringLength(50)] string Title,
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
    string Avatar,
    DateTimeOffset RegisterDate
);

public record CreateUserDto(
    [Required][StringLength(20)] string FirstName,
    [Required][StringLength(20)] string Surname,
    [Required][EmailAddress] string Email,
    [Required][PasswordPropertyText] string Password,
    [Url] string Avatar,
    DateTimeOffset RegisterDate
);

public record UpdateUserDto(
    [Required][StringLength(20)] string FirstName,
    [Required][StringLength(20)] string Surname,
    [Required][EmailAddress] string Email,
    [Required][PasswordPropertyText] string Password,
    [Url] string Avatar,
    DateTimeOffset RegisterDate
);