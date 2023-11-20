using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Dto.Category;

namespace App.Mapper.Category
{
    public class CategoryProfile : Profile
    {
        /* The `CategoryProfile` class is a configuration class for AutoMapper. AutoMapper is a library
        in C# that helps in mapping objects from one type to another. */
        public CategoryProfile(){
            CreateMap<Domain.Entity.Category,CategoryOutDto>();
            CreateMap<CategoryInDto,Domain.Entity.Category>();
            CreateMap<CategoryInDto,Domain.Entity.Category>();
            CreateMap<Domain.Entity.Category,CategoryInDto>();
            CreateMap<Domain.Entity.Category,CategoryDetails>()
            .ForMember(c => c.products,
            opt =>opt.MapFrom(src=>src.Products));
            CreateMap<CategoryDetails,Domain.Entity.Category>();
        }
    }
}