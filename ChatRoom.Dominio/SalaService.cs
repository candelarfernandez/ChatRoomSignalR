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
        void FinalizarSubasta(int idSala);
    }
    public class SalaService : ISalaService
    {
        private SubastaContext _subastaContext;

        public SalaService(SubastaContext subastaContext, IOfertumService ofertumService) {
            _subastaContext = subastaContext;
        }

        public List<Sala> GetSalas()
        {
            return _subastaContext.Salas
        .Include(s => s.Oferta)
        .Where(s => s.Activa == true)
        .ToList();

        }
        public Sala? GetSalaById(int id)
        {
            return _subastaContext.Salas.Include(s => s.Oferta).FirstOrDefault(s => s.Id == id);
        }

        public Sala CreateSala(string nombre, string? fotoProductoNombre, string? idVendedor)
        {
            if (idVendedor == null)
            {
                throw new Exception("El ID del vendedor no puede ser nulo.");
            }
            var sala = new Sala
            {
                Nombre = nombre,
                FotoProductoNombre = fotoProductoNombre,
                IdVendedor = idVendedor
            };

            var producto = new Producto
            {
                Nombre = $"{nombre} - {fotoProductoNombre}",

            };

            _subastaContext.Productos.Add(producto);

            sala.IdProducto = producto.Id;

            _subastaContext.Salas.Add(sala);
            _subastaContext.SaveChanges();
            return sala;
        }
        public void agregarOfertaALaSala(Ofertum oferta, int idSala) {

            var sala =  _subastaContext.Salas.Include(s => s.Oferta).FirstOrDefault(s => s.Id == idSala);
            var ultimaOferta = sala.Oferta.OrderByDescending(o => o.Monto).FirstOrDefault();
            if (ultimaOferta == null || oferta.Monto > ultimaOferta.Monto)
            {
                sala.Oferta.Add(oferta);
                _subastaContext.SaveChanges();
            }
            else
            {
                throw new Exception("La oferta debe ser mayor a la anterior");
            }
        }

        public void FinalizarSubasta(int idSala)
        {
            var sala = _subastaContext.Salas.Include(s => s.Oferta).FirstOrDefault(s => s.Id == idSala);
            sala.Activa = false;

            var ultimaOferta = sala.Oferta.OrderByDescending(o => o.Monto).FirstOrDefault();

            if (ultimaOferta == null)
            {
                throw new Exception("No hay ofertas en la sala");
            }

            var venta = new Venta
            {
                IdComprador = ultimaOferta.IdComprador,
                IdVendedor = sala.IdVendedor,
                IdProducto = sala.IdProducto,
                Monto = ultimaOferta.Monto
            };

        _subastaContext.SaveChanges();
        }
    }
}
