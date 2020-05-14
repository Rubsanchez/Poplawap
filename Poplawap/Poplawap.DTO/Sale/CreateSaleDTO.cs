using System;
using System.Collections.Generic;
using System.Text;

namespace Poplawap.DTO
{
    public class CreateSaleDTO
    {
        public string UserEmail { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public double Prize { get; set; }
        public DateTime? EndDate { get; set; }
        public int Goal { get; set; }
        public List<string> Images { get; set; }
        public List<int> Categories { get; set; }
    }
}
