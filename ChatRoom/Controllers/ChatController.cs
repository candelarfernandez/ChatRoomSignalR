﻿
using ChatRoom.Datos.Entidades;
using ChatRoom.Dominio;
using ChatRoom.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace ChatRoom.Controllers;

public class ChatController : Controller
{
    private readonly ISalaService _salaService;
    private readonly IOfertumService _ofertumService;
    private IHubContext<ChatHub> _hubContext;
    private readonly UserManager<Usuario> _userManager;


    public ChatController(ISalaService salaService, IOfertumService ofertumService, IHubContext<ChatHub> hubContext, UserManager<Usuario> userManager)
    {
        _salaService = salaService;
        _ofertumService = ofertumService;
        _hubContext = hubContext;
        _userManager = userManager;
    }
    public IActionResult Index()
    {
        var user = _userManager.GetUserAsync(User).Result;
        var salas = _salaService.GetSalas();

        if (User.Identity.IsAuthenticated)
        {
            ViewData["AuthenticatedUser"] = user;
            return View("Logueado", salas);
        }
        return View(salas);
    }
    public IActionResult Room(int id)
    {

        if (!User.Identity.IsAuthenticated)
        {
            return RedirectToAction("Login", "Account");
        }

        var sala = _salaService.GetSalaById(id);

        if(sala.Activa == false)
        {
            return RedirectToAction("Index");
        }

        var user = _userManager.GetUserAsync(User).Result;
        if (sala == null)
        {
            return NotFound();
        }
        ViewData["AuthenticatedUser"] = user;
        ViewData["IsCreator"] = (user.Id == sala.IdVendedor);
        return View(sala);
    }

    public IActionResult CreateRoom()
   {

       return View();
   }

  [HttpPost]
   public async Task<IActionResult> CreateRoom(string nombre, string fotoProductoNombre, string idVendedor)
   {
       if (string.IsNullOrEmpty(idVendedor))
       {
           return RedirectToAction("Login", "Account");
       }
       if (!string.IsNullOrEmpty(nombre) && !string.IsNullOrEmpty(fotoProductoNombre))
       {
            _salaService.CreateSala(nombre, fotoProductoNombre, idVendedor);
            var salas = _salaService.GetSalas();
            await _hubContext.Clients.All.SendAsync("ReceiveSalas", salas);
            return RedirectToAction("Index");
       }
       return View();
   }

    [HttpPost]
    public IActionResult FinalizarSubasta(int idSala)
    {
        var venta = _salaService.FinalizarSubasta(idSala);

        if (venta != null)
        {
            var comprador = _salaService.GetUserById(venta.IdComprador);
            var vendedor = _salaService.GetUserById(venta.IdVendedor);
            var producto = _salaService.GetProductoById(venta.IdProducto);

            var mensajeComprador = $"Felicidades! El producto: {producto.Nombre} es tuyo! Contactate con {vendedor.UserName} para reclamar tu compra.";
            //este no se muestra
            var mensajeVendedor = $"Felicidades! {comprador.UserName} ha comprado tu producto!";

            _hubContext.Clients.User(venta.IdComprador.ToString()).SendAsync("ReceiveNotification", mensajeComprador);
            _hubContext.Clients.User(venta.IdVendedor.ToString()).SendAsync("ReceiveNotification", mensajeVendedor);

            var groupName = idSala.ToString();
            _hubContext.Clients.Group(groupName).SendAsync("CloseAuction");

        }
        return RedirectToAction("Index");
    }

}
