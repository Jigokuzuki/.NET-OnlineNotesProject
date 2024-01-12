using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineNotes.Api.Entities;

public class UserNotes
{
    public int Id { get; set; }

    [ForeignKey("NoteId")]
    public int NoteId { get; set; }

    [ForeignKey("UserId")]
    public int UserId { get; set; }
}