using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Res;
using Domain.Dto.Category;

namespace App.Interfaces
{
    /* The `ICategoryService` interface defines a contract for a category service. It specifies several
    methods that can be implemented by a class that provides category-related functionality. */
    public interface ICategoryService
    {
        Task<Response<ICollection<CategoryOutDto>>> getAll();
        Task<Response<CategoryOutDto>> save(CategoryInDto categoryInDto);
        Task<Response<CategoryDetails>> getById(Guid id);
        Task<Response<CategoryDetails>> update(Guid id, CategoryUpdate update);
        Task<Response<string>> delete(Guid id);
    }
}