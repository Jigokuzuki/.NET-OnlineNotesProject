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
}