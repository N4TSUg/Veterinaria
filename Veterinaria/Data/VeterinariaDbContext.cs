using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Veterinaria.Models;

namespace Veterinaria.Data
{
    public class VeterinariaDbContext : DbContext
    {
        public VeterinariaDbContext(DbContextOptions<VeterinariaDbContext> options) : base(options) { }

        public DbSet<Rol> Roles { get; set; } = null!;
        public DbSet<Usuario> Usuarios { get; set; } = null!;
        public DbSet<Cliente> Clientes { get; set; } = null!;
        public DbSet<Mascota> Mascotas { get; set; } = null!;
        public DbSet<Consulta> Consultas { get; set; } = null!;
        public DbSet<Vacuna> Vacunas { get; set; } = null!;
        public DbSet<VacunaAplicada> VacunasAplicadas { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relaciones
            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.Rol)
                .WithMany(r => r.Usuarios)
                .HasForeignKey(u => u.IdRol)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Mascota>()
                .HasOne(m => m.Cliente)
                .WithMany(c => c.Mascotas)
                .HasForeignKey(m => m.IdCliente)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Consulta>()
                .HasOne(c => c.Mascota)
                .WithMany(m => m.Consultas)
                .HasForeignKey(c => c.IdMascota)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<VacunaAplicada>()
                .HasOne(va => va.Mascota)
                .WithMany(m => m.VacunasAplicadas)
                .HasForeignKey(va => va.IdMascota)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<VacunaAplicada>()
                .HasOne(va => va.Vacuna)
                .WithMany(v => v.VacunasAplicadas)
                .HasForeignKey(va => va.IdVacuna)
                .OnDelete(DeleteBehavior.Restrict);

            // Seed Data: Roles
            modelBuilder.Entity<Rol>().HasData(
                new Rol { IdRol = 1, NameRol = "Administrador" },
                new Rol { IdRol = 2, NameRol = "Veterinario" }
            );

            var user1 = new Usuario
            {
                IdUser = 1,
                NameUser = "Administrador",
                EmailUser = "enriquearana1402@gmail.com",
                IdRol = 1,
                //la contraseña es "3nriqueA1402#"
                PasswordUser = "AQAAAAIAAYagAAAAEMGr17R7Y/RniLPqGvxDVm+wu1HJ+zO+blOyf+W61SXe3PmjMGjKzfLBaOrksbPaSw=="
            };

            var user2 = new Usuario
            {
                IdUser = 2,
                NameUser = "Enrique Narro",
                EmailUser = "vetnarro@gmail.com",
                IdRol = 2,
                //la contraseña es "Lucho22*"
                PasswordUser = "AQAAAAIAAYagAAAAELq7CIaD5kLrTogQkheXcq6zVVGqO+Za/pV1tM2GyBXozcCOMNiwp6jtFGL+YMyqhw=="
            };

            // Seed Data: Usuarios
            modelBuilder.Entity<Usuario>().HasData(user1, user2);

            // Seed Data: Vacunas Base
            modelBuilder.Entity<Vacuna>().HasData(
                new Vacuna { IdVacuna = 1, Nombre = "Triple felina Rinotraqueitis Panleucopenia", Tipo = "Nobivac" },
                new Vacuna { IdVacuna = 2, Nombre = "Puppy DP", Tipo = "Biocan" },
                new Vacuna { IdVacuna = 3, Nombre = "Cuádruple DHPPI", Tipo = "Nobivac" },
                new Vacuna { IdVacuna = 4, Nombre = "Quíntuple DAPPv+L4", Tipo = "Nobivac" },
                new Vacuna { IdVacuna = 5, Nombre = "Rabia (Rabies)", Tipo = "Nobivac" },
                new Vacuna { IdVacuna = 6, Nombre = "Sextuple PDHLPiR", Tipo = "Biocan" }
            );
        }
    }
}
