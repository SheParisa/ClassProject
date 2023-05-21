using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QStore.Models
{
    public class ValuePropertyModel:PropertyModel
    {
        public int ValuePropertyId { get; set; }
        public string PropertyValue { get; set; }
        public int PropertyId { get; set; }
    }
}
