using OnlineNotes.Api.Dtos;

namespace OnlineNotes.Api.Entities;

public static class EntityExtensions
{
    public static NoteDto AsDto(this Note note)
    {
        return new NoteDto(
            note.Id,
            note.Title,
            note.Content,
            note.CreatedDate,
            note.ModifiedDate,
            note.Category,
            note.IsFavorite,
            note.Color
        );
    }

    public static UserDto AsDto(this User user)
    {
        return new UserDto(
            user.Id,
            user.FirstName,
            user.Surname,
            user.Email,
            user.Password,
            user.RegisterDate
        );
    }

    public static UserNotesDto AsDto(this UserNotes usernotes)
    {
        return new UserNotesDto(
            usernotes.Id,
            usernotes.UserId,
            usernotes.NoteId
        );
    }
}