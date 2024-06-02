using ChatRoom.Datos.Entidades;


namespace ChatRoom.Dominio
{
    public interface IOfertumService
    {
        List<Oferta> GetOfertums();
        Oferta? GetOfertumById(int id);
        Oferta CreateOfertum(decimal monto, string? idComprador, int? idSala);
    }
    public class OfertumService : IOfertumService
    {
        private static List<Oferta> Ofertums = new List<Oferta>
    {
    };

        public  List<Oferta> GetOfertums()
        {
            return  Ofertums;
        }

        public Oferta? GetOfertumById(int id)
        {
            return Ofertums.FirstOrDefault(o => o.Id == id);
        }

        public Oferta CreateOfertum(decimal monto, string? idComprador, int? idSala)
        {
            int newOfertumId = Ofertums.Count + 1;
            var Ofertum = new Oferta
            {
                Id = newOfertumId,
                Monto = monto,
                IdComprador = idComprador,
                IdSala = idSala
            };
            Ofertums.Add(Ofertum);
            return Ofertum;
        }
        public Oferta? getOfertaMax(int idSala)
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
        }



    }
}

