using System.Diagnostics.CodeAnalysis;

namespace Accounting.Model.DTO
{
    public class PersonDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public bool? Source { get; set; }

        public bool? IsDeleted { get; set; }

        [AllowNull]
        public NearestTransaction NearestTransaction { get; set; }
    }

    public class NearestTransaction
    {
        public int Id { get; set; }

        public DateTime? ActivateDate { get; set; }

        public string LunarActiveDate { get; set; }
    }
}
