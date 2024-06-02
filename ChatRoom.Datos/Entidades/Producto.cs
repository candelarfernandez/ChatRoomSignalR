using System;
using System.Collections.Generic;

namespace ChatRoom.Datos.Entidades;

public partial class Producto
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public decimal? PrecioFinal { get; set; }

    public virtual ICollection<Venta> Venta { get; set; } = new List<Venta>();
}
