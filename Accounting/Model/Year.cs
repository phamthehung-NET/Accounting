using System.ComponentModel.DataAnnotations;

namespace Accounting.Model
{
    public class Year
    {
        [Key]
        public int Id { get; set; }

        public int Name { get; set; }

        public bool IsLeapYear { get; set; }
    }
}
