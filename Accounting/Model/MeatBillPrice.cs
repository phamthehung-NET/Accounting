using System.ComponentModel.DataAnnotations;

namespace Accounting.Model
{
    public class MeatBillPrice
    {
        [Key]
        public int Id { get; set; }

        public int? MeatId { get; set; }

        public int? BillId { get; set; }

        public decimal? Price { get; set; }

        public int PriceType { get; set; }

        public decimal Weight { get; set; }

        public DateTime? ActiveDate { get; set; }

        public string LunarActiveDate { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string LunarCreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public string LunarModifiedDate { get; set; }
    }
}
