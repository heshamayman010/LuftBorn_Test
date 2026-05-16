
using System.Linq.Expressions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Luftborn.Interfaces;
using Luftborn.Data;
using Microsoft.EntityFrameworkCore;

namespace Luftborn.Services
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public BaseRepository(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }
        public TEntity Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            return entity;
        }


        public void AddRange(List<TEntity> entities)
        {
            _context.Set<TEntity>().AddRange(entities);
        }

        public void Update(TEntity entity)
        {
            _context.Update<TEntity>(entity);

        }

        public void Remove(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }


        public bool IsExist(Expression<Func<TEntity, bool>> expression)
        {
            return _context.Set<TEntity>().Any(expression);
        }


        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {

            return await _context.Set<TEntity>().ToListAsync();

        }


        public async Task<IEnumerable<TEntity>> GetAllByAsync(Expression<Func<TEntity, bool>> expression)
        {
            var x = _context.Set<TEntity>().Where(expression).AsNoTracking();


            return await x.ToListAsync();
        }

        public IEnumerable<TEntity> GetAllBy(Expression<Func<TEntity, bool>> expression, string including)
        {
            return _context.Set<TEntity>().Where(expression).AsNoTracking().Include(including);

        }

        public async Task<IEnumerable<TEntity>> GetAllByThreadAsync(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, dynamic>> order)
        {
            return await _context.Set<TEntity>().Where(expression).OrderBy(order).ToListAsync();

        }

        public async Task<TEntity> GetFirstOrderByAsync(Expression<Func<TEntity, object>> expression)
        {
            var entity = await _context.Set<TEntity>().OrderBy(expression).FirstOrDefaultAsync();
            return entity ?? throw new InvalidOperationException("No entity found.");
        }

        public async Task<TEntity> GetLastOrderByAsync(Expression<Func<TEntity, object>> expression)
        {
            var entity = await _context.Set<TEntity>().OrderBy(expression).LastOrDefaultAsync();
            return entity ?? throw new InvalidOperationException("No entity found.");

        }

        public TEntity? GetBy(Expression<Func<TEntity, bool>> expression)
        {
            return _context.Set<TEntity>().FirstOrDefault(expression);
        }
        public async Task<TEntity?> GetByAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await _context.Set<TEntity>().FirstOrDefaultAsync(expression);
        }

        public TEntity? GetById(int id)
        {
            return _context.Set<TEntity>().Find(id);
        }

        public async Task<TEntity?> GetByIdAsync(int id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task<int> CountAll(Expression<Func<TEntity, bool>> expression)
        {
            return await _context.Set<TEntity>().CountAsync(expression);
        }


// here we get only the data for the mapping 
        public async Task<IEnumerable<T>> Map_GetAllByAsync<T>(Expression<Func<TEntity, bool>> expression)
        {
            return await _context.Set<TEntity>()
                .Where(expression)
                .ProjectTo<T>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<IEnumerable<T>> Map_GetAllAsync<T>()
        {
            return await _context.Set<TEntity>()
                .ProjectTo<T>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }


        public async Task<T> Map_GetByAsync<T>(Expression<Func<TEntity, bool>> expression)
        {
                var result = await _context.Set<TEntity>()
                    .Where(expression)
                    .ProjectTo<T>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync();

                return result ?? throw new InvalidOperationException("No matching record found.");


        }

        public async Task<IQueryable<object>> GetAllSelectedByAsync(Expression<Func<TEntity, bool>> expression,
          Expression<Func<TEntity, object>> selector)
        {
            var result = _context.Set<TEntity>()
                .Where(expression)
                // .ProjectTo<T>(_mapper.ConfigurationProvider)
                .Select(selector);

            return await Task.Run(() => result);
        }


        public async Task<int> GetMaxByAsync<T, Tkey>(Expression<Func<T, int>> seletor, Expression<Func<T, bool>> filter) where T : class
        {
            var dbset = _context.Set<T>();
            if (await dbset.AnyAsync(filter))
            {
                var result = await dbset.Where(filter).MaxAsync(seletor);
                return result;

            }
            else
            {

                return 000;
            }

        }


    }
}
