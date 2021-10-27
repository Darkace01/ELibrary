using ELibrary.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELibrary.Service.Contract
{
    public interface ICategoryService
    {
        /// <summary>
        /// Gets a category by its id
        /// Specify true to include relationships such as companies
        /// </summary>
        /// <param name="id"></param>
        /// <param name="includeRelationships"></param>
        /// <returns></returns>
        Category Get(int id, bool includeRelationships = false);

        /// <summary>
        /// Gets all the business in the database
        /// Specify true to include relationships such as companies
        /// </summary>
        /// <param name="includeRelationships"></param>
        /// <returns></returns>
        IQueryable<Category> GetAll(bool includeRelationships = false);

        /// <summary>
        /// Gets all the featured categories in the database
        /// </summary>
        /// <param name="includeRelationships"></param>
        /// <returns></returns>
        IQueryable<Category> GetAllFeatured(bool includeRelationships);

        /// <summary>
        /// Adds a new category
        /// </summary>
        /// <param name="businessCategory"></param>
        /// <returns></returns>
        Task Add(Category businessCategory);

        /// <summary>
        /// Updates an existing category
        /// </summary>
        /// <param name="businessCategory"></param>
        /// <returns></returns>
        Task Update(Category businessCategory, string previousName);

        /// <summary>
        /// Delete category
        /// </summary>
        /// <param name="businessCategory"></param>
        Task Delete(Category businessCategory);

        /// <summary>
        /// Returns the default category
        /// </summary>
        /// <returns></returns>
        Category GetDefaultBusinessCategory();
        Task Update(Category category);
    }
}
