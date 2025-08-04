using System;
using System.Collections.Generic;

namespace SaborCajabambino.Models;

public partial class Inventario
{
    public int IdItem { get; set; }

    public string ItemNombre { get; set; } = null!;

    public int IdItemCategoria { get; set; }

    public string? UnidadMedida { get; set; }

    public decimal? Stock { get; set; }

    public decimal CostoPorUnidad { get; set; }

    public DateOnly? FechaDeExpiracion { get; set; }

    public decimal? NivelReorden { get; set; }

    public decimal? CantidadReorden { get; set; }

    public bool? NecesitaReorden { get; set; }

    public virtual ItemCategorium IdItemCategoriaNavigation { get; set; } = null!;

    public virtual ICollection<ProductoIngrediente> ProductoIngredientes { get; set; } = new List<ProductoIngrediente>();
}
