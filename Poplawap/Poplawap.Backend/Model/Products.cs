using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Poplawap.Backend.Model
{
    public class Products
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string ProductName { get; set; }

        [Required]
        [MaxLength]
        public string Description { get; set; }

        [Required]
        public double Prize { get; set; }

        [Required]
        public DateTime PublishedDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int SaleId { get; set; }
        public virtual Sales Sale { get; set; }

        public virtual ICollection<ProductImages> ProductImages { get; set; }
    }
}
