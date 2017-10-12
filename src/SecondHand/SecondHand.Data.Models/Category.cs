using SecondHand.Data.Models.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondHand.Data.Models
{
    public class Category : DataModel
    {
        [Required]
        public string Name { get; set; }

        public virtual ICollection<Advertisement> Advertisements { get; set; }
    }
}
