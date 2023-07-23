using Microsoft.AspNetCore.Mvc;
using Practice_ASP_NET.Services;

namespace Practice_ASP_NET.Controllers;
public class ClientController : Controller {
    private readonly ILogger<ClientController> _logger;
    private readonly DbContextServices _contextServices;
    public ClientController(ILogger<ClientController> logger, DbContextServices contextServices) {
        _logger = logger;
        _contextServices = contextServices;
    }

    public IActionResult Index() {
        var clients = _contextServices.ShowAllClients();
        return View("Index", clients);
    }

    public IActionResult EditClient(int Id) {
        var client = _contextServices.FindAClient(Id);
        return View("Edit", client);
    }

    public IActionResult Edit_Confirmed(int id, String name, String email, String phone, String address) {
        if (_contextServices.EditClient(id, name, email, phone, address))
            TempData["isUpdated"] = "1 client updated into database";
        else 
            TempData["isUpdated"] = "0 client updated into database";
        var url = Url.Action("EditClient", "Client", new {id= id});
        return Redirect(url);
    }

    public IActionResult InsertClient(String name, String email, String phone, String address) {
        if (_contextServices.AddClient(name, phone, email, address))
            TempData["isAdded"] = "1 client inserted into database";
        else 
            TempData["isAdded"] = "0 client inserted into database";
        var url = Url.Action("Index", "Client");
        return Redirect(url);
    }

    public IActionResult DeleteClient(int Id){
        if (_contextServices.DeleteClient(Id))
            TempData["isDeleted"] = "1 client deleted from database";
        else 
            TempData["isDeleted"] = "0 client deleted from database";
        var url = Url.Action("Index", "Client");
        return Redirect(url);
    }
}