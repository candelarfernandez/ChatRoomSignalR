using ChatRoom.Datos;
using ChatRoom.Datos.Entidades;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace ChatRoom.Dominio
{
    public interface ISalaService
    {
        List<Sala> GetSalas();
        Sala? GetSalaById(int id);
        Sala CreateSala(string nombre, string? fotoProductoNombre, string? idVendedor);
        void agregarOfertaALaSala(Ofertum oferta, int idSala);

    }
    public class SalaService : ISalaService
    {
        private SubastaContext _subastaContext;

        public SalaService(SubastaContext subastaContext, IOfertumService ofertumService) {
            _subastaContext = subastaContext;
        }

        public List<Sala> GetSalas()
        {
            return _subastaContext.Salas.Include(s => s.Oferta).ToList();
        }
        public Sala? GetSalaById(int id)
        {
            return _subastaContext.Salas.Include(s => s.Oferta).FirstOrDefault(s => s.Id == id);
        }

        public Sala CreateSala(string nombre, string? fotoProductoNombre, string? idVendedor)
        {
            if (idVendedor == null)
            {
                throw new ArgumentNullException(nameof(idVendedor), "El ID del vendedor no puede ser nulo.");
            }
            var sala = new Sala
            {
                Nombre = nombre,
                FotoProductoNombre = fotoProductoNombre,
                IdVendedor = idVendedor
            };
            _subastaContext.Salas.Add(sala);
            _subastaContext.SaveChanges();
            return sala;
        }
        public void agregarOfertaALaSala(Ofertum oferta, int idSala) {

            var sala =  _subastaContext.Salas.Include(s => s.Oferta).FirstOrDefault(s => s.Id == idSala);
            if (sala == null) throw new Exception("Sala no encontrada");
            sala.Oferta.Add(oferta);
            _subastaContext.SaveChanges();
        }
    }
}
