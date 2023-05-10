using System.ComponentModel.DataAnnotations;

namespace Accounting.Model
{
    public class MeatSalePrice
    {
        [Key]
        public int Id { get; set; }

        public int? MeatId { get; set; }

        public int? BillId { get; set; }

        public double? Price { get; set; }

        public DateTime? ActiveDate { get; set; }
        
        public DateTime? CreatedDate { get; set; }
        
        public DateTime? ModifiedDate { get; set; }
    }
}
