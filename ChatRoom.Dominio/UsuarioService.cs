using ChatRoom.Datos;
using ChatRoom.Datos.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatRoom.Dominio
{
    public interface IUsuarioService
    {
        List<Venta> GetCompras(string id);
        List<Venta> GetVentas(string id);
    }
    public class UsuarioService : IUsuarioService
    {
        private SubastaContext _subastaContext;

        public UsuarioService(SubastaContext subastaContext)
        {
            _subastaContext = subastaContext;
        }

        public List<Venta> GetCompras(string id)
        {
            var compras = _subastaContext.Venta
                          .Where(v => v.IdComprador == id)
                          .ToList();

            return compras;

        }
        public List<Venta> GetVentas(string id)
        {
            var compras = _subastaContext.Venta
                          .Where(v => v.IdVendedor == id)
                          .ToList();

            return compras;

        }
    }
}
