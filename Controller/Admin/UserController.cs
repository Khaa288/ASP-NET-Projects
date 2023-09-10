using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Note.API.Data;
using Note.API.Models.Entities;

namespace Note.API.Controllers.Admin;

[Authorize]
[Route("api/[controller]")]
[ApiController]

public class UsersController : Controller {
    private readonly NoteDbContext _dbcontext;

    public UsersController(NoteDbContext dbContext) {
        _dbcontext = dbContext;
    }

    [HttpGet("admin")]
    public async Task<IActionResult> GetAllUsers() {
        var AllUser = await _dbcontext.Users.ToListAsync();
        if (AllUser.Any())
            return Ok(AllUser);
        else 
            return NotFound(new {message = "Don't have any user!!!"});
    }

    [HttpGet("admin/{id:Guid}")]
    [ActionName("GetUserById")]
    public async Task<IActionResult> GetUserById([FromRoute] Guid id) {
        var user = await _dbcontext.Users.FindAsync(id);
        if (user == null)
            return NotFound(new { message= "Don't have any user like this!!!"});
        else 
            // Trả về status 200 kèm nội dung là thằng note được trả về
            return Ok(user);
    }

    [HttpPost("admin")]
    public async Task<IActionResult> AddUser(UserModel user) {
        user.id = Guid.NewGuid();
        await _dbcontext.Users.AddAsync(user);
        await _dbcontext.SaveChangesAsync();
        // Trả về đường dẫn là: action(GetUserById)/id(user.id) và trả về đối tượng được tạo
        return CreatedAtAction(nameof(GetUserById), new {id = user.id}, user);
    }

    [HttpPut("admin/{id:Guid}")]
    public async Task<IActionResult> UpdateUserInfo([FromRoute] Guid id, [FromBody] UserModel updateUser) {
        var user = await _dbcontext.Users.FindAsync(id);

        if (user == null)
            return NotFound(new { message= "Don't have any user like this!!!"});

        // chưa bít update gì
        user.Email = updateUser.Email;
        user.FirstName = updateUser.FirstName;
        user.LastName = updateUser.LastName;

        await _dbcontext.SaveChangesAsync();

        return Ok(new { status = "Update success", updateUser = user });
    }

    [HttpDelete("admin/{id:Guid}")]
    public async Task<IActionResult> DeleteUser([FromRoute] Guid id) {
        var user = await _dbcontext.Users.FindAsync(id);

        if (user == null)
            return NotFound(new { message= "Don't have any user like this!!!"});

        _dbcontext.Remove(user);
        await _dbcontext.SaveChangesAsync();

        return Ok(new { status = "Delete success", deletedNote = user});
    }
}