using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Poplawap.Backend.Model
{
    public class Statuses
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        public virtual ICollection<Sales> Sales { get; set; }
    }
}
