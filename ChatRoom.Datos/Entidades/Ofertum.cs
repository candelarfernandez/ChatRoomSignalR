using System;
using System.Collections.Generic;

namespace ChatRoom.Datos.Entidades;

public partial class Ofertum
{
    public int Id { get; set; }

    public decimal Monto { get; set; }

    public int? IdComprador { get; set; }

    public int? IdSala { get; set; }

    public virtual Usuario? IdCompradorNavigation { get; set; }

    public virtual Sala? IdSalaNavigation { get; set; }
}
