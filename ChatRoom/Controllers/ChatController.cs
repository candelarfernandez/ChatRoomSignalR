﻿
using ChatRoom.Datos.Entidades;
using ChatRoom.Dominio;
using Microsoft.AspNetCore.Mvc;

namespace ChatRoom.Controllers;

public class ChatController : Controller
{
    private readonly ISalaService _salaService;
    private readonly IOfertumService _ofertumService;


    public ChatController(ISalaService salaService, IOfertumService ofertumService)
    {
        _salaService = salaService;
        _ofertumService = ofertumService;
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


    public IActionResult ofertum(int id)
    {
        var ofertum = _ofertumService.GetOfertumById(id);
        if (ofertum == null)
        {
            return NotFound();
        }
        return View(ofertum);
    }

    public IActionResult CreateOferta()
    {

        return View();
    }

    //[HttpPost]
    //public IActionResult CreateOferta(decimal monto, int? idComprador, int? idSala)
    //{
    //    if (monto > 0 && idComprador.HasValue && idSala.HasValue)
    //    {
    //        var oferta = _ofertumService.CreateOfertum(monto, idComprador.Value, idSala.Value);
    //        // return RedirectToAction("DetalleSala", new { idSala = idSala.Value });
    //        //var ofertums = _ofertumService.GetOfertums().Where(o => o.IdSala == idSala);
    //        //var ofertaMaxima = ofertums.OrderByDescending(o => o.Monto).FirstOrDefault();
    //        // return View(ofertaMaxima);
    //        var sala = _salaService.GetSalaById(idSala.Value);

    //        // Agregar la oferta recién creada a la lista de ofertas de la sala
    //        if (sala != null)
    //        {
    //            sala.Oferta.Add(oferta);
    //            _salaService.agregarOfertaALaSala(oferta, idSala.Value);
    //        }



    //    }
    //    return View();
    //}

    [HttpPost]
    public IActionResult CreateOferta(decimal monto, int? idComprador, int? idSala)
    {
        if (monto > 0 && idComprador.HasValue && idSala.HasValue)
        {
            var oferta = _ofertumService.CreateOfertum(monto, idComprador.Value, idSala.Value);

            var sala = _salaService.GetSalaById(idSala.Value);
            if (sala != null)
            {
                sala.Oferta.Add(oferta);
                _salaService.agregarOfertaALaSala(oferta, idSala.Value);
            }

          //  return RedirectToAction("DetalleSala", new { idSala = idSala.Value });
        }
        return View();
    }



    //public IActionResult DetalleSala(int idSala)
    //{
    //    var sala = _salaService.GetSalaById(idSala);
    //    if (sala == null)
    //    {
    //        return NotFound();
    //    }

    //    var ofertums = _ofertumService.GetOfertums().Where(o => o.IdSala == idSala);
    //    var ofertaMaxima = ofertums.OrderByDescending(o => o.Monto).FirstOrDefault();

    //    ViewData["OfertaMaxima"] = ofertaMaxima;

    //    return View(sala);
    //}


  
}
