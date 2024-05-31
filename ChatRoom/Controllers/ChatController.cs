
using ChatRoom.Dominio;
using Microsoft.AspNetCore.Mvc;

namespace ChatRoom.Controllers;

public class ChatController : Controller
{
    private readonly ISalaService _salaService;

    public ChatController(ISalaService salaService)
    {
        _salaService = salaService;
    }
    public IActionResult Index()
    {
        var salas = _salaService.GetSalas();
        return View(salas);
    }
    public IActionResult Room(int id)
    {
        var sala = _salaService.GetSalaById(id);
        if (sala == null)
        {
            return NotFound();
        }
        return View(sala);
    }

    public IActionResult CreateRoom()
    {

        return View();
    }

    [HttpPost]
    public IActionResult CreateRoom(string nombre, string fotoProductoNombre, int? idVendedor)
    {
        if (!string.IsNullOrEmpty(nombre) && !string.IsNullOrEmpty(fotoProductoNombre))
        {
            var sala = _salaService.CreateSala(nombre, fotoProductoNombre, idVendedor);
            return RedirectToAction("Index");
        }
        return View();
    }
}
