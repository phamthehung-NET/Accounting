using Accounting.Data;
using Accounting.Model.DTO;
using Accounting.Repositories.Interfaces;
using Accounting.Utilities;

namespace Accounting.Repositories.Implementations
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly AccountingDbContext context;

        public DashboardRepository(AccountingDbContext _context)
        {
            context = _context;
        }

        public DashboardDTO GetWastedWeight(DateTime? date)
        {
            var data = context.MeatBillPrices.Where(x => x.ActiveDate.Value.Date.CompareTo(date.Value.Date) == 0).Join(context.Meats, a => a.MeatId, b => b.Id, (a, b) =>
            new MeatBillPriceDTO
            {
                Id = a.Id,
                BillId = a.BillId,
                CreatedDate = a.CreatedDate,
                ModifiedDate = a.ModifiedDate,
                Price = a.Price,
                Weight = a.Weight,
                PriceType = a.PriceType,
                ActiveDate = a.ActiveDate,
                MeatId = b.Id,
                MeatName = b.Name,
                MeatType = b.Type,
            });

            DashboardDTO returnedData = new()
            {
                EntryMeats = data.Where(x => x.PriceType == (int)PriceType.Entry).ToList(),
                SaleMeats = data.Where(x => x.PriceType == (int)PriceType.Sale).ToList(),
            };
            return returnedData;
        }

        public List<DashboardDTO> GetChartData()
        {
            List<DashboardDTO> returnedData = new();
            var dateTime = DateTime.Now;

            var data = context.MeatBillPrices
                .Where(x => x.ActiveDate.Value.Date.CompareTo(dateTime.AddDays(-10).Date) >= 0 && x.ActiveDate.Value.Date.CompareTo(dateTime.Date) <= 0)
                .Join(context.Meats, a => a.MeatId, b => b.Id, (a, b) =>
            new MeatBillPriceDTO
            {
                Id = a.Id,
                BillId = a.BillId,
                CreatedDate = a.CreatedDate,
                ModifiedDate = a.ModifiedDate,
                Price = a.Price,
                Weight = a.Weight,
                PriceType = a.PriceType,
                ActiveDate = a.ActiveDate,
                MeatId = b.Id,
                MeatName = b.Name,
                MeatType = b.Type,
            }).GroupBy(x => new { x.ActiveDate.Value.Date })
            .Select(x => new
            {
                ActivateDate = x.Key.Date,
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
                }).Where(x => x.Id != null && x.Id > 0)
            }).OrderBy(x => x.ActivateDate);
            
            foreach(var item in data)
            {
                DashboardDTO wastedData = new()
                {
                    EntryMeats = item.Data.Where(x => x.PriceType == (int)PriceType.Entry).ToList(),
                    SaleMeats = item.Data.Where(x => x.PriceType == (int)PriceType.Sale).ToList(),
                    ActivateDate = item.ActivateDate,
                };
                returnedData.Add(wastedData);
            };

            return returnedData;
        }
    }
}
