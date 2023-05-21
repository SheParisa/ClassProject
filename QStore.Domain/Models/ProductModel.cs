using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QStore.Models
{
    public class ProductModel
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public string ProductName { get; set; }
        //public Tuple<AttributeModel,string ,List<string>> ProductProperty { get; set; }
        public string Description { get; set; }
        public string About { get; set; }
        public List<ProductPrroperty> productPrroperties { get; set; }
    }
}
