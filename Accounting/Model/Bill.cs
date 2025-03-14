using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Accounting.Model
{
    public class Bill
    {
        [Key]
        public int Id { get; set; }

        public int PersonId { get; set; }

        public int Type { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? ActiveDate { get; set; }

        public string LunarActiveDate { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string LunarCreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public string LunarModifiedDate { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal PaidAmount { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal RestMeatWeight { get; set; }

        public bool IsPaid { get; set; }
    }
}
