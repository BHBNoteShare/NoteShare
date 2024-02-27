using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using NoteShare.Data;
using NoteShare.Models;

namespace NoteShare.Core.Services
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> GetAsQueryable();
        Task<TEntity?> GetByIdAsync(string id);
        Task<TResult> GetByIdAsync<TResult>(string id);
        Task<List<TEntity>> GetAllAsync();
        Task<List<TResult>> GetAllAsync<TResult>();
        Task<PagedResult<TResult>> GetAllAsync<TResult>(QueryParameters queryParameters);
        Task<TEntity> AddAsync(TEntity entity);
        Task<TResult> AddAsync<TSource, TResult>(TSource source);
        Task DeleteAsync(string id);
        Task<TEntity> DeleteSoftAsync(string id);
        Task UpdateAsync(TEntity entity);
        Task UpdateAsync<TSource>(string id, TSource source) where TSource : IBaseDto;
        Task<bool> Exists(string id);
    }

    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : AbstractEntity
    {
        private readonly NoteShareDbContext _context;
        private readonly IMapper _mapper;
        protected readonly DbSet<TEntity> DbSet;
        public GenericRepository(NoteShareDbContext noteShareDbContext, IMapper mapper)
        {
            _context = noteShareDbContext;
            _mapper = mapper;
            DbSet = _context.Set<TEntity>();
        }

        public IQueryable<TEntity> GetAsQueryable()
        {
            return DbSet;
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<TResult> AddAsync<TSource, TResult>(TSource source)
        {
            var entity = _mapper.Map<TEntity>(source);

            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<TResult>(entity);
        }

        public async Task DeleteAsync(string id)
        {
            var entity = await GetByIdAsync(id) ?? throw new NullReferenceException("Entity not found");
            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<TEntity> DeleteSoftAsync(string id)
        {
            var entity = await GetByIdAsync(id) ?? throw new NullReferenceException("Entity not found");
            entity.Deleted = true;
            await UpdateAsync(entity);
            return entity;
        }

        public async Task<bool> Exists(string id)
        {
            var entity = await GetByIdAsync(id);
            return entity != null;
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<List<TResult>> GetAllAsync<TResult>()
        {
            return await _context.Set<TEntity>()
                .ProjectTo<TResult>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<PagedResult<TResult>> GetAllAsync<TResult>(QueryParameters queryParameters)
        {
            var items = await _context.Set<TEntity>()
                .Skip(queryParameters.PageSize * queryParameters.PageNumber)
                .Take(queryParameters.PageSize)
                .ProjectTo<TResult>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return new PagedResult<TResult>
            {
                Items = items,
                PageNumber = queryParameters.PageNumber,
                PageSize = queryParameters.PageSize
            };
        }

        public async Task<TEntity?> GetByIdAsync(string id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task<TResult> GetByIdAsync<TResult>(string id)
        {
            var result = await _context.Set<TEntity>().FindAsync(id) ?? throw new NullReferenceException("Entity not found");
            return _mapper.Map<TResult>(result);
        }

        public async Task UpdateAsync(TEntity entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync<TSource>(string id, TSource source) where TSource : IBaseDto
        {
            if (id != source.Id)
            {
                throw new Exception("Invalid Id used in request");
            }

            var entity = await GetByIdAsync(id) ?? throw new NullReferenceException("Entity not found");
            _mapper.Map(source, entity);
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }
    }

    public interface IBaseDto
    {
        public string Id { get; set; }
    }
}
