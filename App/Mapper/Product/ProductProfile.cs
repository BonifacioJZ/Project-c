using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Dto.Product;

namespace App.Mapper.Product
{
    public class ProductProfile : Profile
    {
        public ProductProfile(){
            CreateMap<ProductOutDto,Domain.Entity.Product>();
            CreateMap<Domain.Entity.Product,ProductOutDto>();
            CreateMap<ProductInDto,Domain.Entity.Product>();
            CreateMap<Domain.Entity.Product,ProductInDto>();
            CreateMap<Domain.Entity.Product,ProductDetails>()
            .ForMember(p=>p.Category,
            opt=>opt.MapFrom(p=>p.Category));
            CreateMap<ProductDetails,Domain.Entity.Product>();
        }
    }
}