using ChatRoom.Datos;
using ChatRoom.Datos.Entidades;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ChatRoom.Dominio
{
    public interface ISalaService
    {
        List<Sala> GetSalas();
        Sala? GetSalaById(int id);
        Sala CreateSala(string nombre, string? fotoProductoNombre, string? idVendedor);
        void agregarOfertaALaSala(Ofertum oferta, int idSala);
        Venta FinalizarSubasta(int idSala, bool forceClose);
        Usuario GetUserById(string userId);
        Producto GetProductoById(int productoId);

    }
    public class SalaService : ISalaService
    {
        private SubastaContext _subastaContext;

        public SalaService(SubastaContext subastaContext, IOfertumService ofertumService)
        {
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
            try
            {
                if (idVendedor == null)
                {
                    throw new ArgumentException("El ID del vendedor no puede ser nulo.");
                }
                if (GetSalaActivaByNombre(nombre) != null)
                {
                    throw new InvalidOperationException("Ya existe una sala con ese nombre.");
                }

                var producto = new Producto
                {
                    Nombre = $"{nombre}",

                };

                _subastaContext.Productos.Add(producto);
                _subastaContext.SaveChanges();

                var sala = new Sala
                {
                    Nombre = nombre,
                    FotoProductoNombre = fotoProductoNombre,
                    IdVendedor = idVendedor,
                    Activa = true,
                    IdProducto = producto.Id
                };

                _subastaContext.Salas.Add(sala);
                _subastaContext.SaveChanges();
                return sala;
            } catch (Exception ex)
            {
                throw new Exception($"No se pudo crear la sala: {ex.Message}");
            }
        }

        private Sala GetSalaActivaByNombre(string nombre)
        {
            return _subastaContext.Salas.FirstOrDefault(s => s.Nombre == nombre && s.Activa==true);
        }

        public void agregarOfertaALaSala(Ofertum oferta, int idSala)
        {

            var sala = _subastaContext.Salas.Include(s => s.Oferta).FirstOrDefault(s => s.Id == idSala);
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

        public Venta FinalizarSubasta(int idSala, bool forceClose)
        {
            var sala = _subastaContext.Salas.Include(s => s.Oferta).FirstOrDefault(s => s.Id == idSala);
            var ultimaOferta = sala.Oferta.OrderByDescending(o => o.Monto).FirstOrDefault();

            if (ultimaOferta == null&& !forceClose)
            {
                throw new Exception("No es posible cerrar la sala");
            }

            sala.Activa = false;
            if (ultimaOferta != null)
            {
                var usuario = _subastaContext.Usuarios.Find(ultimaOferta.IdComprador);
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
            // Si no hay ofertas y se fuerza el cierre, simplemente cerrar la sala
            _subastaContext.SaveChanges();
            return null; // Retornar null si no hubo ventas
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
