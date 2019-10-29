using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Poplawap.Backend.Model
{
    public class Comments
    {
        public int Id { get; set; }

        [Required]
        [MaxLength]
        public string CommentMessage { get; set; }
        
        [Required]
        public DateTime Date { get; set; }

        public int? ParentComment { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public int SaleId { get; set; }
        public virtual Sales Sale { get; set; }
    }
}
