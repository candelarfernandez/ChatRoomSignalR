using ChatRoom.Datos.Entidades;
using ChatRoom.Dominio;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace ChatRoom
{
    public class ChatHub : Hub
    {
        private readonly ISalaService _salaService;
        private readonly IOfertumService _ofertumService;

        public ChatHub(ISalaService salaService, IOfertumService ofertumService)
        {
            _salaService = salaService;
            _ofertumService = ofertumService;
        }     

        public async Task AddOferta(string salaId, string idComprador, string monto)
        {
                int salaIdInt = int.Parse(salaId);
                decimal montoDecimal = decimal.Parse(monto);
            try
            {
                var oferta = _ofertumService.CreateOfertum(montoDecimal, idComprador, salaIdInt);
                if (oferta != null)
                {                  
                    _salaService.agregarOfertaALaSala(oferta, salaIdInt);
                    var sala = _salaService.GetSalaById(salaIdInt);
                    if (sala != null)
                    {
                        await Clients.Group(salaId).SendAsync("ReceiveOferta", sala.Oferta);
                    }
                }
                
            }
            catch (Exception ex)
            {
                await Clients.Caller.SendAsync("ShowError", ex.Message);
            }
        }
        public async Task JoinSala(string salaId, string userName)
        {
                int salaIdInt = int.Parse(salaId);
                await Groups.AddToGroupAsync(Context.ConnectionId, salaId);
                var sala = _salaService.GetSalaById(salaIdInt);
                if (sala != null)
                {
                    await Clients.Caller.SendAsync("ReceiveOferta", sala.Oferta);
                }
            await Clients.Group(salaId).SendAsync("ShowWho", $"{userName} se unió a la sala");

        }

        public async Task SendNotification(string userId, string message)
        {
            await Clients.User(userId).SendAsync("ReceiveNotification", message);
        }

        public async Task CloseAuction(string groupName)
        {
            await Clients.Group(groupName).SendAsync("CloseAuction");
        }
        public async Task CreateSala(string nombre, string base64Image, string idVendedor)
        {
                var fileName = $"{nombre}.jpg"; // Genera un nombre único para la imagen
                var filePath = Path.Combine("wwwroot/images", fileName);

                try
                {

                    byte[] imageBytes = Convert.FromBase64String(base64Image);
                    await System.IO.File.WriteAllBytesAsync(filePath, imageBytes);

                    // Guarda solo la ruta relativa en la base de datos
                    var relativeFilePath = "/images/" + fileName;
                    var sala = _salaService.CreateSala(nombre, relativeFilePath, idVendedor);

                    if (sala != null)
                    {
                        var salas = _salaService.GetSalas();
                        await Clients.All.SendAsync("ReceiveSalas", salas);
                    }
                    else
                    {
                        throw new Exception("Error al crear la sala.");
                    }
                }
                catch (Exception ex)
                {
                    
                    throw new Exception($"Error interno del servidor: {ex.Message}");
                }
            
        }
        public async Task GetSalas()
        {
            var salas = _salaService.GetSalas();
            await Clients.Caller.SendAsync("ReceiveSalas", salas);
        }

    }
}
