using System.ComponentModel.DataAnnotations;

namespace Accounting.Model
{
    public class History
    {
        [Key]
        public int Id { get; set; }

        public int ObjectId { get; set; }

        public int Type { get; set; }

        public DateTime CreatedDate { get; set; }

        public int Action { get; set; }

        public string Content { get; set; }
    }
}
