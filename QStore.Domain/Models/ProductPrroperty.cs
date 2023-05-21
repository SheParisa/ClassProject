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

       // private Tuple<int, int, int> fdguple = new Tuple<int, int, int>(1, 1, 1);
        public Tuple<int, int, int> VProductProperty { get; set; }= new Tuple<int, int, int>(1, 1, 1);
    }
}
