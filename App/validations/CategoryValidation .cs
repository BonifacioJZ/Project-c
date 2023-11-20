using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Dto.Category;
using Domain.Entity;
using FluentValidation;
using Persistence;

namespace App.validations
{
    public class CategoryValidation : AbstractValidator<CategoryInDto>
    {
        private readonly DbCtx _db;
        public CategoryValidation(DbCtx db){
            _db = db;
            RuleFor(x => x.Name).NotEmpty().MaximumLength(150).Must(UniqueName).WithMessage("This category name already exists.");
            RuleFor(x => x.Description).MaximumLength(500);
            RuleFor(x => x.GuardName).MaximumLength(13).NotEmpty().Must(UniqueGuardName).WithMessage("The GuardName already exists.");
        }
       /// <summary>
       /// The function checks if a given name is unique in a database table called "Categories".
       /// </summary>
       /// <param name="name">The name parameter is a string that represents the name of a
       /// category.</param>
       /// <returns>
       /// The method is returning a boolean value. It returns true if there is no category in the
       /// database with the same name (case-insensitive), and false if there is a category with the
       /// same name.
       /// </returns>
        private bool UniqueName(string name){
            var dbCategory = _db.Categories
            .Where(x => x.Name.ToLower()== name.ToLower())
            .SingleOrDefault();
            if(dbCategory == null) return true;
            return false;
        }
        /// <summary>
        /// The function checks if a given name is unique among the guard names in a database table.
        /// </summary>
        /// <param name="name">The name parameter is a string representing the name of a
        /// category.</param>
        /// <returns>
        /// The method is returning a boolean value. If the dbCategory is null, it returns true,
        /// indicating that the guard name is unique. Otherwise, it returns false, indicating that the
        /// guard name already exists in the database.
        /// </returns>
        private bool UniqueGuardName(string name){
            var dbCategory = _db.Categories
            .Where(x => x.GuardName.ToLower()== name.ToLower())
            .SingleOrDefault();
            if(dbCategory == null) return true;
            return false;
        }
    }
}