using Microsoft.EntityFrameworkCore;
using Note.API.Models.Entities;

namespace Note.API.Data;

public class NoteDbContext : DbContext {
    public NoteDbContext(DbContextOptions options) : base(options) {

    }

    public DbSet<NoteModel> notes {get;set;}
}