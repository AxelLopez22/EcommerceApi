using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ecommerceApi.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ecommerceApi.Context
{
    public partial class RepositoryContext : IdentityDbContext
    {
        public RepositoryContext()
        {
        }

        public RepositoryContext(DbContextOptions<RepositoryContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Categorium> Categoria { get; set; }
        public virtual DbSet<Compra> Compras { get; set; }
        public virtual DbSet<DetalleCompra> DetalleCompras { get; set; }
        public virtual DbSet<DetalleVentum> DetalleVenta { get; set; }
        public virtual DbSet<Inventario> Inventarios { get; set; }
        public virtual DbSet<Producto> Productos { get; set; }
        public virtual DbSet<Proveedore> Proveedores { get; set; }
        public virtual DbSet<Ventum> Venta { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseSqlServer("Server=LAPTOP-BMJ3PPCK;Database=Ecommerce;Trusted_Connection=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

           
            modelBuilder.Entity<Categorium>(entity =>
            {
                entity.HasKey(e => e.IdCategoria)
                    .HasName("PK__Categori__A3C02A105C8931F9");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Compra>(entity =>
            {
                entity.HasKey(e => e.IdCompra)
                    .HasName("PK__Compras__0A5CDB5CE3AB252A");

                entity.Property(e => e.FechaCompra).HasColumnType("datetime");

                entity.Property(e => e.IdUsuario).HasMaxLength(450);

                entity.HasOne(d => d.IdProveedorNavigation)
                    .WithMany(p => p.Compras)
                    .HasForeignKey(d => d.IdProveedor)
                    .HasConstraintName("Fk_Compras_Refe_Proveedor");

            });

            modelBuilder.Entity<DetalleCompra>(entity =>
            {
                entity.HasKey(e => e.IdDetalle)
                    .HasName("PK__DetalleC__E43646A53BE94F24");

                entity.ToTable("DetalleCompra");

                entity.HasOne(d => d.IdCompraNavigation)
                    .WithMany(p => p.DetalleCompras)
                    .HasForeignKey(d => d.IdCompra)
                    .HasConstraintName("Fk_DetalleCompra_Refe_Compra");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.DetalleCompras)
                    .HasForeignKey(d => d.IdProducto)
                    .HasConstraintName("Fk_DetalleCompra_Refe_Producto");
            });

            modelBuilder.Entity<DetalleVentum>(entity =>
            {
                entity.HasKey(e => e.IdDetalle)
                    .HasName("PK__DetalleV__E43646A563B2D7C2");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.DetalleVenta)
                    .HasForeignKey(d => d.IdProducto)
                    .HasConstraintName("Fk_DetalleVenta_Refe_Producto");

                entity.HasOne(d => d.IdVentaNavigation)
                    .WithMany(p => p.DetalleVenta)
                    .HasForeignKey(d => d.IdVenta)
                    .HasConstraintName("Fk_DetalleVenta_Refe_Venta");
            });

            modelBuilder.Entity<Inventario>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Inventario");

                entity.Property(e => e.Categoria)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.ImagenUrl)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.NombreProducto)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasKey(e => e.IdProductos)
                    .HasName("PK__Producto__718C7D07D8BDFBFD");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.ImagenUrl)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.NombreProducto)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdCategoriaNavigation)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => d.IdCategoria)
                    .HasConstraintName("Fk_Productos_Refe_Categoria");
            });

            modelBuilder.Entity<Proveedore>(entity =>
            {
                entity.HasKey(e => e.IdProveedor)
                    .HasName("PK__Proveedo__E8B631AF3409476C");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.RazonSocial)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Ruc)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Ventum>(entity =>
            {
                entity.HasKey(e => e.IdVenta)
                    .HasName("PK__Venta__BC1240BDC7C4C99C");

                entity.Property(e => e.FechaVenta).HasColumnType("datetime");

                entity.Property(e => e.IdUsuario).HasMaxLength(450);

            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
