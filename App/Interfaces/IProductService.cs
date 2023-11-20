using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Res;
using Domain.Dto.Product;

namespace App.Interfaces
{
    public interface IProductService
    {
        Task<Response<ICollection<ProductOutDto>>> getAll();
        Task<Response<ProductOutDto>> save(ProductInDto product);
        Task<Response<ProductDetails>> getById(Guid id);
        Task<Response<ProductOutDto>> update(ProductUpdateDto product,Guid id);
        Task<Response<string>> delete(Guid id);
    }
}