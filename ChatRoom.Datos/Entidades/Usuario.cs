using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace ChatRoom.Datos.Entidades;

public partial class Usuario : IdentityUser
{
    //public override string? Id { get; set; }

    //public override string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public override string Email { get; set; } = null!;

    public decimal? DineroDisponible { get; set; }

    public virtual ICollection<Ofertum> Oferta { get; set; } = new List<Ofertum>();

    public virtual ICollection<Sala> Salas { get; set; } = new List<Sala>();

    public virtual ICollection<Ventum> VentumIdCompradorNavigations { get; set; } = new List<Ventum>();

    public virtual ICollection<Ventum> VentumIdVendedorNavigations { get; set; } = new List<Ventum>();
}
