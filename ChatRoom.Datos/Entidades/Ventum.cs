using System;
using System.Collections.Generic;

namespace ChatRoom.Datos.Entidades;

public partial class Ventum
{
    public int Id { get; set; }

    public int? IdProducto { get; set; }

    public int? IdVendedor { get; set; }

    public int? IdComprador { get; set; }

    public decimal? Monto { get; set; }

    public virtual Usuario? IdCompradorNavigation { get; set; }

    public virtual Producto? IdProductoNavigation { get; set; }

    public virtual Usuario? IdVendedorNavigation { get; set; }

    public virtual ICollection<Notificacion> Notificacions { get; set; } = new List<Notificacion>();
}
