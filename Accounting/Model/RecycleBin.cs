using System.ComponentModel.DataAnnotations;

namespace Accounting.Model
{
    public class RecycleBin
    {
        [Key]
        public int Id { get; set; }

        public int? ObjectId { get; set; }

        public int Type { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string LunarCreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public string LunarModifiedDate { get; set; }
    }
}
