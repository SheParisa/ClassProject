using QStore.Enums;
using QStore.Models;
using QStore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace QStore
{
    public class ConsoleHelper
    {
        CategoryManagementService categoryManagement = new CategoryManagementService();
        ProductManagementService productManagement = new ProductManagementService();
        public int MainMenu()
        {
            Console.Clear();
            Console.WriteLine("Welcome to Application!");
            Console.WriteLine("what you want to select? 0/1/2/3/4? (write 0 to exit)");
            Console.WriteLine("1. Add a new product");
            Console.WriteLine("2. Add a new Category");
            Console.WriteLine("3. Report products");
            Console.WriteLine("4. Report Categories");
            return Convert.ToInt32(Console.ReadLine());
        }
        public void ActionMenu(Request Selector)
        {
            switch (Selector)
            {
                case Request.AddProduct:
                    {
                        try
                        {
                            Product_InputInfo();
                        }
                        catch
                        {
                            Console.Clear();
                            Console.WriteLine("return to main menu!!!");
                        }
                        break;
                    }
                case Request.AddCategory:
                    {
                        try
                        {
                            Category_InputInfo();

                        }
                        catch
                        {
                            Console.Clear();
                            Console.WriteLine("return to main menu!!!");
                        }
                        break;
                    }
                case Request.ReportProduct:
                    {
                        Report_Product();
                        Console.ReadLine();
                        break;
                    }
                case Request.ReportCategory:
                    {
                        Report_Category();
                        Console.ReadLine();
                        break;
                    }

            }
        }
        public void Product_InputInfo()
        {
            Console.Clear();

            if (productManagement.GetProperties().Count != 0)
            {
                // Create new product
                ProductModel product = InputProductData();

                // Different Property to Set
                List<int> ids = SelectedProperty();

                // to get selected value property
                List<int> svp_ids = SelectedValueProperty(ids);

                //Integrate information
                List<ProductPrroperty> productProperties = productManagement.GetListProductPrroperties(product.ProductId, svp_ids);

                // to add properties to product
                productManagement.UpdateProduct(product.ProductId, productProperties);

            }
            else
            {
                // to add property and values
                InputDataForProperty();
                Product_InputInfo();
            }

        }
        public ProductModel InputProductData()
        {
            ProductModel product = new ProductModel();
            if (categoryManagement.GetCategories().Count != 0)
            {
                Console.WriteLine("first, you must choose Product Category. ");

                Console.WriteLine("Please enter Category Id?");
                Report_Category();
                string ctp = Console.ReadLine();
                do
                {
                    if (categoryManagement.GetCategories().Find(x => x.CategoryId == Convert.ToInt32(ctp)) != null)
                    {
                        product.CategoryId = Convert.ToInt32(ctp);

                    }
                    else
                    {
                        Console.WriteLine("Incorrect!!! Please enter Category Id?");

                        ctp = Console.ReadLine();
                    }
                } while (product.CategoryId == 0);

            }
            else
            {
                Console.WriteLine("Sorry!! first you must define Category. ");

                ActionMenu(Request.AddCategory);
                //InputProductData();
            }
            Console.WriteLine("Add a Product (enter xxx to exit)");
            Console.WriteLine("Name?");
            product.ProductName = Console.ReadLine();
            Console.WriteLine("description?");
            product.Description = Console.ReadLine();
            Console.WriteLine("About?");
            product.About = Console.ReadLine();


            productManagement.AddProduct(product);
            return product;
        }
        public List<int> SelectedProperty()
        {
            List<int> ids = new List<int>();
            Console.WriteLine("Please choose property(select different number to exit)");
            for (int i = 0; i < productManagement.GetProperties().Count; i++)
            {
                Console.WriteLine(productManagement.GetProperties()[i].PropertyId + " => " + productManagement.GetProperties()[i].Name);
            }
            int a = Convert.ToInt32(Console.ReadLine());
            do
            {
                if (productManagement.GetProperties().Find(p => p.PropertyId == a) != null)
                {
                    ids.Add(a);
                }
                Console.WriteLine("Please choose property(select 0 number to exit)");

                a = Convert.ToInt32(Console.ReadLine());
            } while (a != 0);
            return ids;
        }
        public List<int> SelectedValueProperty(List<int> ids)
        {
            List<int> svp_ids = new List<int>();
            List<ValuePropertyModel> svp = productManagement.SelectedValueProps(ids);
            Console.WriteLine("Please choose value property(select different number to exit)");

            foreach (ValuePropertyModel v in svp)
            {
                Console.WriteLine(v.ValuePropertyId + ". " + v.Name + " : " + v.PropertyValue);
             }
            int a = Convert.ToInt32(Console.ReadLine()); 
            do
            {
                if (svp.Find(s => s.ValuePropertyId == a) != null)
                {
                    svp_ids.Add(a);
                }
                Console.WriteLine("Please choose value property(select different number to exit)");
                a = Convert.ToInt32(Console.ReadLine());

            } while (a != 0);
            return svp_ids;
        }
        public void InputDataForProperty()
        {
            Console.Clear();
            Console.WriteLine("to add a product,first you must add a property");
            Console.WriteLine("Add Property Name");
            PropertyModel property = new PropertyModel();
            property.Name = Console.ReadLine();
            productManagement.AddProperty(property);
            Console.WriteLine("Add Property value (enter xxx to exit)");
            List<string> valueProperties = new List<string>();
            string inputValue = Console.ReadLine();
            while (inputValue != null && inputValue != "xxx")
            {
                valueProperties.Add(inputValue);
                Console.WriteLine("Add Property value (enter xxx to exit)");
                inputValue = Console.ReadLine();
            }
            productManagement.SetValueProperty(property.PropertyId, valueProperties);
        }
        public CategoryModel Category_InputInfo()
        {

            Console.WriteLine("Please write Category name? write xxx to exit");
            string categoryname = Console.ReadLine();
            if (categoryname != "xxx")
            {
                CategoryModel cm = categoryManagement.AddCategory(new CategoryModel() { CategoryName = categoryname });
                Console.WriteLine("Please write SubCategory name? write xxx to exit");
                categoryname = Console.ReadLine();
                while (categoryname != "xxx")
                {

                    categoryManagement.AddCategory(new CategoryModel() { ParentCategoryId = cm.CategoryId, CategoryName = categoryname });
                    Console.WriteLine("Please write SubCategory name? write xxx to exit");
                    categoryname = Console.ReadLine();
                }
            }
            return null;
        }
        public void Report_Product()
        {
            List<ProductModel> products = productManagement.GetProducts();
            foreach (ProductModel product in products)
            {
                //, product.ProductProperty.Item1.attributeValues[]
                Console.WriteLine("productId \t ProductName \t About \n description");
                Console.WriteLine("{0}\t{1}\t{2}\t{3}",product.ProductId,product.ProductName,product.About,product.Description);
                Console.WriteLine("Properties:\n");
                foreach(ProductPrroperty p in product.productPrroperties)
                {
                    Console.WriteLine("{0} => {1}",productManagement.GetNameProductProperty(p), productManagement.GetNameProductPropertiesValue(p));
                    
                    
                }

            }
        }
        public void Report_Category()
        {
            List<CategoryModel> categories = categoryManagement.GetCategories();
            foreach (CategoryModel category in categories)
            {
                Console.WriteLine(category.CategoryId + " => " + category.CategoryName);

            }
        }
    }
}
#region CommentedPreviousCode
//while (pname != "xxx")
//{
//    AttributeModel attmodel = new AttributeModel();
//    string txtname = "";
//    Tuple<AttributeModel,string, List<string>> tuple = new Tuple<AttributeModel,string, List<string>>(attmodel,txtname, new List<string>());


//    Console.WriteLine("Please write Attribute name? (color/type/description,...)write xxx to exit");
//    string attname = Console.ReadLine();
//    if (attname != "xxx")
//    {
//        Console.WriteLine("Please write Attribute value?  write xxx to exit");
//        string attvalue = Console.ReadLine();
//        attmodel.attributeValues = new Dictionary<string, string>() { { attname, attvalue } };
//        List<string> list = new List<string>();
//        Console.WriteLine($"Please write property name for {0}?  write yyy to exit", attname);
//       txtname = Console.ReadLine();
//        Console.WriteLine($"Please write property value for {0}?  write yyy to exit", txtname);

//        string txt = Console.ReadLine();
//        while (txt != "yyy")
//        {
//            Console.WriteLine($"Please write property value for {0}?  write yyy to exit", attname);

//            if (txt != "yyy")
//                list.Add(txt);
//            txt = Console.ReadLine();
//        }
//        tuple = Tuple.Create(attmodel,txtname, list);
//    }

//    return new ProductModel() { ProductName = pname, ProductProperty = tuple };
//}
#endregion CommentedPreviousCode
