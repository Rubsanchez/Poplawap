using System;
using System.Collections.Generic;
using System.Text;

namespace Poplawap.DTO
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentCategory { get; set; }
        public string Icon { get; set; }
    }
}
