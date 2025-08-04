using System;
using System.Collections.Generic;

namespace SaborCajabambino.Models;

public partial class Pedido
{
    public int IdPedido { get; set; }

    public DateOnly Fecha { get; set; }

    public TimeOnly Hora { get; set; }

    public string Estado { get; set; } = null!;

    public int? IdMesa { get; set; }

    public int? IdCliente { get; set; }

    public int? IdEmpleado { get; set; }

    public string? TipoPedido { get; set; }

    public string? DireccionEntrega { get; set; }

    public string? Comentarios { get; set; }

    public virtual ICollection<DetallePedido> DetallePedidos { get; set; } = new List<DetallePedido>();

    public virtual Cliente? IdClienteNavigation { get; set; }

    public virtual Empleado? IdEmpleadoNavigation { get; set; }

    public virtual Mesa? IdMesaNavigation { get; set; }
}
