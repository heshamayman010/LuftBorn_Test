using AutoMapper;
using Luftborn.Interfaces;

namespace Luftborn.UnitofWorkF;

public interface IUnitofWork
{

    Task<bool> Complete();

    IMapper Mapper { get; }
    IBaseRepository<TEntity> BaseRepository<TEntity>() where TEntity : class;




}