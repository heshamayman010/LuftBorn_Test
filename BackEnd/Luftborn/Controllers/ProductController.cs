using Luftborn.Controllers;
using Luftborn.Dtos;
using Luftborn.Entities;
using Luftborn.Interfaces;
using Luftborn.UnitofWorkF;
using Microsoft.AspNetCore.Mvc;

public class ProductController : BaseGenericApiController<Product, ProductAddDto, ProductReturnDto>
{

    private readonly IBaseRepository<Product> _productsRepository;
    public ProductController(IUnitofWork uow) : base(uow)
    {
        _productsRepository=uow.BaseRepository<Product>();

    }



    [HttpGet("GetAllProductsByCategoryId/{catid}")]
    public async Task<IActionResult>GetAllProductsByCategoryId(int catid)
    {
        
        if (catid == 0)
        {
            return NotFound();
        }

        var Products=await _productsRepository.Map_GetAllByAsync<ProductReturnDto>(x=>x.CategoryId==catid);
        return Ok(Products);
    }


    [HttpGet("search/{word}")]
    public async Task<IActionResult>GetAllProductsByCategoryId(string word)
    {
        
        if (string.IsNullOrEmpty(word))
        {
            return Ok( await _productsRepository.Map_GetAllAsync<ProductReturnDto>());
        }

        var Products=await _productsRepository.Map_GetAllByAsync<ProductReturnDto>(x=>
        x.NameAr.Contains(word)
        ||
        x.NameEn.Contains(word)
        );
        return Ok(Products);
    }


}