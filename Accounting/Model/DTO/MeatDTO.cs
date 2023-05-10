namespace Accounting.Model.DTO
{
    public class MeatDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int? Type { get; set; }

        public bool? Prozen { get; set; }

        public bool? IsDeleted { get; set; }

        public double? YesterdayEntryPrice { get; set; }

        public double? TodayEntryPrice { get; set; }

        public double? YesterdaySalePrice { get; set; }

        public double? TodaySalePrice { get; set; }
    }
}
