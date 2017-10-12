using SecondHand.Data.Models.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondHand.Data.Models
{
    public class Photo : DataModel
    {
        [Required]
        public string Url { get; set; }

        [Required]
        public virtual Advertisement Advertisement { get; set; }
    }
}
