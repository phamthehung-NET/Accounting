using System.ComponentModel.DataAnnotations;

namespace Accounting.Model
{
    public class MeatPrice
    {
        [Key]
        public int Id { get; set; }

        public int? MeatId { get; set; }

        public decimal? Price { get; set; }

        public int PriceType { get; set; }

        public DateTime? ActiveDate { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }
    }
}
