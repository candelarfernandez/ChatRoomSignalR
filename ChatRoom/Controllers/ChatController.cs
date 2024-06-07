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
        var sala = _salaService.GetSalaById(id);
        var user = _userManager.GetUserAsync(User).Result;
        if (sala == null)
        {
            return NotFound();
        }
        ViewData["AuthenticatedUser"] = user;
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

    [HttpPost]
    public IActionResult FinalizarSubasta(int idSala)
    {
        _salaService.FinalizarSubasta(idSala);
        return RedirectToAction("Index");
    }

   

}
