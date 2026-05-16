using Luftborn.Controllers;
using Luftborn.Dtos;
using Luftborn.Entities;
using Luftborn.UnitofWorkF;

public class CategoryController : BaseGenericApiController<Category, CategoryAddDto, CategoryReturnDto>
{
    public CategoryController(IUnitofWork uow) : base(uow)
    {
    }
}