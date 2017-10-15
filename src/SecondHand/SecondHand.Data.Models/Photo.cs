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
        public string Url { get; set; }

        public virtual Advertisement Advertisement { get; set; }
    }
}
