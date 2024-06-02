using ChatRoom.Datos;
using ChatRoom.Datos.Entidades;
using Microsoft.EntityFrameworkCore;


namespace ChatRoom.Dominio
{
    public interface IOfertumService
    {
        List<Ofertum> GetOfertums();
        Ofertum? GetOfertumById(int id);
        Ofertum CreateOfertum(decimal monto, int? idComprador, int? idSala);
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

        public Ofertum CreateOfertum(decimal monto, int? idComprador, int? idSala)
        {
            var ofertum = new Ofertum
            {
                Monto = monto,
                IdComprador = idComprador,
                IdSala = idSala
            };

            _subastaContext.Oferta.Add(ofertum);
            _subastaContext.SaveChanges();

            return ofertum;
        }
        /*public Ofertum? getOfertaMax(int idSala)
        {
            var ofertasEnSala = Ofertums.Where(o => o.IdSala == idSala);

            if (ofertasEnSala.Any())
            {
                var ofertaMaxima = ofertasEnSala.OrderByDescending(o => o.Monto).First();
                return ofertaMaxima;
            }
            else
            {
                return null; 
            }
        }*/



    }
}

