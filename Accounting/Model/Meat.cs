using System.ComponentModel.DataAnnotations;

namespace Accounting.Model
{
    public class Meat
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public int? Type { get; set; }

        public bool? Prozen { get; set; }

        public bool? IsDeleted { get; set; }
    }
}
