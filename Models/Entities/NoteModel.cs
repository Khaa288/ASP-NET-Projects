namespace Note.API.Models.Entities;

public class NoteModel {
    public Guid id {set; get;}
    public string? Title {set; get;}
    public string? Description {set; get;}
    public bool IsVisible {set; get;}
}