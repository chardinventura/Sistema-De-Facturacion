namespace SistemaFacturacion.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ModelSF : DbContext
    {
        public ModelSF()
            : base("name=ModelSF")
        {
        }

        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<Compra> Compras { get; set; }
        public virtual DbSet<EntradaMercancia> EntradaMercancias { get; set; }
        public virtual DbSet<Facturacion> Facturacions { get; set; }
        public virtual DbSet<Producto> Productos { get; set; }
        public virtual DbSet<Proveedore> Proveedores { get; set; }
        public virtual DbSet<Stock> Stocks { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>()
                .Property(e => e.Cedula)
                .IsUnicode(false);

            modelBuilder.Entity<Cliente>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Cliente>()
                .Property(e => e.Telefono)
                .IsUnicode(false);

            modelBuilder.Entity<Cliente>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Cliente>()
                .Property(e => e.Categoria)
                .IsUnicode(false);

            modelBuilder.Entity<Cliente>()
                .HasMany(e => e.Compras)
                .WithOptional(e => e.Cliente)
                .HasForeignKey(e => e.cliente_id);

            modelBuilder.Entity<Cliente>()
                .HasMany(e => e.Facturacions)
                .WithOptional(e => e.Cliente)
                .HasForeignKey(e => e.cliente_id);

            modelBuilder.Entity<Facturacion>()
                .Property(e => e.Descuento)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Facturacion>()
                .Property(e => e.Monto)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Producto>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Producto>()
                .Property(e => e.Precio)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Producto>()
                .HasMany(e => e.Compras)
                .WithOptional(e => e.Producto)
                .HasForeignKey(e => e.producto_id);

            modelBuilder.Entity<Producto>()
                .HasMany(e => e.EntradaMercancias)
                .WithOptional(e => e.Producto)
                .HasForeignKey(e => e.producto_id);

            modelBuilder.Entity<Producto>()
                .HasMany(e => e.Stocks)
                .WithOptional(e => e.Producto)
                .HasForeignKey(e => e.producto_id);

            modelBuilder.Entity<Proveedore>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Proveedore>()
                .Property(e => e.Cedula)
                .IsUnicode(false);

            modelBuilder.Entity<Proveedore>()
                .Property(e => e.Telefono)
                .IsUnicode(false);

            modelBuilder.Entity<Proveedore>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Proveedore>()
                .HasMany(e => e.EntradaMercancias)
                .WithOptional(e => e.Proveedore)
                .HasForeignKey(e => e.proveedor_id);

            modelBuilder.Entity<Proveedore>()
                .HasMany(e => e.Productos)
                .WithOptional(e => e.Proveedore)
                .HasForeignKey(e => e.proveedor_id);
        }
    }
}
