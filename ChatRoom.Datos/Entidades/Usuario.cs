using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace ChatRoom.Datos.Entidades;

public partial class Usuario : IdentityUser
{
    //public int Id { get; set; }

    //public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public decimal? DineroDisponible { get; set; }

    public virtual ICollection<Oferta> Oferta { get; set; } = new List<Oferta>();

    public virtual ICollection<Sala> Salas { get; set; } = new List<Sala>();

    public virtual ICollection<Venta> VentumIdCompradorNavigations { get; set; } = new List<Venta>();

    public virtual ICollection<Venta> VentumIdVendedorNavigations { get; set; } = new List<Venta>();
}
