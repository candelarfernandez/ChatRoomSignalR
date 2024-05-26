using System;
using System.Collections.Generic;

namespace ChatRoom.Datos.Entidades;

public partial class Notificacion
{
    public int Id { get; set; }

    public string Mensaje { get; set; } = null!;

    public int IdVenta { get; set; }

    public virtual Ventum IdVentaNavigation { get; set; } = null!;
}
