using System;
using AutoMapper;
using Luftborn.Dtos;
using Luftborn.Entities;

namespace API.Helpers;

public class AutoMapperHelper:Profile
{

    public AutoMapperHelper()
    {

        CreateMap<Product,ProductAddDto>().ReverseMap();
        CreateMap<Product,ProductReturnDto>()
        .ForMember(x=>x.CategoryNameAr,opt=>opt.MapFrom(z=>z.category.NameAr))
        .ForMember(x=>x.CategoryNameEn,opt=>opt.MapFrom(z=>z.category.NameEn)).ReverseMap();

        CreateMap<Category,CategoryAddDto>().ReverseMap();
        
        CreateMap<Category,CategoryReturnDto>().ReverseMap();






    }

}
