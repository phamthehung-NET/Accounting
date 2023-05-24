namespace Accounting.Model.DTO
{
    public class MeatDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int? Type { get; set; }

        public bool? Prozen { get; set; }

        public bool? IsDeleted { get; set; }

        public decimal? Weight { get; set; }

        public decimal? YesterdayEntryPrice { get; set; }

        public decimal? TodayEntryPrice { get; set; }

        public decimal? YesterdaySalePrice { get; set; }

        public decimal? TodaySalePrice { get; set; }
    }
}
