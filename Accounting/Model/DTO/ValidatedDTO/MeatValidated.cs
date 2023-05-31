using Accounting.Utilities;

namespace Accounting.Model.DTO.ValidatedDTO
{
    public class MeatValidated
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int? Type { get; set; }

        public bool? Frozen { get; set; }

        public bool? IsDeleted { get; set; }

        public decimal? Weight { get; set; }

        public decimal? YesterdayEntryPrice { get; set; }

        public decimal? TodayEntryPrice { get; set; }

        public decimal? YesterdaySalePrice { get; set; }

        public decimal? TodaySalePrice { get; set; }
    }
}
