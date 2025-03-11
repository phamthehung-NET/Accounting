using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Accounting.Model
{
    public class MeatPrice
    {
        [Key]
        public int Id { get; set; }

        public int? MeatId { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Price { get; set; }

        public int PriceType { get; set; }

        public DateTime? ActiveDate { get; set; }

        public string LunarActiveDate { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string LunarCreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }
        
        public string LunarModifiedDate { get; set; }
    }
}
