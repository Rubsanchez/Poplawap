using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Poplawap.Backend.Model
{
    public class ProductImages
    {
        public int Id { get; set; }

        [Required]
        [MaxLength]
        public string Base64 { get; set; }

        [Required]
        [StringLength(200)]
        public string ImageAlt { get; set; }

        public int ProductId { get; set; }
        public virtual Products Product { get; set; }
    }
}
