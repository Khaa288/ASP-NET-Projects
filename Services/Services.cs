#nullable disable
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Note.API.Data;

namespace Note.API.Service;

public static class Service {
    public static void ConfigureServices(this WebApplicationBuilder builder) {
        // Add services to the container.
        builder.Services.AddRazorPages();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddDbContext<NoteDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("NotesDbConnectionString")));
        builder.Services.AddSignalR();

        // Service cho authentication
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme) // có JwtBearerDefaults.AuthenticationScheme mới xài được [Authorize]
                        .AddJwtBearer(options => {
                            String Key = builder.Configuration["Jwt:Key"];
                            String Issuer = builder.Configuration["Jwt:Issuer"];
                            String Audience = builder.Configuration["Jwt:Audience"];
                            options.TokenValidationParameters = new TokenValidationParameters{
                                ValidateIssuer = true,
                                ValidateAudience = true,
                                ValidateLifetime = true,
                                ValidateIssuerSigningKey = true,
                                ValidIssuer = Issuer,
                                ValidAudience = Audience,
                                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key))
                            };
                        });

        // // thêm service để 2 host giao tiếp được với nhau (FE với BE)
        // // nếu ko có thì FE k gửi API cho BE được
        // builder.Services.AddCors(options => options.AddDefaultPolicy(builder => {
        //     builder.WithOrigins("http://localhost:5078/")
        //            .AllowAnyOrigin()
        //            .AllowAnyHeader()
        //            .AllowAnyMethod();
        //         //    .AllowCredentials();
        // }));

        // The services include Support for Controllers, Model Binding, API Explorer, Authorization, CORS, Validations, Formatter Mapping
        // Sử dụng được cho ASP.NET API thay cho các service trên
        builder.Services.AddControllers();
    }
}