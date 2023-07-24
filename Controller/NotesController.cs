using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Note.API.Data;
using Note.API.Models.Entities;

namespace Note.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotesController : Controller {
    private readonly NoteDbContext _dbcontext;

    public NotesController(NoteDbContext dbContext) {
        _dbcontext = dbContext;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllNote() {
        var AllNote = await _dbcontext.notes.ToListAsync();
        if (AllNote.Any())
            return Ok(AllNote);
        else 
            return NotFound(new {message = "Don't have any note!!!"});
    }

    [HttpGet]
    [Route("{id:Guid}")]
    [ActionName("GetNoteById")]
    public async Task<IActionResult> GetNoteById([FromRoute] Guid id) {
        var note = await _dbcontext.notes.FindAsync(id);
        if (note == null)
            return NotFound(new { message= "Don't have any note like this!!!"});
        else 
            // Trả về status 200 kèm nội dung là thằng note được trả về
            return Ok(note);
    }

    [HttpPost]
    public async Task<IActionResult> AddNote(NoteModel note) {
        note.id = Guid.NewGuid();
        await _dbcontext.notes.AddAsync(note);
        await _dbcontext.SaveChangesAsync();
        // Trả về đường dẫn là: action(GetNoteById)/id(Note.id) và trả về đối tượng được tạo
        return CreatedAtAction(nameof(GetNoteById), new {id = note.id}, note);
    }

    [HttpPut]
    [Route("{id:Guid}")]
    public async Task<IActionResult> UpdateNote([FromRoute] Guid id, [FromBody] NoteModel updateNote) {
        var note = await _dbcontext.notes.FindAsync(id);

        if (note == null)
            return NotFound(new { message= "Don't have any note like this!!!"});

        note = updateNote;
        await _dbcontext.SaveChangesAsync();

        return Ok(new { status = "Update success", updateNote = note});
    }

    [HttpDelete]
    [Route("{id:Guid}")]
    public async Task<IActionResult> DeleteNote([FromRoute] Guid id) {
        var note = await _dbcontext.notes.FindAsync(id);

        if (note == null)
            return NotFound(new { message= "Don't have any note like this!!!"});

        _dbcontext.Remove(note);
        await _dbcontext.SaveChangesAsync();

        return Ok(new { status = "Delete success", deletedNote = note});
    }
}