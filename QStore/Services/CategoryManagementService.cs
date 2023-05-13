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
        private List<CategoryModel>  categories = new List<CategoryModel>();
        public List<CategoryModel> GetCategories()
        {
            return categories;
        }
        public bool AddCategory(CategoryModel category)
        {
            try
            {
                categories.Add(category);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
