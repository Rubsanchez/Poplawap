using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Poplawap.Backend.Model
{
    public class Sales
    {
        public int Id { get; set; }

        [Required]
        public int Goal { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<Comments> Comments { get; set; }

        public int ProductId { get; set; }
        public virtual Products Product { get; set; }

        public int StatusId { get; set; }
        public virtual Statuses Status { get; set; }

        public virtual ICollection<SalesCategories> Categories { get; set; }

    }
}
