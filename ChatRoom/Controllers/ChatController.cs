﻿
using ChatRoom.Datos.Entidades;
using ChatRoom.Dominio;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace ChatRoom.Controllers;

public class ChatController : Controller
{
    private readonly ISalaService _salaService;
    private IHubContext<ChatHub> _hubContext;
    private readonly UserManager<Usuario> _userManager;


    public ChatController(ISalaService salaService, IHubContext<ChatHub> hubContext, UserManager<Usuario> userManager)
    {
        _salaService = salaService;
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

        if (sala.Activa == false)
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

    //unicamente valida si no esta logueado el usuario, para que no se rompa, no me dejo agregarlo de alguna forma en el hub, 
    //asi que si el usuario NO esta logueado, y quiere crear una sala, entra en este metodo (vista index) sino entra en el metodo
    //invocado x signalR en la vista "Logueado"

    [HttpPost]
    public async Task<IActionResult> CreateRoom(string idVendedor)
    {
        if (string.IsNullOrEmpty(idVendedor))
        {
            TempData["ErrorCreateRoom"] = true;
            TempData["ErrorMessage"] = "Debes iniciar sesión para crear una sala.";
            return RedirectToAction("Login", "Account");
        }
        return View();
    }

    [HttpPost]
    public IActionResult FinalizarSubasta([FromBody] FinalizarSubastaRequest request)
    {
        try
        {
            var venta = _salaService.FinalizarSubasta(request.IdSala, request.ForceClose);
            if (venta != null)
            {
                var comprador = _salaService.GetUserById(venta.IdComprador);
                var vendedor = _salaService.GetUserById(venta.IdVendedor);
                var producto = _salaService.GetProductoById(venta.IdProducto);

                var mensajeComprador = $"Felicidades! El producto: {producto.Nombre} es tuyo! Contactate con {vendedor.UserName} para reclamar tu compra.";
                var mensajeVendedor = $"Felicidades! {comprador.UserName} ha comprado tu producto!";

                _hubContext.Clients.User(venta.IdComprador.ToString()).SendAsync("ReceiveNotification", mensajeComprador);
                _hubContext.Clients.User(venta.IdVendedor.ToString()).SendAsync("ReceiveNotification", mensajeVendedor);
            }
            var groupName = request.IdSala.ToString();
            _hubContext.Clients.Group(groupName).SendAsync("CloseAuction");
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    public class FinalizarSubastaRequest
    {
        public int IdSala { get; set; }
        public bool ForceClose { get; set; }
    }


}
