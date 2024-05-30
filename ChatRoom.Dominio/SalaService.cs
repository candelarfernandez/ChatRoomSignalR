using ChatRoom.Datos.Entidades;

namespace ChatRoom.Dominio
{
    public interface ISalaService
    {
        List<Sala> GetSalas();
        Sala? GetSalaById(int id);
        Sala CreateSala(string nombre, string? fotoProductoNombre, int? idVendedor);
    }
    public class SalaService : ISalaService
    {
        private static List<Sala> Salas = new List<Sala>
    {
        new Sala { Id = 1, Nombre = "Cervezas", FotoProductoNombre = "cerveza.jpg" },
        new Sala { Id = 2, Nombre = "Programacion", FotoProductoNombre = "libro.jpg" },
        new Sala { Id = 3, Nombre = "Moda", FotoProductoNombre = "chaqueta.jpg" }
    };

        public List<Sala> GetSalas()
        {
            return Salas;
        }

        public Sala? GetSalaById(int id)
        {
            return Salas.FirstOrDefault(s => s.Id == id);
        }

        public Sala CreateSala(string nombre, string? fotoProductoNombre, int? idVendedor)
        {
            int newRoomId = Salas.Count + 1;
            var sala = new Sala
            {
                Id = newRoomId,
                Nombre = nombre,
                FotoProductoNombre = fotoProductoNombre,
                IdVendedor = idVendedor
            };
            Salas.Add(sala);
            return sala;
        }

    }
}
