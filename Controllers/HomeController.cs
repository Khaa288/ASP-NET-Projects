using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Practice_ASP_NET.Models;
using Practice_ASP_NET.Services;

namespace Practice_ASP_NET.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly DbContextServices _contextServices;

    public HomeController(ILogger<HomeController> logger, DbContextServices contextServices)
    {
        _contextServices = contextServices;
        _logger = logger;
    }

    public IActionResult Index()
    {
        //_contextServices.InitData();
        return View();
    }

    public IActionResult CreateDB() {
        if (_contextServices.CreateDb()) {
            TempData["isCreated"] = "MyStoreDB created successfully!!!";
        }
        else
            TempData["isCreated"] = "MyStoreDB created failed!!!";
        var url = Url.Action("Index", "Home");
        return Redirect(url);
    }

    public IActionResult DeleteDB()
    {
        if (_contextServices.DeleteDb()) {
            TempData["isDeleted"] = "MyStoreDB deleted successfully!!!";
        }
        else 
             TempData["isDeleted"] = "MyStoreDB deleted failed!!!";
        var url = Url.Action("Index", "Home");
        return Redirect(url);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
