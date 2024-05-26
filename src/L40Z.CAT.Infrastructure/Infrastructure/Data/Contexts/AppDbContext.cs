using Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Contexts
{
    public class AppDbContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public override int SaveChanges()
        {
            UpdateAuditFields();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateAuditFields();
            return await base.SaveChangesAsync(cancellationToken);
        }

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
