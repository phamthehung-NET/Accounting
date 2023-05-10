using System.ComponentModel.DataAnnotations;

namespace Accounting.Model
{
    public class Person
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public bool? Source { get; set; }

        public bool? IsDeleted { get; set; }
    }
}
