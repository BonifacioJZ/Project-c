using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;
using App.Interfaces;
using App.Res;
using Domain.Dto.Category;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Api.Controllers
{
    /* The CategoryController class is a C# controller that handles HTTP requests related to
    categories, such as retrieving all categories, creating a new category, updating a category, and
    deleting a category. */
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IValidator<CategoryInDto> _validator;
        private readonly IValidator<CategoryUpdate> _updateValidator;

        public CategoryController(ICategoryService categoryService,IValidator<CategoryInDto> validator,IValidator<CategoryUpdate> updateValidator){
            _categoryService = categoryService;
            _validator = validator;
            _updateValidator=updateValidator;
        }

        /// <summary>
        /// This function is an HTTP GET endpoint that retrieves all categories and returns them as a
        /// response.
        /// </summary>
        /// <returns>
        /// The method is returning an ActionResult<Response<ICollection<CategoryOutDto>>>.
        /// </returns>
        [HttpGet]
        public async Task<ActionResult<Response<ICollection<CategoryOutDto>>>> index ()
        {
            var response = await _categoryService.getAll();
            return Ok(response);
        }
        
        /// <summary>
        /// This function is a HTTP POST endpoint that creates a new category and returns the response.
        /// </summary>
        /// <param name="CategoryInDto">CategoryInDto is a data transfer object (DTO) that represents
        /// the input data for creating a category. It contains the necessary properties to create a
        /// category, such as the category name, description, or any other relevant information.</param>
        /// <returns>
        /// The method is returning an ActionResult<Response<CategoryOutDto>>.
        /// </returns>
        [HttpPost]
        public async Task<ActionResult<Response<CategoryOutDto>>> create ([FromBody]CategoryInDto category){
            ValidationResult result = await _validator.ValidateAsync(category);
            if(!result.IsValid){
                string json = JsonConvert.SerializeObject(result.Errors);
                return BadRequest(json);
            }
            var response = await _categoryService.save(category);
            if(response.Data==null) return BadRequest(response);
            return Created("create",new{response}); 
        
        }/// <summary>
        /// The above function is a GET endpoint that retrieves a category by its ID and returns a
        /// response containing the category details.
        /// </summary>
        /// <param name="Guid">The "Guid" parameter in the above code is a unique identifier that
        /// represents a specific category. It is used to retrieve the details of a category by its
        /// ID.</param>
        /// <returns>
        /// The method is returning an `ActionResult<Response<CategoryDetails>>`.
        /// </returns>
        
        [HttpGet("{id}")]
        public async Task<ActionResult<Response<CategoryDetails>>> show(Guid id){
            var response = await _categoryService.getById(id);
            if(response.Data==null) return NotFound(response);
            return Ok(response);
        }
        /// <summary>
        /// This function updates a category with the given ID using the provided update object and
        /// returns the updated category details.
        /// </summary>
        /// <param name="Guid">The `Guid` parameter represents a unique identifier for the category that
        /// needs to be updated. It is used to identify the specific category in the database.</param>
        /// <param name="CategoryUpdate">CategoryUpdate is a model or class that contains the updated
        /// information for a category. It is passed as the request body in the HTTP PUT
        /// request.</param>
        /// <returns>
        /// The method is returning an ActionResult<Response<CategoryDetails>>.
        /// </returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<Response<CategoryDetails>>> update(Guid id, [FromBody]CategoryUpdate update){
            ValidationResult result = await _updateValidator.ValidateAsync(update);
            if(!result.IsValid){
                string json = JsonConvert.SerializeObject(result.Errors);
                return BadRequest(json);
            }
            var response = await _categoryService.update(id,update);
            if(response.Data == null) return BadRequest(response);
            return Ok(response);
            
        }

        /// <summary>
        /// The above function is a DELETE endpoint that deletes a category by its ID and returns a
        /// response indicating the success or failure of the operation.
        /// </summary>
        /// <param name="Guid">The "Guid" parameter in the above code is a unique identifier that
        /// represents a specific category. It is used to identify the category that needs to be
        /// deleted.</param>
        /// <returns>
        /// The method is returning an ActionResult<Response<string>>.
        /// </returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Response<string>>> delete(Guid id){
            var response = await _categoryService.delete(id);
            if(response.Data == null)return BadRequest(response);

            return Ok(response);
        }
    }
}