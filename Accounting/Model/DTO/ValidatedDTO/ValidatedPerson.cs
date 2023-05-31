using Accounting.Utilities;

namespace Accounting.Model.DTO.ValidatedDTO
{
    public class ValidatedPerson
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [PhoneNumber]
        public string PhoneNumber { get; set; }

        [Required]
        public string Address { get; set; }

        public bool? Source { get; set; }

        public bool? IsDeleted { get; set; }
    }
}
