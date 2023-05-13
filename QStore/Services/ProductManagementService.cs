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
        private Random random = new Random();
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
        public ProductModel AddProduct(ProductModel product)
        {
            try
            {
                product.ProductId = random.Next();
                products.Add(product);
                return product;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public void UpdateProduct(int ProductId, List<ProductPrroperty> productPrroperties)
        {
            products.Find(p => p.ProductId == ProductId).productPrroperties = productPrroperties;
        }
        public PropertyModel AddProperty(PropertyModel property)
        {
            try
            {
                property.PropertyId = random.Next();
                properties.Add(property);
                return property;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public bool AddValueProperty(ValuePropertyModel valueProperty)
        {
            try
            {
                valueProperty.ValuePropertyId = random.Next();
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
        public List<ProductPrroperty> GetListProductPrroperties(int ProductId, List<int> svp)
        {
            Random random = new Random();
            List<ProductPrroperty> productPrroperties = new List<ProductPrroperty>();
            foreach (int id in svp)
            {
                ValuePropertyModel vpm = values.Find(v => v.ValuePropertyId == id);
                productPrroperties.Add(new ProductPrroperty()
                {
                    IdProductProperty = random.Next(),
                    VProductProperty = new Tuple<int, int, int>(ProductId, vpm.PropertyId, vpm.ValuePropertyId)
                });
            }
            return productPrroperties;

        }
        public string GetNameProductPropertiesValue(ProductPrroperty productPrroperty)
        {
            return values.Find(v => v.ValuePropertyId == productPrroperty.VProductProperty.Item3).PropertyValue;
        }
        public string GetNameProductProperty(ProductPrroperty productPrroperty)
        {
            return properties.Find(p => p.PropertyId == productPrroperty.VProductProperty.Item2).Name;

        }
        public void SetValueProperty(int propertyId, List<string> values)
        {
            foreach (string value in values)
            {
                AddValueProperty(new ValuePropertyModel() { PropertyId = propertyId, PropertyValue = value });
            }
        }
    }
}
