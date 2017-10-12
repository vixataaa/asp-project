using SecondHand.Data.Models.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondHand.Data.Models
{
    public class Advertisement : DataModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        public virtual ICollection<Photo> Photos { get; set; }

        [Required]
        public virtual ApplicationUser AddedBy { get; set; }

        [Required]
        public virtual Category Category { get; set; }

        [Required]
        public CurrencyType CurrencyType { get; set; }
    }
}
