using ChatRoom.Datos;
using ChatRoom.Datos.Entidades;
using Microsoft.EntityFrameworkCore;


namespace ChatRoom.Dominio
{
    public interface IOfertumService
    {
        List<Ofertum> GetOfertums();
        Ofertum? GetOfertumById(int id);
        Ofertum CreateOfertum(decimal monto, string? idComprador, int? idSala);
    }
    public class OfertumService : IOfertumService
    {
        private SubastaContext _subastaContext;

        public OfertumService(SubastaContext subastaContext) {
            _subastaContext = subastaContext;
        }

        public  List<Ofertum> GetOfertums()
        {
            return _subastaContext.Oferta.Include(o => o.IdCompradorNavigation).Include(o => o.IdSalaNavigation).ToList();
        }

        public Ofertum? GetOfertumById(int id)
        {
            return _subastaContext.Oferta.Include(o => o.IdCompradorNavigation).Include(o => o.IdSalaNavigation).FirstOrDefault(o => o.Id == id);
        }

        public Ofertum CreateOfertum(decimal monto, string? idComprador, int? idSala)
        {
            var usuarioComprador = _subastaContext.Usuarios.Find(idComprador);

            if (usuarioComprador.DineroDisponible < monto)
            {
                throw new InvalidOperationException("El usuario comprador no tiene suficiente dinero disponible.");
            }

            var ofertum = new Ofertum
            {
                Monto = monto,
                IdComprador = idComprador,
                IdSala = idSala
            };
            return ofertum;
        }
     

    }
}

