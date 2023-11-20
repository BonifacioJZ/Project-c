using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Interfaces;
using App.Res;
using Domain.Dto.Product;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService){
            _productService = productService;
        }

       /// <summary>
       /// The above function is an HTTP GET endpoint that returns a collection of products as a
       /// response.
       /// </summary>
       /// <returns>
       /// The method is returning an ActionResult<Response<ICollection<ProductOutDto>>>.
       /// </returns>
        [HttpGet]
        public async Task<ActionResult<Response<ICollection<ProductOutDto>>>> index(){
            var response = await _productService.getAll();
            return Ok(response); 
        }
       /// <summary>
       /// The above function is a HTTP POST endpoint that creates a new product and returns the
       /// response.
       /// </summary>
       /// <param name="ProductInDto">ProductInDto is a data transfer object (DTO) that represents the
       /// input data for creating a product. It contains the necessary properties and data for creating
       /// a new product.</param>
       /// <returns>
       /// The code is returning an ActionResult<Response<ProductOutDto>>.
       /// </returns>
        [HttpPost]
        public async Task<ActionResult<Response<ProductOutDto>>> create([FromBody]ProductInDto product){
            var response = await _productService.save(product);
            if(response.Data ==null) return BadRequest(response);
            return Created("created",new{
                response
            });
        }
        /// <summary>
        /// The above function is an HTTP GET endpoint that retrieves a product by its ID and returns a
        /// response containing the product details.
        /// </summary>
        /// <param name="Guid">The "Guid" parameter in the above code is a unique identifier that
        /// represents a specific product. It is used to retrieve the details of a product from the
        /// database.</param>
        /// <returns>
        /// The method is returning an ActionResult<Response<ProductDetails>>.
        /// </returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Response<ProductDetails>>> Show(Guid id){
            var response = await _productService.getById(id);
            if(response.Data == null) return NotFound(response);
            return Ok(response); 
        }

       /// <summary>
       /// This C# function updates a product with the specified ID using an HTTP PUT request.
       /// </summary>
       /// <param name="ProductUpdateDto">ProductUpdateDto is a data transfer object (DTO) that contains
       /// the updated information for a product. It is used to transfer data between the client and the
       /// server when updating a product.</param>
       /// <param name="Guid">The "Guid" parameter is a unique identifier that represents a specific
       /// object or entity. In this case, it is used to identify the product that needs to be
       /// updated.</param>
       /// <returns>
       /// The method is returning an ActionResult<Response<ProductOutDto>>.
       /// </returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<Response<ProductOutDto>>> update([FromBody]ProductUpdateDto product,Guid id){
            var response = await _productService.update(product,id);
            if(response.Data == null) return NotFound(response);
            return Ok(response); 
        }

        
        /// <summary>
        /// This function handles the HTTP DELETE request to delete a product by its ID.
        /// </summary>
        /// <param name="Guid">The `Guid` parameter represents a unique identifier for the resource that
        /// needs to be deleted. In this case, it is used to identify the specific product that needs to
        /// be deleted.</param>
        /// <returns>
        /// The method is returning an ActionResult<Response<string>>.
        /// </returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Response<string>>> delete(Guid id){
            var response = await _productService.delete(id);
            if(response.Data == null) return NotFound(response);
            return Ok(response);
        } 
    }
}