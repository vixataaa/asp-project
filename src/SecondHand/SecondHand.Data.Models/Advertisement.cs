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
        public string Title { get; set; }
        
        public string Description { get; set; }
        
        public decimal Price { get; set; }

        public virtual ICollection<Photo> Photos { get; set; }
        
        public virtual ApplicationUser AddedBy { get; set; }
        
        public virtual Category Category { get; set; }
        
        public CurrencyType CurrencyType { get; set; }
    }
}
