using QStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QStore.Services
{
    public class CategoryManagementService
    {
        private Random random = new Random();
        private List<CategoryModel>  categories = new List<CategoryModel>();
        public List<CategoryModel> GetCategories()
        {
            return categories;
        }
        public CategoryModel AddCategory(CategoryModel category)
        {
            try
            {
                category.CategoryId = random.Next();
                categories.Add(category);
                return category;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
