namespace Accounting.Model.DTO
{
    public class DashboardDTO
    {
        public List<MeatBillPriceDTO> EntryMeats { get; set; } = new();

        public List<MeatBillPriceDTO> SaleMeats { get; set; } = new();

        public List<MeatBillPriceDTO> Result
        {
            get
            {
                var list = new List<MeatBillPriceDTO>();
                SaleMeats.ForEach(sale =>
                {
                    var entry = EntryMeats.FirstOrDefault(x => x.MeatId == sale.MeatId);
                    if (entry != null)
                    {
                        MeatBillPriceDTO meat = new()
                        {
                            MeatId = sale.MeatId,
                            MeatName = sale.MeatName,
                            MeatType = sale.MeatType,
                            ActiveDate = sale.ActiveDate,
                            CreatedDate = sale.CreatedDate,
                            Weight = entry.Weight - sale.Weight,
                            ModifiedDate = sale.ModifiedDate,
                        };
                        list.Add(meat);
                    }
                });
                return list;
            }
        }

        public decimal WastedWeight
        {
            get
            {
                decimal total = 0;
                Result.ForEach(x =>
                {
                    total += x.Weight != null ? x.Weight.Value : 0;
                });

                return total;
            }
        }
    }
}
