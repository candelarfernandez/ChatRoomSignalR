using System;
using System.Collections.Generic;

namespace ChatRoom.Datos.Entidades;

public partial class Sala
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? FotoProductoNombre { get; set; }

    public string? IdVendedor { get; set; }

    public int IdProducto { get; set; }

    public string? UsuariosConectados { get; set; }

    public bool? Activa { get; set; }

    public long? TiempoRestante { get; set; }

    public DateTime? HoraFinalizacion { get; set; }

    public DateTime? HoraUltimaOferta { get; set; }

    public virtual Usuario? IdVendedorNavigation { get; set; }

    public virtual Producto? IdProductoNavigation { get; set; }

    public virtual ICollection<Ofertum> Oferta { get; set; } = new List<Ofertum>();
}
