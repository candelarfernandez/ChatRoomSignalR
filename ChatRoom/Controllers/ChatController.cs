
using ChatRoom.Datos.Entidades;
using ChatRoom.Dominio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace ChatRoom.Controllers;

public class ChatController : Controller
{
    private readonly ISalaService _salaService;
    private readonly IOfertumService _ofertumService;
    private IHubContext<ChatHub> _hubContext;


    public ChatController(ISalaService salaService, IOfertumService ofertumService, IHubContext<ChatHub> hubContext)
    {
        _salaService = salaService;
        _ofertumService = ofertumService;
        _hubContext = hubContext;
    }
    public IActionResult Index()
    {
        if (User.Identity.IsAuthenticated)
        {
            return View("Logueado");
        }
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
    public IActionResult CreateRoom(string nombre, string fotoProductoNombre, string? idVendedor)
    {
        if (!string.IsNullOrEmpty(nombre) && !string.IsNullOrEmpty(fotoProductoNombre))
        {
            var sala = _salaService.CreateSala(nombre, fotoProductoNombre, idVendedor);
            return RedirectToAction("Index");
        }
        return View();
    }

   

}
