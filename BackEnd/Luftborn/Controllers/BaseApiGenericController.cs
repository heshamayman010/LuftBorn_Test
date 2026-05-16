using Luftborn.Interfaces;
using Luftborn;
using Luftborn.Controllers;
using Luftborn.UnitofWorkF;
using Microsoft.AspNetCore.Mvc;
using Luftborn.Entities;
using Luftborn.Dtos;
using Microsoft.AspNetCore.Authorization;

namespace Luftborn.Controllers
{
    [Authorize]
    public class BaseGenericApiController<TEntity, TAddDto, TReturnDto> : BaseController
        where TEntity : BaseEntity
        where TAddDto : BaseDto
        where TReturnDto : BaseDto
    {
        private readonly IUnitofWork _uow;
        private readonly IBaseRepository<TEntity> _Repo;
        public BaseGenericApiController(IUnitofWork uow)
        {
            _uow = uow;
            _Repo = _uow.BaseRepository<TEntity>();
        }


        [HttpPost("add")]
        public virtual async Task<IActionResult> Add(TAddDto dto)
        {

            dto.Id = 0;

            var x = _uow.Mapper.Map<TEntity>(dto);

            var result = _Repo.Add(x);

            if (!await _uow.Complete()) return BadRequest(400);

            var map = _uow.Mapper.Map<TReturnDto>(result);

            return Ok(map);
        }

        protected virtual async Task<IActionResult> Add(TEntity entity)
        {
            var result = _Repo.Add(entity);

            if (!await _uow.Complete()) return BadRequest(400);

            var map = _uow.Mapper.Map<TReturnDto>(result);

            return Ok(map);
        }

        [HttpPut("update")]
        public virtual async Task<IActionResult> Update(TAddDto dto)
        {
            var entity = await _Repo.GetByAsync(x => x.Id == dto.Id);

            if (entity == null || entity.IsDeleted) return NotFound(StatusCodes.Status404NotFound);

            var result = _uow.Mapper.Map(dto, entity);

            _Repo.Update(result);

            if (await _uow.Complete()) return Ok();

            return BadRequest("Failed to Update");
        }

        protected virtual async Task<IActionResult> Update(TEntity entity)
        {
            _Repo.Update(entity);

            if (!await _uow.Complete()) return BadRequest("Failed to Update");

            return Ok();

        }

        [HttpDelete]
        public virtual async Task<IActionResult> Delete(int id)
        {
            var entity = await _Repo.GetByAsync(x => x.Id == id && x.IsDeleted == false);

            if (entity == null || entity.IsDeleted) return NotFound(StatusCodes.Status404NotFound);

            entity.IsDeleted = true;

            _Repo.Update(entity);

            if (!await _uow.Complete()) return BadRequest(StatusCodes.Status400BadRequest);

            return Ok();

        }


        [HttpDelete("HardDelete")]
        public virtual async Task<IActionResult> HardDelete(int id)
        {
            var entity = await _Repo.GetByAsync(x => x.Id == id && x.IsDeleted == false);

            if (entity == null || entity.IsDeleted) return NotFound(StatusCodes.Status404NotFound);


            _Repo.Remove(entity);

            if (!await _uow.Complete()) return BadRequest(StatusCodes.Status400BadRequest);

            return Ok();

        }





        protected virtual async Task<IActionResult> Delete(TEntity entity)
        {
            entity.IsDeleted = true;
            _Repo.Update(entity);

            if (!await _uow.Complete()) return BadRequest("Failed to Delete");

            return Ok("deleted");
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> Get()
        {
            var result = await _Repo.Map_GetAllByAsync<TReturnDto>(x => !x.IsDeleted);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {

            var result = await _Repo.Map_GetByAsync<TReturnDto>(x => x.Id == id && x.IsDeleted == false);

            return Ok(result);
        }
        [HttpGet("count")]
        public async Task<IActionResult> Count()
        {
            var result = await _Repo.CountAll(x => !x.IsDeleted);
            return Ok(result);
        }

    }
}
