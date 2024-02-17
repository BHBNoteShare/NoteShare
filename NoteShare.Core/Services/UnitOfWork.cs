using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NoteShare.Data;

namespace NoteShare.Core.Services
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : AbstractEntity;
        DbSet<TEntity> GetDbSet<TEntity>() where TEntity : AbstractEntity;
        int SaveChanges();
        Task SaveChangesAsync();
        NoteShareDbContext Context();
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly NoteShareDbContext _noteShareDbContext;
        private readonly IMapper _mapper;
        private Dictionary<Type, object> _repositories;

        public UnitOfWork(NoteShareDbContext recipeDbContext, IMapper mapper)
        {
            _noteShareDbContext = recipeDbContext;
            _mapper = mapper;
        }

        public NoteShareDbContext Context()
        {
            return _noteShareDbContext;
        }

        public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : AbstractEntity
        {
            if (_repositories == null)
            {
                _repositories = new Dictionary<Type, object>();
            }

            var type = typeof(TEntity);
            if (!_repositories.ContainsKey(type))
            {
                _repositories[type] = new GenericRepository<TEntity>(_noteShareDbContext,_mapper);
            }

            return (IGenericRepository<TEntity>)_repositories[type];
        }

        public int SaveChanges()
        {
            return _noteShareDbContext.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _noteShareDbContext.Dispose();
            }
        }

        public DbSet<TEntity> GetDbSet<TEntity>() where TEntity : AbstractEntity
        {
            return _noteShareDbContext.Set<TEntity>();
        }

        public Task SaveChangesAsync()
        {
            return _noteShareDbContext.SaveChangesAsync();
        }
    }
}
