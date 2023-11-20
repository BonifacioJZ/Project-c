using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Interfaces;
using App.Res;
using AutoMapper;
using Domain.Dto.Category;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace App.Services
{
    /* The CategoryService class is responsible for handling CRUD operations for categories in a
    database. */
    public class CategoryService : ICategoryService
    {
        private readonly DbCtx dbCtx;
        private readonly IMapper mapper;

        public CategoryService(DbCtx dbCtx, IMapper mapper){
            this.dbCtx = dbCtx;
            this.mapper = mapper;
        
        }
       /// <summary>
       /// The function "getAll" retrieves all categories from the database, maps them to CategoryOutDto
       /// objects, and returns a response object containing the categories.
       /// </summary>
       /// <returns>
       /// The method is returning a `Response` object with a generic type of
       /// `ICollection<CategoryOutDto>`. The `Data` property of the `Response` object is set to the
       /// `categoryOuts` variable, which is a collection of `CategoryOutDto` objects. The `Success`
       /// property is set to `true`, indicating that the operation was successful. The `message`
       /// property is set
       /// </returns>
        public async Task<Response<ICollection<CategoryOutDto>>> getAll(){
            var categories = await dbCtx.Categories.ToListAsync();
            var categoryOuts = mapper.Map<ICollection<CategoryOutDto>>(categories);
            return new Response<ICollection<CategoryOutDto>>(){
                Data = categoryOuts,
                Success = true,
                message = "Categories fetched successfully"
            };
        }

        /// <summary>
        /// The function saves a category object to a database and returns a response indicating whether
        /// the save was successful or not.
        /// </summary>
        /// <param name="CategoryInDto">CategoryInDto is a data transfer object (DTO) that contains the
        /// properties of a category that needs to be saved. It is used to transfer data between the
        /// client and the server.</param>
        /// <returns>
        /// The method is returning a `Task<Response<CategoryOutDto>>`.
        /// </returns>
        public async Task<Response<CategoryOutDto>> save(CategoryInDto categoryInDto)
        {
            var category = mapper.Map<Category>(categoryInDto);
            category.Id = Guid.NewGuid();
            dbCtx.Add(category);
            var result = await dbCtx.SaveChangesAsync();
            if (result == 0){
                return new Response<CategoryOutDto>(){
                    Success=false,
                    message="Error saving category",
                    Data=null
                };
            }
            return new Response<CategoryOutDto>(){
                Success=true,
                message="Category saved successfully",
                Data=mapper.Map<CategoryOutDto>(category)
            };
        }

        /// <summary>
        /// The function "show" retrieves a category from the database based on its ID and returns a
        /// response object containing the category details.
        /// </summary>
        /// <param name="Guid">The parameter "id" is of type Guid, which is a globally unique
        /// identifier. It is used to identify a specific category in the database.</param>
        /// <returns>
        /// The method is returning a `Task<Response<CategoryDetails>>`.
        /// </returns>
        public async Task<Response<CategoryDetails>> getById(Guid id)
        {
            var category = await dbCtx.Categories
            .Where(c=>c.Id==id)
            .Include(c=>c.Products).FirstOrDefaultAsync();
            if(category == null) return new Response<CategoryDetails>(){
                Success=false,
                message="Category not found",
                Data=null
            };
            var categoryOuts = mapper.Map<CategoryDetails>(category);
            return new Response<CategoryDetails>(){
                Success=true,
                message="Category fetched successfully",
                Data=categoryOuts
            };
        }

       /// <summary>
       /// The function updates a category in a database and returns a response indicating whether the
       /// update was successful or not.
       /// </summary>
       /// <param name="Guid">The `Guid` parameter represents a unique identifier for a category. It is
       /// used to find the category in the database that needs to be updated.</param>
       /// <param name="CategoryUpdate">CategoryUpdate is a class that contains the properties to update
       /// a category. It may have the following properties:</param>
       /// <returns>
       /// The method is returning a `Task<Response<CategoryDetails>>`.
       /// </returns>
        public async Task<Response<CategoryDetails>> update(Guid id, CategoryUpdate update)
        {
            var category = await dbCtx.Categories.FindAsync(id);
            if (category == null) return new Response<CategoryDetails>(){
                Success=false,
                message="Category not found",
                Data=null
            };
            category.Name = update.Name ?? category.Name;
            category.Description = update.Description ?? category.Description;
            category.GuardName = update.GuardName ?? category.GuardName;
            
            var result = await dbCtx.SaveChangesAsync();
            if(result == 0) return new Response<CategoryDetails>(){
                Success = false,
                message="Error to save category",
                Data=null
            };
            
            return new Response<CategoryDetails>(){
                Success=true,
                message="Update successfully",
                Data=mapper.Map<CategoryDetails>(category)
            };
        }

        /// <summary>
        /// The function deletes a category from a database and returns a response indicating whether
        /// the deletion was successful or not.
        /// </summary>
        /// <param name="Guid">The parameter "id" is of type Guid, which is a globally unique
        /// identifier. It is used to identify a specific category that needs to be deleted.</param>
        /// <returns>
        /// The method is returning a `Task<Response<string>>`.
        /// </returns>
        public async Task<Response<string>> delete(Guid id)
        {
            var category = await dbCtx.Categories.FindAsync(id);
            if(category == null) return new Response<string>(){
                Success=false,
                message="Category not found",
                Data=null
            };

            dbCtx.Categories.Remove(category);
            var result = await dbCtx.SaveChangesAsync();
            if(result == 0) return new Response<string>(){
                Success=false,
                message="Error to delete category",
                Data=null
            };

            return new Response<string>(){
                Success=true,
                message="Category deleted successfully",
                Data="deleted"
            };
        }
    }
}