using System;
using System.Collections.Generic;

namespace SaborCajabambino.Models;

public partial class Empleado
{
    public int IdEmpleado { get; set; }

    public string NombreCompleto { get; set; } = null!;

    public string Dni { get; set; } = null!;

    public DateOnly? FechaNacimiento { get; set; }

    public string? Direccion { get; set; }

    public string? Telefono { get; set; }

    public string? CorreoElectronico { get; set; }

    public string Rol { get; set; } = null!;

    public string Turno { get; set; } = null!;

    public DateOnly? FechaContratacion { get; set; }

    public decimal? Salario { get; set; }

    public string? Estado { get; set; }

    public string Usuario { get; set; } = null!;

    public string Contrasena { get; set; } = null!;

    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
}
