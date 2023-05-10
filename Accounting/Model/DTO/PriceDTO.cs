namespace Accounting.Model.DTO
{
    public class PriceDTO
    {
        public int Id { get; set; }

        public int? MeatId { get; set; }

        public double? Price { get; set; }

        public DateTime? ActiveDate { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int PriceType { get; set; }
    }
}
