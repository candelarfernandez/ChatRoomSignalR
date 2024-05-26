﻿using System;
using System.Collections.Generic;

namespace ChatRoom.Datos.Entidades;

public partial class Usuario
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public decimal? DineroDisponible { get; set; }

    public virtual ICollection<Ofertum> Oferta { get; set; } = new List<Ofertum>();

    public virtual ICollection<Sala> Salas { get; set; } = new List<Sala>();

    public virtual ICollection<Ventum> VentumIdCompradorNavigations { get; set; } = new List<Ventum>();

    public virtual ICollection<Ventum> VentumIdVendedorNavigations { get; set; } = new List<Ventum>();
}