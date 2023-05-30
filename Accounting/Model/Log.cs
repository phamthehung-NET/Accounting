using System.ComponentModel.DataAnnotations;

namespace Accounting.Model
{
    public class Log
    {
        [Key]
        public int Id { get; set; }

        public string Location { get; set; }

        public string StackTrace { get; set; }

        public string Message { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
