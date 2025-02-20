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
                entity.HasKey(r => r.ID_ROL);
                entity.Property(r => r.ID_ROL).ValueGeneratedOnAdd().UseIdentityColumn();
                entity.Property(r => r.ROL_NOMBRE)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            // Configuración de UsuarioEntity
            modelBuilder.Entity<UsuarioEntity>(entity =>
            {
                entity.HasKey(u => u.ID_USUARIO);
                entity.Property(r => r.ID_USUARIO).ValueGeneratedOnAdd().UseIdentityColumn();
                entity.Property(u => u.USU_NOMBRE)
                    .IsRequired()
                    .HasMaxLength(100);
                entity.Property(u => u.USU_EMAIL)
                    .IsRequired()
                    .HasMaxLength(100);
                entity.Property(u => u.USU_FECHA_REGISTRO)
                    .HasDefaultValueSql("GETDATE()");

                // Relación con RolEntity
                entity.HasOne(u => u.Rol)
                    .WithMany(r => r.Usuarios)
                    .HasForeignKey(u => u.ID_ROL);



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
                entity.HasKey(p => p.ID_PRODUCTO);
                entity.Property(p => p.ID_PRODUCTO).ValueGeneratedOnAdd().UseIdentityColumn();
                entity.Property(p => p.PROD_NOMBRE)
                    .IsRequired()
                    .HasMaxLength(100);
                entity.Property(p => p.PROD_PRECIO)
                    .HasColumnType("decimal(18,2)");
                entity.Property(p => p.PROD_FECHA_CREACION)
                    .HasDefaultValueSql("GETDATE()");

                // Relación con UsuarioEntity
                entity.HasOne(p => p.Usuario)
                    .WithMany()
                    .HasForeignKey(p => p.ID_USUARIO);
            });
        }
    }
}
