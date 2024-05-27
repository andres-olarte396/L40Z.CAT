using Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Contexts
{
    /// <summary>
    /// Clase que representa el contexto de la base de datos de la aplicación.
    /// </summary>
    public class AppDbContext : DbContext
    {
        /// <summary>
        /// Propiedad que representa la tabla de usuarios.
        /// </summary>
        public DbSet<UserEntity> Users { get; set; }

        /// <summary>
        /// Constructor de la clase.
        /// </summary>
        /// <param name="options">
        /// Parámetro que representa las opciones de configuración del contexto.
        /// </param>
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        /// <summary>
        /// Método que se ejecuta al momento de crear el modelo de la base de datos.
        /// </summary>
        /// <returns>
        /// Retorna un objeto de tipo <see cref="ModelBuilder"/>.
        /// </returns>
        public override int SaveChanges()
        {
            UpdateAuditFields();
            return base.SaveChanges();
        }

        /// <summary>
        /// Método que se ejecuta al momento de crear el modelo de la base de datos de forma asíncrona.
        /// </summary>
        /// <param name="cancellationToken">
        /// Parámetro que representa el token de cancelación.
        /// </param>
        /// <returns>
        /// Retorna un objeto de tipo <see cref="Task{int}"/>.
        /// </returns>
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateAuditFields();
            return await base.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Método que se encarga de actualizar los campos de auditoría de las entidades.
        /// </summary>
        private void UpdateAuditFields()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is UserEntity && (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entry in entries)
            {
                var userEntity = (UserEntity)entry.Entity;

                if (entry.State == EntityState.Added)
                {
                    userEntity.CreatedAt = DateTime.UtcNow;
                    userEntity.CreatedBy = "system"; // Aquí puedes poner el usuario autenticado
                }
                else if (entry.State == EntityState.Modified)
                {
                    userEntity.ModifiedAt = DateTime.UtcNow;
                    userEntity.ModifiedBy = "system"; // Aquí puedes poner el usuario autenticado
                }
            }
        }
    }
}
