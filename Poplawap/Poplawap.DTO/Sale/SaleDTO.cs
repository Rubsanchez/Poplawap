using System;
using System.Collections.Generic;
using System.Text;

namespace Poplawap.DTO
{
    public class SaleDTO
    {        
        public string ProductName { get; set; }
        public string Description { get; set; }
        public double Prize { get; set; }
        public DateTime PublishedDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int Goal { get; set; }
        public int Status { get; set; }
        public List<string> Images { get; set; }
    }
}
