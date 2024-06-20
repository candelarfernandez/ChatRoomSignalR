using ChatRoom.Datos;
using ChatRoom.Datos.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatRoom.Dominio
{
    public interface IUsuarioService
    {
        void depositar(string id, double monto);
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
        public void depositar(string id, double monto)
        {
            {
                var usuario = _subastaContext.Usuarios.Find(id);

                if (usuario != null)
                {
                    decimal dineroDisponibleActual = usuario.DineroDisponible ?? 0;

                    usuario.DineroDisponible = dineroDisponibleActual + (decimal)monto; 
                    _subastaContext.SaveChanges();
                }
                
            }
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
