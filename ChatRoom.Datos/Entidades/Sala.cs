using System;
using System.Collections.Generic;

namespace ChatRoom.Datos.Entidades;

public partial class Sala
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? FotoProductoNombre { get; set; }

    public string? IdVendedor { get; set; }

    public string? UsuariosConectados { get; set; }

    public virtual Usuario? IdVendedorNavigation { get; set; }

    public virtual ICollection<Oferta> Oferta { get; set; } = new List<Oferta>();
}
