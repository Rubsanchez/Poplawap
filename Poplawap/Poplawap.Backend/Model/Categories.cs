using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Poplawap.Backend.Model
{
    public class Categories
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        
        public int? ParentCategory { get; set; }

        public virtual ICollection<SalesCategories> Sales { get; set; }
    }
}
