using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QStore.Models
{
    public class ProductPrroperty
    {
        public int IdProductProperty { get; set; }
        
        public Tuple<int ,int, int> VProductProperty { get; set; }
    }
}
