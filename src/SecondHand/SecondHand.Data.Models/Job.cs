using SecondHand.Data.Models.Abstract;

namespace SecondHand.Data.Models
{
    public class Job : DataModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public virtual Firm AddedBy { get; set; }
    }
}
