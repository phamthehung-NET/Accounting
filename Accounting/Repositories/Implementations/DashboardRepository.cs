using Accounting.Data;
using Accounting.Model;
using Accounting.Model.DTO;
using Accounting.Repositories.Interfaces;
using Accounting.Utilities;
using BlazorDateRangePicker;

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
                ActiveDate = a.ActiveDate,
                CreatedDate = a.CreatedDate,
                ModifiedDate = a.ModifiedDate,
                LunarActiveDate = a.LunarActiveDate,
                LunarCreatedDate = a.LunarCreatedDate,
                LunarModifiedDate = a.LunarModifiedDate,
                Price = a.Price,
                Weight = a.Weight,
                PriceType = a.PriceType,
                MeatId = b.Id,
                MeatName = b.Name,
                MeatType = b.Type,
            });

            DashboardDTO returnedData = new()
            {
                EntryMeats = data.Where(x => x.PriceType == (int)PriceType.Entry).ToList(),
                SaleMeats = data.Where(x => x.PriceType == (int)PriceType.Sale).ToList(),
            };

            var bill = context.Bills.FirstOrDefault(x => x.ActiveDate.Value.Date.CompareTo(date.Value.Date) == 0 && x.PersonId == 0);

            if (bill != null)
            {
                returnedData.RestMeatWeight = bill.RestMeatWeight;
            }

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
                ActiveDate = a.ActiveDate,
                CreatedDate = a.CreatedDate,
                ModifiedDate = a.ModifiedDate,
                LunarActiveDate = a.LunarActiveDate,
                LunarCreatedDate = a.LunarCreatedDate,
                LunarModifiedDate = a.LunarModifiedDate,
                Price = a.Price,
                Weight = a.Weight,
                PriceType = a.PriceType,
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
                    ActiveDate = y.ActiveDate,
                    CreatedDate = y.CreatedDate,
                    ModifiedDate = y.ModifiedDate,
                    LunarActiveDate = y.LunarActiveDate,
                    LunarCreatedDate = y.LunarCreatedDate,
                    LunarModifiedDate = y.LunarModifiedDate,
                    Price = y.Price,
                    Weight = y.Weight,
                    PriceType = y.PriceType,
                    MeatId = y.MeatId,
                    MeatName = y.MeatName,
                    MeatType = y.MeatType,
                }).Where(x => x.Id != null && x.Id > 0)
            }).OrderBy(x => x.ActivateDate);

            var bills = context.Bills.Where(x => x.ActiveDate.Value.Date.CompareTo(dateTime.AddDays(-10).Date) >= 0 && x.ActiveDate.Value.Date.CompareTo(dateTime.Date) <= 0 && x.PersonId == 0).ToList();

            foreach (var item in data)
            {
                var bill = bills.FirstOrDefault(x => x.ActiveDate.Value.Date.CompareTo(item.ActivateDate.Date) == 0);
                DashboardDTO wastedData = new()
                {
                    EntryMeats = item.Data.Where(x => x.PriceType == (int)PriceType.Entry).ToList(),
                    SaleMeats = item.Data.Where(x => x.PriceType == (int)PriceType.Sale).ToList(),
                    ActivateDate = item.ActivateDate,
                    RestMeatWeight = bill != null ? bill.RestMeatWeight : 0
                };
                returnedData.Add(wastedData);
            };

            for(var i = DateTime.Now.AddDays(-9); i <= DateTime.Now; i = i.AddDays(1))
            {
                if(!returnedData.Any(x => DateTime.Compare(x.ActivateDate.Date, i) == 0))
                {
                    DashboardDTO wastedData = new()
                    {
                        EntryMeats = new(),
                        SaleMeats = new(),
                        ActivateDate = i,
                        RestMeatWeight = 0
                    };
                    returnedData.Add(wastedData);
                }
            }

            return returnedData.OrderBy(x => x.ActivateDate).ToList();
        }

        public decimal GetRestMeatInADay(DateTime dateTime)
        {
            var data = context.Bills.FirstOrDefault(x => x.ActiveDate.Value.CompareTo(dateTime) == 0 && x.PersonId == 0);
            if (data != null)
            {
                return data.RestMeatWeight;
            }
            return 0;
        }

        public List<(DateTime, decimal)> GetRestMeatInDays(DateRange range)
        {
            var data = context.Bills.Where(x => x.ActiveDate.Value.Date.CompareTo(range.Start.Date) >= 0 && x.ActiveDate.Value.Date.CompareTo(range.End.Date) <= 0).ToList();
            return data.Select(x => (x.ActiveDate.Value, x.RestMeatWeight)).ToList();
        }

        public List<DebtDashboardDTO> GetDebtData(DasboardDebtFilterType type, int numberItemTake = 0)
        {
            var data = context.People.Where(person => !person.IsDeleted.Value)
                .Select(person => new
                {
                    PersonId = person.Id,
                    PersonName = person.Name,
                    Bills = context.Bills
                        .Where(bill => bill.PersonId == person.Id && bill.Id > 0 && !bill.IsPaid && !bill.IsDeleted)
                        .Select(bill => new BillDTO
                        {
                            Id = bill.Id,
                            ActiveDate = bill.ActiveDate,
                            LunarActiveDate = bill.LunarActiveDate,
                            PaidAmount = bill.PaidAmount,
                            Items = context.MeatBillPrices
                                .Where(meat => meat.BillId == bill.Id && meat.PriceType == (int)PriceType.Sale)
                                .Select(mbp => new MeatBillPriceDTO
                                {
                                    Id = mbp.Id,
                                    Weight = mbp.Weight,
                                    Price = mbp.Price
                                }).AsEnumerable()
                        }).AsEnumerable()
                });

            List<DebtDashboardDTO> returnedData = new();
            IEnumerable<DebtDashboardDTO> orderedData = new List<DebtDashboardDTO>();
            foreach (var item in data)
            {
                var debt = new DebtDashboardDTO
                {
                    Name = item.PersonName,
                    Debt = item.Bills.Sum(x => x.TotalPrice) - item.Bills.Sum(x => x.PaidAmount),
                    Date = item.Bills.OrderBy(x => x.ActiveDate.Value).FirstOrDefault()?.ActiveDate.Value ?? DateTime.Now,
                    LunarDate = item.Bills.OrderBy(x => x.ActiveDate.Value).FirstOrDefault()?.LunarActiveDate,
                };
                if(debt.Debt > 0)
                {
                    returnedData.Add(debt);
                }
            }
            switch (type)
            {
                case DasboardDebtFilterType.MostDebt:
                    orderedData = returnedData.OrderByDescending(x => x.Debt);
                    break;
                case DasboardDebtFilterType.LongestDebt:
                    orderedData = returnedData.OrderBy(x => x.Date);
                    break;
                default: break;
            }
            if (numberItemTake > 0)
            {
                return orderedData.Where(x => x.Debt > 0).Take(numberItemTake).ToList();
            }
            return orderedData.ToList();
        }
    }
}
