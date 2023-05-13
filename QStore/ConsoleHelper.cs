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
                            productManagement.AddProduct(Product_InputInfo());
                        }
                        catch {
                            Console.Clear();
                            Console.WriteLine("return to main menu!!!");
                         }
                        break;
                    }
                case Request.AddCategory:
                    {
                        try
                        {
                            categoryManagement.AddCategory(Category_InputInfo());

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
        public ProductModel Product_InputInfo()
        {
            Console.Clear();

            if (productManagement.GetProperties() != null)
            {
                Console.WriteLine("Add a Product (enter xxx to exit)");
                Console.WriteLine("Name?");
                string pname = Console.ReadLine();
                Console.WriteLine("Please choose property");
                List<int> ids = new List<int>();
                for (int i = 0; i < productManagement.GetProperties().Count; i++)
                {
                    Console.WriteLine($"{0}. {1}",i,productManagement.GetProperties()[i].Name);
                    int a = Convert.ToInt32( Console.ReadLine());
                    if (a == i)
                    {
                        ids.Add(productManagement.GetProperties()[i].PropertyId);
                    } 
                }
                // to get selected value property
                List<int> svp_ids = new List<int>();

                List<ValuePropertyModel> svp=productManagement.SelectedValueProps(ids);
                foreach (ValuePropertyModel v in svp)
                {
                    Console.WriteLine($"{0}. {1} => {2}",v.ValuePropertyId, v.Name, v.PropertyValue);
                    int a = Convert.ToInt32(Console.ReadLine());
                    if (a == v.ValuePropertyId)
                    {
                        svp_ids.Add(v.ValuePropertyId);
                    }
                }


            }
            else
            {
                Console.WriteLine("to add a product,first you must add a property");
            }
            
            while (pname != "xxx")
            {
                AttributeModel attmodel = new AttributeModel();
                string txtname = "";
                Tuple<AttributeModel,string, List<string>> tuple = new Tuple<AttributeModel,string, List<string>>(attmodel,txtname, new List<string>());


                Console.WriteLine("Please write Attribute name? (color/type/description,...)write xxx to exit");
                string attname = Console.ReadLine();
                if (attname != "xxx")
                {
                    Console.WriteLine("Please write Attribute value?  write xxx to exit");
                    string attvalue = Console.ReadLine();
                    attmodel.attributeValues = new Dictionary<string, string>() { { attname, attvalue } };
                    List<string> list = new List<string>();
                    Console.WriteLine($"Please write property name for {0}?  write yyy to exit", attname);
                   txtname = Console.ReadLine();
                    Console.WriteLine($"Please write property value for {0}?  write yyy to exit", txtname);

                    string txt = Console.ReadLine();
                    while (txt != "yyy")
                    {
                        Console.WriteLine($"Please write property value for {0}?  write yyy to exit", attname);

                        if (txt != "yyy")
                            list.Add(txt);
                        txt = Console.ReadLine();
                    }
                    tuple = Tuple.Create(attmodel,txtname, list);
                }

                return new ProductModel() { ProductName = pname, ProductProperty = tuple };
            }
            return null;
        }
        public CategoryModel Category_InputInfo()
        {

            Console.WriteLine("Please write Category name? write xxx to exit");
            string categoryname = Console.ReadLine();
            if (categoryname != "xxx")
            {
                return new CategoryModel() { CategoryName = categoryname };
            }
            return null;
        }
        public void Report_Product()
        {
            List<ProductModel> products = productManagement.GetProducts();
            foreach (ProductModel product in products)
            {
                //, product.ProductProperty.Item1.attributeValues[]
                Console.WriteLine($"{0}\t{1}", product.ProductName);
                
            }
        }
        public void Report_Category()
        {
            List<CategoryModel> categories = categoryManagement.GetCategories();
            foreach (CategoryModel category in categories)
            {
                Console.WriteLine("${1}\n", category.CategoryName);

            }
        }
    }
}
