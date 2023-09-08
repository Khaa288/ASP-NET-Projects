using Microsoft.EntityFrameworkCore;
using Note.API.Data;
using Note.API.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<NoteDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("NotesDbConnectionString")));
builder.Services.AddSignalR();

// // thêm service để 2 host giao tiếp được với nhau (FE với BE)
// // nếu ko có thì FE k gửi API cho BE được
builder.Services.AddCors(options => options.AddDefaultPolicy(builder => {
    builder.WithOrigins("http://localhost:5078/")
           .AllowAnyOrigin()
           .AllowAnyHeader()
           .AllowAnyMethod();
        //    .AllowCredentials();
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseCors(policy => 
    policy.AllowAnyHeader()
          .AllowAnyOrigin()
          .AllowAnyMethod()
        //   .AllowCredentials()
);

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.MapHub<NoteHub>("/noteHub");

app.MapControllerRoute(
    name: "default",
    pattern: "api/[controller]/{id?}"
);

// app.UseEndpoints(enpoints => {
//     enpoints.MapControllerRoute(
//         name: "default",
//         pattern: "api/[controller]/{id?}"
//     );
// });

app.Run();
