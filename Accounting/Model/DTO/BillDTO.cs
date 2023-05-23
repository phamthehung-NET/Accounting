namespace Accounting.Model.DTO
{
    public class BillDTO
    {
        public int Id { get; set; }

        public int PersonId { get; set; }

        public string PersonName { get; set; }

        public int Type { get; set; }

        public DateTime? ActiveDate { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public bool? IsPaid { get; set; }

        public IEnumerable<MeatBillPrice> Items { get; set; }
    }
}
