using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Dto.Category;
using FluentValidation;
using Persistence;

namespace App.validations
{
    public class CategoryUpdateValidation : AbstractValidator<CategoryUpdate>
    {
        private readonly DbCtx _db;
        public CategoryUpdateValidation(DbCtx db){
            _db = db;
            RuleFor(x => x.Name).NotEmpty().MaximumLength(150).Must(UniqueName).WithMessage("This category name already exists.");
            RuleFor(x => x.Description).MaximumLength(500);
            RuleFor(x => x.GuardName).NotEmpty().MaximumLength(13).Must(UniqueGuardName).WithMessage("The GuardName already exists.");
        }

        /// <summary>
        /// The function checks if a given name is unique among categories, excluding the category with
        /// the same ID.
        /// </summary>
        /// <param name="CategoryUpdate">CategoryUpdate is a class that represents the updated
        /// information for a category. It likely contains properties such as Id, Name, Description,
        /// etc.</param>
        /// <param name="name">The name of the category that you want to check for uniqueness.</param>
        /// <returns>
        /// The method is returning a boolean value. It returns true if the name is unique (not found in
        /// the database) or if the category id matches the id of the category found in the database. It
        /// returns false if the name is not unique (found in the database) and the category id does not
        /// match.
        /// </returns>
        private bool UniqueName(CategoryUpdate category,string name){
            var dbCategory = _db.Categories
            .Where(x => x.Name.ToLower()== name.ToLower())
            .SingleOrDefault();
            if(dbCategory == null) return true;
            if(category.id == dbCategory.Id) return true;
            return false;
        }
        /// <summary>
        /// The function checks if a given category name is unique among existing categories, excluding
        /// the category being updated.
        /// </summary>
        /// <param name="CategoryUpdate">CategoryUpdate is a class that represents the updated
        /// information for a category. It likely contains properties such as Id, Name, and
        /// GuardName.</param>
        /// <param name="name">The name parameter is a string representing the name of a
        /// category.</param>
        /// <returns>
        /// The method is returning a boolean value.
        /// </returns>
        private bool UniqueGuardName(CategoryUpdate category,string name){
            var dbCategory = _db.Categories
            .Where(x => x.GuardName.ToLower()== name.ToLower())
            .SingleOrDefault();
            if(dbCategory == null) return true;
            if(category.id == dbCategory.Id) return true;
            return false;
        }
    }
}