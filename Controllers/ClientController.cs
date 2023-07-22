using Microsoft.AspNetCore.Mvc;
namespace Practice_ASP_NET.Controllers;
public class ClientController : Controller {
    private readonly ILogger<ClientController> _logger;
    private readonly DbContextServices _contextServices;
    public ClientController(ILogger<ClientController> logger, DbContextServices contextServices) {
        _logger = logger;
        _contextServices = contextServices;
    }

    public IActionResult Index() {
        ViewBag.clients = _contextServices.ShowAllClients();
        return View();
    }

    [HttpPost]
    public IActionResult InsertClient(String name, String email, String phone, String address) {
        if (_contextServices.AddClient(name, phone, email, address))
            TempData["isAdded"] = "1 client inserted into database";
        else 
            TempData["isAdded"] = "0 client inserted into database";
        var url = Url.Action("Index", "Client");
        return Redirect(url);
    }

    public IActionResult DeleteClient(Client delClient){
        if (_contextServices.DeleteClient(delClient))
            TempData["isDeleted"] = "1 client deleted from database";
        else 
            TempData["isDeleted"] = "0 client deleted from database";
        var url = Url.Action("Index", "Client");
        return Redirect(url);
    }
}