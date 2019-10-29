using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Poplawap.Backend.Model
{
    public class SalesCategories
    {
        public int SaleId { get; set; }
        public virtual Sales Sale { get; set; }

        public int CategoryId { get; set; }
        public virtual Categories Category {get; set;}
    }
}
