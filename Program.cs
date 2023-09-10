#nullable disable
using Note.API.Hubs;
using Note.API.Service;

var builder = WebApplication.CreateBuilder(args);

// All Services [Detailed Service at Services.cs]
builder.ConfigureServices();

// Configure the HTTP request pipeline.
var app = builder.Build();

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

app.UseAuthentication();

app.UseAuthorization();

app.MapRazorPages();

app.MapHub<NoteHub>("/noteHub");

app.MapControllerRoute(
    name: "default",
    pattern: "api/[controller]/{id?}"
);

// app.MapControllerRoute(
//     name: "default",
//     pattern: "api/[controller]/admin/{id?}"
// );

// app.UseEndpoints(enpoints => {
//     enpoints.MapControllerRoute(
//         name: "default",
//         pattern: "api/[controller]/{id?}"
//     );

//     enpoints.MapControllers();
// });

app.Run();
