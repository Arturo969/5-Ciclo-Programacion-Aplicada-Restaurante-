using System;
using System.Collections.Generic;

namespace SaborCajabambino.Models;

public partial class ItemCategorium
{
    public int IdItemCategoria { get; set; }

    public string Categoria { get; set; } = null!;

    public string? Descripcion { get; set; }

    public virtual ICollection<Inventario> Inventarios { get; set; } = new List<Inventario>();
}
