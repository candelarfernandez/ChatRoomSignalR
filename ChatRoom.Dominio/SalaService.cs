using ChatRoom.Datos;
using ChatRoom.Datos.Entidades;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace ChatRoom.Dominio
{
    public interface ISalaService
    {
        List<Sala> GetSalas();
        Sala? GetSalaById(int id);
        Sala CreateSala(string nombre, string? fotoProductoNombre, string? idVendedor);
        void agregarOfertaALaSala(Oferta oferta, int idSala);

    }
    public class SalaService : ISalaService
    {
        private static List<Sala> Salas = new List<Sala>
    {
        new Sala { Id = 1, Nombre = "Cervezas", FotoProductoNombre = "cerveza.jpg" },
        new Sala { Id = 2, Nombre = "Programacion", FotoProductoNombre = "libro.jpg" },
        new Sala { Id = 3, Nombre = "Moda", FotoProductoNombre = "chaqueta.jpg" }
    };
        private SubastaContext _subastaContext;

        public SalaService(SubastaContext subastaContext) {
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
        public void agregarOfertaALaSala(Oferta oferta, int idSala) {

            var sala = GetSalaById(idSala);
            if (sala != null)
            {
                sala.Oferta.Add(oferta);
                _subastaContext.SaveChanges();
            }
        }
    }
}
