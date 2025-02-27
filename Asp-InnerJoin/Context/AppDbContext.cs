using Asp_InnerJoin.Models;
using Microsoft.EntityFrameworkCore;

namespace Asp_InnerJoin.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // DbSet para las entidades
        public DbSet<RolEntity> Roles { get; set; }
        public DbSet<UsuarioEntity> Usuarios { get; set; }
        public DbSet<ProductoEntity> Productos { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de RolEntity
            modelBuilder.Entity<RolEntity>(entity =>
            {
                entity.HasKey(r => r.IdRol);
                entity.Property(r => r.IdRol).ValueGeneratedOnAdd().UseIdentityColumn();
                entity.Property(r => r.NombreRol)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            // Configuración de UsuarioEntity
            modelBuilder.Entity<UsuarioEntity>(entity =>
            {
                entity.HasKey(u => u.IdUsuario);
                entity.Property(r => r.IdUsuario).ValueGeneratedOnAdd().UseIdentityColumn();
                entity.Property(u => u.NombreUsuario)
                    .IsRequired()
                    .HasMaxLength(100);
                entity.Property(u => u.EmailUsuario)
                    .IsRequired()
                    .HasMaxLength(100);
                entity.HasIndex(e => e.EmailUsuario)
                .IsUnique();
                entity.Property(u => u.FechaRegistro)
                    .HasDefaultValueSql("GETDATE()");
                entity.Property(u => u.IdRolUsuario).HasDefaultValue(3);

                // Relación con RolEntity
                entity.HasOne(u => u.Rol)
                    .WithMany(r => r.Usuarios)
                    .HasForeignKey(u => u.IdRolUsuario);



                //Usa Cascade si los datos hijos(productos) no tienen sentido sin el padre(usuario).
                //Evita Cascade si necesitas mantener los datos hijos o si podrían estar relacionados con otros datos importantes. En ese caso, usa Restrict o SetNull.
                /*
                entity.HasOne(u => u.Rol)
                    .WithMany(r => r.Usuarios)
                    .HasForeignKey(u => u.ID_ROL)
                    .OnDelete(DeleteBehavior.Restrict);
                */
            });

            // Configuración de ProductoEntity
            modelBuilder.Entity<ProductoEntity>(entity =>
            {
                entity.HasKey(p => p.IdProducto);
                entity.Property(p => p.IdProducto).ValueGeneratedOnAdd().UseIdentityColumn();
                entity.Property(p => p.NombreProducto)
                    .IsRequired()
                    .HasMaxLength(100);
                entity.Property(p => p.PrecioPRoducto)
                    .HasColumnType("decimal(18,2)");
                entity.Property(p => p.FechaCreacion)
                    .HasDefaultValueSql("GETDATE()");

                // Relación con UsuarioEntity
                entity.HasOne(p => p.Usuario)
                    .WithMany()
                    .HasForeignKey(p => p.IdUsuarioProducto);
            });
        }
    }
}
