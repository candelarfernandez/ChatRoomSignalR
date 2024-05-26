﻿using System;
using System.Collections.Generic;

namespace ChatRoom.Datos.Entidades;

public partial class Sala
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? FotoProductoNombre { get; set; }

    public int? IdVendedor { get; set; }

    public string? UsuariosConectados { get; set; }

    public virtual Usuario? IdVendedorNavigation { get; set; }

    public virtual ICollection<Ofertum> Oferta { get; set; } = new List<Ofertum>();
}
