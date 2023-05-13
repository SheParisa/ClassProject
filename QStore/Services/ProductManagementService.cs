using QStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QStore.Services
{
    public class ProductManagementService
    {
        private List<ProductModel> products = new List<ProductModel>();
        private List<PropertyModel> properties = new List<PropertyModel>();
        private List<ValuePropertyModel> values = new List<ValuePropertyModel>();
        public List<ProductModel> GetProducts()
        {
            return products;
        }
        public List<PropertyModel> GetProperties()
        {
            return properties;
        }
        public List<ValuePropertyModel> GetValueProperties()
        {
            return values;
        }
        public bool AddProduct(ProductModel product)
        {
            try
            {
            products.Add(product);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool AddProperty(PropertyModel property)
        {
            try
            {
                properties.Add(property);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool AddValueProperty(ValuePropertyModel valueProperty)
        {
            try
            {
                values.Add(valueProperty);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<ValuePropertyModel> SelectedValueProps(List<int> ParentIds)
        {
            List<ValuePropertyModel> valueprop = new List<ValuePropertyModel>();
            foreach (int id in ParentIds)
            {
                valueprop = values.Where(vp => vp.PropertyId == id).ToList();
            }
            return valueprop;
        }
    }
}
