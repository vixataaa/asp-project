using SecondHand.Data.Models.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondHand.Data.Models
{
    public class Message : DataModel
    {
        [Required]
        public string Text { get; set; }

        public virtual ApplicationUser Author { get; set; }

        public virtual Chat Chat { get; set; }
    }
}
