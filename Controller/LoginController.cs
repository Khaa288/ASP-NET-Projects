using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Note.API.Data;
using Note.API.Models.Entities;

namespace Note.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class LoginController : ControllerBase{
    private readonly NoteDbContext _dbcontext;
    private readonly IConfiguration _config;

    public LoginController(NoteDbContext dbContext, IConfiguration config) {
        this._dbcontext = dbContext;
        this._config = config;
    }

    [AllowAnonymous]
    [HttpPost]
    public IActionResult Login([FromBody] UserLoginModel userLogin) {
        // Xác thực user
        var user = Authenticate(userLogin);

        // Tạo token nếu có user hợp lệ 
        if (user != null) {
            var token = GenerateToken(user);
            return Ok(new {message = "User authenticated", token = token});
        }

        else {
            return NotFound("User not found, Access denied!");
        }
    }

    private UserModel Authenticate(UserLoginModel userLogin) {
        #nullable disable
        // Tìm kiếm user
        var user = _dbcontext.Users.Where(value => value.Username == userLogin.Username &&  
                                                   value.Password == userLogin.Password).FirstOrDefault();
        // Return rỗng nếu không có user
        return user != null ? user : null;
    }

    private string GenerateToken(UserModel user) {
        #nullable disable
        // optional -> có cái credentials, key, claim này bảo mật hơn
        // Tức là nó gen cái token dài hơn 
        String key = _config["Jwt:Key"];

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>() {
            new Claim(ClaimTypes.NameIdentifier, user.Username),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.GivenName, user.FirstName),
            new Claim(ClaimTypes.Surname, user.LastName),
            new Claim(ClaimTypes.Role, user.Role)
        };
        
        // từ những options ở trên từ đó tạo valid tokens cho authentication
        var token = new JwtSecurityToken(_config["Jwt:Issuer"], 
                                         _config["Jwt:Audience"],
                                         claims,
                                         expires: DateTime.Now.AddMinutes(15),
                                         signingCredentials: credentials);

        
        // hoặc chỉ cần 2 thuộc tính căn bẩn để gen token là Issuer(ng tổ chức token) - Audience(ng xài token)
        // dead code để ví dụ
        var basicToken = new JwtSecurityToken(_config["Jwt: Issuer"],
                                              _config["Jwt: Audience"]);
        // dead code để ví dụ

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}


