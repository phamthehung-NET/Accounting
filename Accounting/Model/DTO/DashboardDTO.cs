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
                var entries = EntryMeats.GroupBy(x => x.MeatId).Select(x => new
                {
                    MeatId = x.Key.Value,
                    Data = x.Select(y => new MeatBillPriceDTO
                    {
                        Id = y.Id,
                        BillId = y.BillId,
                        CreatedDate = y.CreatedDate,
                        ModifiedDate = y.ModifiedDate,
                        Price = y.Price,
                        Weight = y.Weight,
                        PriceType = y.PriceType,
                        ActiveDate = y.ActiveDate,
                        MeatId = y.MeatId,
                        MeatName = y.MeatName,
                        MeatType = y.MeatType,
                    })
                }).ToList();
                SaleMeats.GroupBy(x => x.MeatId).Select(x => new
                {
                    MeatId = x.Key.Value,
                    Data = x.Select(y => new MeatBillPriceDTO
                    {
                        Id = y.Id,
                        BillId = y.BillId,
                        CreatedDate = y.CreatedDate,
                        ModifiedDate = y.ModifiedDate,
                        Price = y.Price,
                        Weight = y.Weight,
                        PriceType = y.PriceType,
                        ActiveDate = y.ActiveDate,
                        MeatId = y.MeatId,
                        MeatName = y.MeatName,
                        MeatType = y.MeatType,
                    })
                }).ToList().ForEach(sale =>
                {
                    var entry = entries.FirstOrDefault(x => x.MeatId == sale.MeatId);
                    if (entry != null)
                    {
                        MeatBillPriceDTO meat = new()
                        {
                            MeatId = sale.MeatId,
                            MeatName = sale.Data.First().MeatName,
                            MeatType = sale.Data.First().MeatType,
                            ActiveDate = sale.Data.First().ActiveDate,
                            CreatedDate = sale.Data.First().CreatedDate,
                            ModifiedDate = sale.Data.First().ModifiedDate,
                        };
                        decimal totalEntry = 0;
                        decimal totalSale = 0;
                        foreach (var item in sale.Data)
                        {
                            totalSale += item.Weight.Value;
                        }
                        foreach (var item in entry.Data)
                        {
                            totalEntry += item.Weight.Value;
                        }
                        meat.Weight = totalEntry - totalSale;
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
