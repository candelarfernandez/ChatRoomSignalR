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
        Venta FinalizarSubasta(int idSala);
        Usuario GetUserById(string userId);
        Producto GetProductoById(int productoId);

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
                IdVendedor = idVendedor,
                Activa = true
            };

            var producto = new Producto
            {
                Nombre = $"{nombre}",

            };

            _subastaContext.Productos.Add(producto);
            _subastaContext.SaveChanges();
            sala.IdProducto = producto.Id;

            _subastaContext.Salas.Add(sala);
            _subastaContext.SaveChanges();
            return sala;
        }
        public void agregarOfertaALaSala(Ofertum oferta, int idSala) {

            var sala =  _subastaContext.Salas.Include(s => s.Oferta).FirstOrDefault(s => s.Id == idSala);
            var ultimaOferta = sala.Oferta.OrderByDescending(o => o.Monto).FirstOrDefault();
           /* quisimos hacer condicion que cuando no deposito, el dinero disponible es 0, que no te deje ofertar, 
           actualmente te deja si vos no depositaste. si vos no ofertaste, y depositas, ahi te funciona la logica, 
           sino ya se rompe todo y te deja ofertar.
           var comprador = _subastaContext.Usuarios.FirstOrDefault(c => c.Id == oferta.IdComprador);*/
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

        public Venta FinalizarSubasta(int idSala)
        {
            var sala = _subastaContext.Salas.Include(s => s.Oferta).FirstOrDefault(s => s.Id == idSala);
            sala.Activa = false;

            var ultimaOferta = sala.Oferta.OrderByDescending(o => o.Monto).FirstOrDefault();
            var usuario = _subastaContext.Usuarios.Find(ultimaOferta.IdComprador);

            if (ultimaOferta == null)
            {
                throw new Exception("No hay ofertas en la sala");
            }
            decimal dineroDisponibleActual = usuario.DineroDisponible ?? 0;
                usuario.DineroDisponible = dineroDisponibleActual - (decimal)ultimaOferta.Monto;
                _subastaContext.SaveChanges();

            var venta = new Venta
            {
                IdComprador = ultimaOferta.IdComprador,
                IdVendedor = sala.IdVendedor,
                IdProducto = sala.IdProducto,
                Monto = ultimaOferta.Monto
            };

            _subastaContext.Venta.Add(venta);
            _subastaContext.SaveChanges();

            return venta;
        }

        public Usuario GetUserById(string userId)
        {
           return _subastaContext.Usuarios.Find(userId);

        }

        public Producto GetProductoById(int productoId)
        {
            return _subastaContext.Productos.Find(productoId);
        }

    }
}
