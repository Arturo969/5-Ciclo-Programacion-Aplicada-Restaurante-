using System;
using System.Collections.Generic;

namespace SaborCajabambino.Models;

public partial class ProductoIngrediente
{
    public int IdProducto { get; set; }

    public int IdItem { get; set; }

    public decimal Cantidad { get; set; }

    public virtual Inventario IdItemNavigation { get; set; } = null!;

    public virtual Producto IdProductoNavigation { get; set; } = null!;
}
