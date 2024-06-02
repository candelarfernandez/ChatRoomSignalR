using ChatRoom.Datos.Entidades;
using ChatRoom.Dominio;
using Microsoft.AspNetCore.SignalR;
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
        public async Task JoinSala(string salaId)
        {
                int salaIdInt = int.Parse(salaId);
                await Groups.AddToGroupAsync(Context.ConnectionId, salaId);
                var sala = _salaService.GetSalaById(salaIdInt);
                if (sala != null)
                {
                    await Clients.Caller.SendAsync("ReceiveOferta", sala.Oferta);
                }

        }
    
    }
}
