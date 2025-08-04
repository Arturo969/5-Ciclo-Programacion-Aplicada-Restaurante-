using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SaborCajabambino.Models;

namespace SaborCajabambino.Data;

public partial class RestauranteProgramacionIiContext : DbContext
{
    public RestauranteProgramacionIiContext()
    {
    }

    public RestauranteProgramacionIiContext(DbContextOptions<RestauranteProgramacionIiContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categoria> Categoria { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<DetallePedido> DetallePedidos { get; set; }

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<Inventario> Inventarios { get; set; }

    public virtual DbSet<ItemCategorium> ItemCategoria { get; set; }

    public virtual DbSet<Mesa> Mesas { get; set; }

    public virtual DbSet<Pedido> Pedidos { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<ProductoIngrediente> ProductoIngredientes { get; set; }

    public virtual DbSet<Reserva> Reservas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=RestauranteProgramacionII;User ID=sa;Password=sql123;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.IdCategoria).HasName("PK__Categori__CB90334998DB2029");

            entity.ToTable("Categoria", "GENERAL");

            entity.HasIndex(e => e.Nombre, "UQ__Categori__75E3EFCF532F5563").IsUnique();

            entity.Property(e => e.IdCategoria).HasColumnName("Id_Categoria");
            entity.Property(e => e.Nombre).HasMaxLength(50);
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente).HasName("PK__Cliente__3DD0A8CBB24054EB");

            entity.ToTable("Cliente", "CLIENTE");

            entity.HasIndex(e => e.CorreoElectronico, "UQ__Cliente__531402F3C82B9C5A").IsUnique();

            entity.HasIndex(e => e.Dni, "UQ__Cliente__C035B8DD85E5C672").IsUnique();

            entity.Property(e => e.IdCliente).HasColumnName("Id_Cliente");
            entity.Property(e => e.ApellidoMaterno).HasMaxLength(100);
            entity.Property(e => e.ApellidoPaterno).HasMaxLength(100);
            entity.Property(e => e.CorreoElectronico).HasMaxLength(100);
            entity.Property(e => e.Dni)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("DNI");
            entity.Property(e => e.Nombres).HasMaxLength(100);
            entity.Property(e => e.Telefono).HasMaxLength(15);
        });

        modelBuilder.Entity<DetallePedido>(entity =>
        {
            entity.HasKey(e => e.IdDetalle).HasName("PK__DetalleP__9274780BEBAA4D3E");

            entity.ToTable("DetallePedido", "TRANSACCION");

            entity.Property(e => e.IdDetalle).HasColumnName("Id_Detalle");
            entity.Property(e => e.IdPedido).HasColumnName("Id_Pedido");
            entity.Property(e => e.IdProducto).HasColumnName("Id_Producto");

            entity.HasOne(d => d.IdPedidoNavigation).WithMany(p => p.DetallePedidos)
                .HasForeignKey(d => d.IdPedido)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetallePedido_Pedido");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.DetallePedidos)
                .HasForeignKey(d => d.IdProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetallePedido_Producto");
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.IdEmpleado).HasName("PK__Empleado__74056223068BCAAD");

            entity.ToTable("Empleado", "PERSONAL");

            entity.HasIndex(e => e.CorreoElectronico, "UQ__Empleado__531402F3BE32A4A4").IsUnique();

            entity.HasIndex(e => e.Dni, "UQ__Empleado__C035B8DD3F25C468").IsUnique();

            entity.HasIndex(e => e.Usuario, "UQ__Empleado__E3237CF7FE6BBD19").IsUnique();

            entity.Property(e => e.IdEmpleado).HasColumnName("Id_Empleado");
            entity.Property(e => e.Contrasena).HasMaxLength(255);
            entity.Property(e => e.CorreoElectronico).HasMaxLength(100);
            entity.Property(e => e.Dni)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("DNI");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .HasDefaultValue("Activo");
            entity.Property(e => e.NombreCompleto).HasMaxLength(200);
            entity.Property(e => e.Rol).HasMaxLength(50);
            entity.Property(e => e.Salario).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Telefono).HasMaxLength(15);
            entity.Property(e => e.Turno).HasMaxLength(50);
            entity.Property(e => e.Usuario).HasMaxLength(50);
        });

        modelBuilder.Entity<Inventario>(entity =>
        {
            entity.HasKey(e => e.IdItem).HasName("PK__Inventar__F325AAC4B8C1A4E7");

            entity.ToTable("Inventario", "INVENTARIO");

            entity.HasIndex(e => e.ItemNombre, "UQ__Inventar__68F4921C3F3F5D7B").IsUnique();

            entity.Property(e => e.IdItem).HasColumnName("Id_Item");
            entity.Property(e => e.CantidadReorden).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.CostoPorUnidad).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.IdItemCategoria).HasColumnName("id_ItemCategoria");
            entity.Property(e => e.ItemNombre).HasMaxLength(100);
            entity.Property(e => e.NecesitaReorden).HasDefaultValue(false);
            entity.Property(e => e.NivelReorden).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Stock).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.UnidadMedida).HasMaxLength(20);

            entity.HasOne(d => d.IdItemCategoriaNavigation).WithMany(p => p.Inventarios)
                .HasForeignKey(d => d.IdItemCategoria)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_inventarioCategoria");
        });

        modelBuilder.Entity<ItemCategorium>(entity =>
        {
            entity.HasKey(e => e.IdItemCategoria).HasName("PK__ItemCate__34EB96A2453642A7");

            entity.ToTable("ItemCategoria", "INVENTARIO");

            entity.Property(e => e.IdItemCategoria).HasColumnName("Id_ItemCategoria");
            entity.Property(e => e.Categoria).HasMaxLength(255);
        });

        modelBuilder.Entity<Mesa>(entity =>
        {
            entity.HasKey(e => e.IdMesa).HasName("PK__Mesa__F6BC977E0359B5EF");

            entity.ToTable("Mesa", "GENERAL");

            entity.Property(e => e.IdMesa).HasColumnName("Id_Mesa");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .HasDefaultValue("Disponible");
            entity.Property(e => e.Ubicacion).HasMaxLength(50);
        });

        modelBuilder.Entity<Pedido>(entity =>
        {
            entity.HasKey(e => e.IdPedido).HasName("PK__Pedido__A5D00139EABDF100");

            entity.ToTable("Pedido", "TRANSACCION");

            entity.Property(e => e.IdPedido).HasColumnName("Id_Pedido");
            entity.Property(e => e.Estado).HasMaxLength(20);
            entity.Property(e => e.Hora).HasPrecision(0);
            entity.Property(e => e.IdCliente).HasColumnName("Id_Cliente");
            entity.Property(e => e.IdEmpleado).HasColumnName("Id_Empleado");
            entity.Property(e => e.IdMesa).HasColumnName("Id_Mesa");
            entity.Property(e => e.TipoPedido).HasMaxLength(20);

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.IdCliente)
                .HasConstraintName("FK_Pedido_Cliente");

            entity.HasOne(d => d.IdEmpleadoNavigation).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.IdEmpleado)
                .HasConstraintName("FK_Pedido_Empleado");

            entity.HasOne(d => d.IdMesaNavigation).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.IdMesa)
                .HasConstraintName("FK_Pedido_Mesa");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProducto).HasName("PK__Producto__2085A9CF0BACD25F");

            entity.ToTable("Producto", "GENERAL");

            entity.Property(e => e.IdProducto).HasColumnName("Id_Producto");
            entity.Property(e => e.EsPreparado).HasDefaultValue(true);
            entity.Property(e => e.IdCategoria).HasColumnName("Id_Categoria");
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Precio).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdCategoria)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Producto_Categoria");
        });

        modelBuilder.Entity<ProductoIngrediente>(entity =>
        {
            entity.HasKey(e => new { e.IdProducto, e.IdItem }).HasName("PK__Producto__7FB7F3639EC5B088");

            entity.ToTable("ProductoIngrediente", "GENERAL");

            entity.Property(e => e.IdProducto).HasColumnName("Id_Producto");
            entity.Property(e => e.IdItem).HasColumnName("Id_Item");
            entity.Property(e => e.Cantidad).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdItemNavigation).WithMany(p => p.ProductoIngredientes)
                .HasForeignKey(d => d.IdItem)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductoIngrediente_Inventario");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.ProductoIngredientes)
                .HasForeignKey(d => d.IdProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductoIngrediente_Producto");
        });

        modelBuilder.Entity<Reserva>(entity =>
        {
            entity.HasKey(e => e.IdReserva).HasName("PK__Reserva__9E953BE107559C70");

            entity.ToTable("Reserva", "CLIENTE");

            entity.Property(e => e.IdReserva).HasColumnName("Id_Reserva");
            entity.Property(e => e.Estado).HasMaxLength(20);
            entity.Property(e => e.Hora).HasPrecision(0);
            entity.Property(e => e.IdCliente).HasColumnName("Id_Cliente");
            entity.Property(e => e.IdMesa).HasColumnName("Id_Mesa");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.IdCliente)
                .HasConstraintName("FK_Reserva_Cliente");

            entity.HasOne(d => d.IdMesaNavigation).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.IdMesa)
                .HasConstraintName("FK_Reserva_Mesa");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
