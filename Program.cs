using Microsoft.EntityFrameworkCore;
using Note.API.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<NoteDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("NotesDbConnectionString")));

// // thêm service để 2 host giao tiếp được với nhau (FE với BE)
// // nếu ko có thì FE k gửi API cho BE được
builder.Services.AddCors(options => options.AddDefaultPolicy(builder => {
    builder.WithOrigins("http://172.16.2.108:6433/", "http://localhost:5078/")
           .AllowAnyOrigin()
           .AllowAnyHeader().AllowAnyMethod();
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

app.UseCors(policy => policy.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.UseEndpoints(enpoints => {
    enpoints.MapControllerRoute(
        name: "default",
        pattern: "api/[controller]/{id?}"
    );
});

app.Run();
