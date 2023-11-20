using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Interfaces;
using App.Res;
using AutoMapper;
using Domain.Dto.Product;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace App.Services
{
    public class ProductService : IProductService
    {
        private readonly DbCtx _db;
        private readonly IMapper _mapper;

        public ProductService(DbCtx db, IMapper mapper){
            _db = db;
            _mapper = mapper;
        }

        public async Task<Response<string>> delete(Guid id)
        {
            var product = await _db.Products.FindAsync(id);
            if (product==null) return new Response<string>(){
                Success = false,
                message = "product not found",
                Data = null
            };
            _db.Products.Remove(product);
            var result = await _db.SaveChangesAsync();
            if(result==0) return new Response<string>(){
                Success = false,
                message = "product not deleted",
                Data = null
            };
            return new Response<string>(){
                Success = true,
                message = "product deleted successfully",
                Data = "deleted"
            };
        }

        /// <summary>
        /// The function "getAll" retrieves all products from the database and returns a response object
        /// containing a collection of product DTOs.
        /// </summary>
        /// <returns>
        /// The method is returning a `Task<Response<ICollection<ProductOutDto>>>`.
        /// </returns>
        public async Task<Response<ICollection<ProductOutDto>>> getAll()
        {
            var products = await _db.Products.ToListAsync();
            return new Response<ICollection<ProductOutDto>>(){
                Success=true,
                message="products fetch successfully",
                Data= _mapper.Map<ICollection<ProductOutDto>>(products)
            };
        }

        /// <summary>
        /// The function getById retrieves a product by its ID from a database and returns a response
        /// object containing the product details.
        /// </summary>
        /// <param name="Guid">The parameter "id" is of type Guid, which is a globally unique
        /// identifier. It is used to uniquely identify a specific product in the database.</param>
        /// <returns>
        /// The method is returning a `Task<Response<ProductDetails>>`.
        /// </returns>
        public async Task<Response<ProductDetails>> getById(Guid id)
        {
            var product = await _db.Products
            .Where(p=> p.id==id)
            .Include(p=>p.Category)
            .FirstOrDefaultAsync();
            if(product==null) return new Response<ProductDetails>(){
                Success=false,
                message="product not found",
                Data=null
            };
            return new Response<ProductDetails>(){
                Success=true,
                message="product fetch successfully",
                Data=_mapper.Map<ProductDetails>(product)
            };
        }
/// <summary>
/// The `save` function saves a product to the database and returns a response indicating whether the
/// save was successful or not.
/// </summary>
/// <param name="ProductInDto">A data transfer object (DTO) that represents the input data for creating
/// a new product. It contains properties such as the product name, description, price, and category
/// ID.</param>
/// <returns>
/// The method is returning a `Task<Response<ProductOutDto>>`.
/// </returns>
        public async Task<Response<ProductOutDto>> save(ProductInDto product)
        {
            var productOut = _mapper.Map<Product>(product);
            productOut.id = Guid.NewGuid();
            var category = await _db.Categories.FindAsync(product.CategoryId);
            if(category==null) return new Response<ProductOutDto>(){
                Success=false,
                message="category not found",
                Data=null
            };

            productOut.Category = category;
            productOut.CategoryId = category.Id;
            _db.Products.Add(productOut);

            var result = await _db.SaveChangesAsync();
            if(result==0) return new Response<ProductOutDto>(){
                Success=false,
                message="product not saved",
                Data=null
            };

            return new Response<ProductOutDto>(){
                Success=true,
                message="product saved successfully",
                Data=_mapper.Map<ProductOutDto>(productOut)
            };
        }

        /// <summary>
        /// The function updates a product in a database and returns a response indicating whether the
        /// update was successful or not.
        /// </summary>
        /// <param name="ProductUpdateDto">ProductUpdateDto is a data transfer object that contains the
        /// updated information for a product. It includes the following properties:</param>
        /// <param name="Guid">The parameter "id" is of type Guid and represents the unique identifier
        /// of the product that needs to be updated.</param>
        /// <returns>
        /// The method is returning a `Task<Response<ProductOutDto>>`.
        /// </returns>
        public async Task<Response<ProductOutDto>> update(ProductUpdateDto product, Guid id)
        {
            var oldProduct = await _db.Products.FindAsync(id);
            if(oldProduct==null) return new Response<ProductOutDto>(){
                Success = false,
                message = "product not found",
                Data = null
            };
            
            oldProduct.Name = product.Name ?? oldProduct.Name;
            oldProduct.Description = product.Description ?? oldProduct.Description;
            oldProduct.Price = product.Price;
            oldProduct.Quantity = product.Quantity;
            oldProduct.IsEnable = product.IsEnable;
            
            var category = await _db.Categories.FindAsync(product.CategoryId);
            if (category == null ) return new Response<ProductOutDto>(){
                Success = false,
                message = "category not found",
                Data = null
            };

            oldProduct.Category = category;
            var result = await _db.SaveChangesAsync();
            if(result==0) return new Response<ProductOutDto>(){
                Success = false,
                message = "product not updated",
                Data = null
            }; 

            return new Response<ProductOutDto>(){
                Success = true,
                message = "product updated successfully",
                Data = _mapper.Map<ProductOutDto>(oldProduct)
            };
        }
    }


}