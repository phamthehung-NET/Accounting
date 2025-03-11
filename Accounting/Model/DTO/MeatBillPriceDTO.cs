namespace Accounting.Model.DTO
{
    public class MeatBillPriceDTO
    {
        public int? Id { get; set; }

        public int? MeatId { get; set; }

        public string MeatName { get; set; }

        public int? MeatType { get; set; }

        public bool Frozen { get; set; }

        public int? BillId { get; set; }

        public decimal? Price { get; set; }

        public int? PriceType { get; set; }

        public decimal? Weight { get; set; }

        public DateTime? ActiveDate { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public string LunarActiveDate { get; set; }

        public string LunarCreatedDate { get; set; }

        public string LunarModifiedDate { get; set; }

        public decimal ItemPrice
        {
            get
            {
                return (Weight ?? 0) * (Price ?? 0);
            }
        }
    }
}
