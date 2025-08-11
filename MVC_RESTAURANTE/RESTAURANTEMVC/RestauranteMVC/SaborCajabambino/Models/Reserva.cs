using System;
using System.Collections.Generic;

namespace SaborCajabambino.Models;

public partial class Reserva
{
    public int IdReserva { get; set; }
    public DateOnly Fecha { get; set; }
    public TimeOnly Hora { get; set; }
    public int? IdMesa { get; set; }
    public int? IdCliente { get; set; }
    public int NumeroPersonas { get; set; }
    public string? Estado { get; set; }
    public string? Comentarios { get; set; }
    public virtual Cliente? IdClienteNavigation { get; set; }
    public virtual Mesa? IdMesaNavigation { get; set; }
}
