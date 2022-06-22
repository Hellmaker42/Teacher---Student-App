using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tenta_API.ViewModel.Category;

namespace Tenta_API.Interfaces
{
    public interface ICategoryRepository
    {
        public Task AddCategoryAsync(PostCategoryViewModel model);
        public Task<List<CategoryViewModel>> GetAllCategoriesAsync();
        public Task<CategoryViewModel> GetCategoryByIdAsync(int id);
        public Task<List<CategoryWithCoursesViewModel>> GetCategoryWithCoursesAsync();
        public Task UpdateCategoryAsync(PostCategoryViewModel model, int id);
        public void DeleteCategoryAsync(int id);
        public Task<bool> SaveAllChangesAsync();
    }
}