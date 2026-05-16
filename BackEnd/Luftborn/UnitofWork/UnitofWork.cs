using AutoMapper;
using Luftborn.Interfaces;
using Luftborn.Data;
using Luftborn.Interfaces;
using Luftborn.UnitofWorkF;
using System.Collections;
using Luftborn.Services;

public class UnitofWork (DataContext context
, IMapper mapper) : IUnitofWork
{

        private Hashtable _repositories;



        public IMapper Mapper => mapper;



        public IBaseRepository<TEntity> BaseRepository<TEntity>() where TEntity : class
        {
            if (_repositories == null) _repositories = new Hashtable();

            var type = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(BaseRepository<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), context, mapper);

                _repositories.Add(type, repositoryInstance);
            }

            return (BaseRepository<TEntity>)_repositories[type];
        }

    public async Task<bool> Complete()
    {
        return await context.SaveChangesAsync() > 0;

    }
}