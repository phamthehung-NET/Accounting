using System.ComponentModel.DataAnnotations;

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

        public bool IsPaid { get; set; }
    }
}
