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

        public string LunarActiveDate { get; set; }

        public string LunarCreatedDate { get; set; }

        public string LunarModifiedDate { get; set; }

        public bool? IsPaid { get; set; }

        public bool IsDeleted { get; set; }

        public decimal TotalPrice 
        { 
            get
            {
                decimal value = 0;
                if (Items != null)
                {
                    foreach (var item in Items)
                    {
                        if (item.Price != null)
                        {
                            value += item.Price.Value * (item.Weight != null ? item.Weight.Value : 0);
                        }
                    }
                }
                return value;
            }
        } 

        public IEnumerable<MeatBillPriceDTO> Items { get; set; }

    }
}
