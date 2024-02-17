using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace NoteShare.Data
{
    public class NoteShareDbContext : DbContext
    {
        private IHttpContextAccessor httpContextAccessor;

        public NoteShareDbContext(DbContextOptions options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public override int SaveChanges()
        {
            var entries = ChangeTracker.Entries();
            foreach (var entry in entries)
            {
                if (entry.Entity is AuditedEntity entity)
                {
                    var now = DateTime.UtcNow;
                    var user = httpContextAccessor.HttpContext.User.Identity.Name;
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entity.CreatedAt = now;
                            entity.CreatedBy = user;
                            entity.ModifiedAt = now;
                            entity.ModifiedBy = user;
                            break;
                        case EntityState.Modified:
                            Entry(entity).Property(x => x.CreatedAt).IsModified = false;
                            Entry(entity).Property(x => x.CreatedBy).IsModified = false;
                            entity.ModifiedAt = now;
                            entity.ModifiedBy = user;
                            break;
                    }
                }
            }
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries();
            foreach (var entry in entries)
            {
                if (entry.Entity is AuditedEntity entity)
                {
                    var now = DateTime.UtcNow;
                    var user = httpContextAccessor.HttpContext.User.Identity.Name;
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entity.CreatedAt = now;
                            entity.CreatedBy = user;
                            entity.ModifiedAt = now;
                            entity.ModifiedBy = user;
                            break;
                        case EntityState.Modified:
                            Entry(entity).Property(x => x.CreatedAt).IsModified = false;
                            Entry(entity).Property(x => x.CreatedBy).IsModified = false;
                            entity.ModifiedAt = now;
                            entity.ModifiedBy = user;
                            break;
                    }
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
