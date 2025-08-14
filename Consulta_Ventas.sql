WITH ProductoCosto AS (
   SELECT
        p.Id_Producto AS productoid,
        p.EsPreparado, 
        ISNULL(
            SUM(CASE
                WHEN p.EsPreparado = 1 THEN ISNULL(inv_ing.CostoPorUnidad, 0) * ISNULL(pg_ing.Cantidad, 0)
                ELSE 0 
            END),
            0
        ) AS CostoCalculadoIngredientes,
        ISNULL(
            MAX(CASE
                WHEN p.EsPreparado = 0 THEN ISNULL(inv_direct.CostoPorUnidad, 0)
                ELSE 0 
            END),
            0
        ) AS CostoDirectoProducto
    FROM [RestauranteDBasePrueba5m].[GENERAL].[Producto] p
    -- LEFT JOIN a ProductoIngrediente y Inventario para calcular costos de ingredientes (para productos preparados)
    LEFT JOIN [RestauranteDBasePrueba5m].[GENERAL].[ProductoIngrediente] pg_ing ON p.Id_Producto = pg_ing.Id_Producto
    LEFT JOIN [RestauranteDBasePrueba5m].[INVENTARIO].[Inventario] inv_ing ON pg_ing.Id_Item = inv_ing.Id_Item
    -- LEFT JOIN directo a Inventario para obtener el costo de productos no preparados que son ítems de inventario
    LEFT JOIN [RestauranteDBasePrueba5m].[INVENTARIO].[Inventario] inv_direct ON p.Nombre = inv_direct.ItemNombre
    GROUP BY p.Id_Producto, p.EsPreparado
)
SELECT
	  (c.Nombres + c.ApellidoPaterno + c.ApellidoPaterno) AS Cliente,
	   p.Nombre,	   
	   e.NombreCompleto AS Empleado,
	   m.Id_Mesa AS Mesa,
    (p.Precio * dp.Cantidad) AS PrecioVentaTotal,
    dp.Cantidad * (
        CASE
            WHEN pc.EsPreparado = 1 THEN pc.CostoCalculadoIngredientes
            WHEN pc.EsPreparado = 0 THEN pc.CostoDirectoProducto
            ELSE 0
        END
    ) AS CostoTotal,
    (p.Precio * dp.Cantidad) - (
        dp.Cantidad * (
            CASE
                WHEN pc.EsPreparado = 1 THEN pc.CostoCalculadoIngredientes
                WHEN pc.EsPreparado = 0 THEN pc.CostoDirectoProducto
                ELSE 0
            END
        )
    ) AS Margen
FROM [RestauranteDBasePrueba5m].[TRANSACCION].[DetallePedido] dp
INNER JOIN [RestauranteDBasePrueba5m].[GENERAL].[Producto] p ON dp.Id_Producto = p.Id_Producto
LEFT JOIN [ProductoCosto] pc ON dp.Id_Producto = pc.productoid
INNER JOIN [RestauranteDBasePrueba5m].[GENERAL].[Categoria] cat ON p.Id_Categoria = cat.Id_Categoria
INNER JOIN [RestauranteDBasePrueba5m].[TRANSACCION].[Pedido] pe ON dp.Id_Pedido = pe.Id_Pedido
INNER JOIN [RestauranteDBasePrueba5m].[CLIENTE].[Cliente] c ON c.Id_Cliente = pe.Id_Cliente
INNER JOIN [RestauranteDBasePrueba5m].[PERSONAL].[Empleado] e ON e.Id_Empleado = pe.Id_Empleado
INNER JOIN [RestauranteDBasePrueba5m].[GENERAL].[Mesa] m ON m.Id_Mesa = pe.Id_Mesa