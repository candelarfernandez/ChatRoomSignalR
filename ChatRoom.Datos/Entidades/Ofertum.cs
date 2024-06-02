using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ChatRoom.Datos.Entidades;

public partial class Ofertum
{
    public int Id { get; set; }

    public decimal Monto { get; set; }

    public string? IdComprador { get; set; }

    public int? IdSala { get; set; }

    [JsonIgnore]
    public virtual Usuario? IdCompradorNavigation { get; set; }
    [JsonIgnore]

    public virtual Sala? IdSalaNavigation { get; set; }
}
