using ChatRoom.Datos.Entidades;


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
        private static List<Ofertum> Ofertums = new List<Ofertum>
    {
    };

        public  List<Ofertum> GetOfertums()
        {
            return  Ofertums;
        }

        public Ofertum? GetOfertumById(int id)
        {
            return Ofertums.FirstOrDefault(o => o.Id == id);
        }

        public Ofertum CreateOfertum(decimal monto, int? idComprador, int? idSala)
        {
            int newOfertumId = Ofertums.Count + 1;
            var Ofertum = new Ofertum
            {
                Id = newOfertumId,
                Monto = monto,
                IdComprador = idComprador,
                IdSala = idSala
            };
            Ofertums.Add(Ofertum);
            return Ofertum;
        }
        public Ofertum? getOfertaMax(int idSala)
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

