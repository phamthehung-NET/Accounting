using Accounting.Data;
using Accounting.Model.DTO;
using Accounting.Repositories.Interfaces;

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
            //var data = context.MeatBillPrices.Where(x => x.ActiveDate.Value.Date.CompareTo(date.Value.Date) == 0).Join(context.Meats, a => a.MeatId, b => b.Id, );

            //DashboardDTO returnedData = new()
            //{
            //    EntryMeats = data.Where(x => x.PriceType == (int)PriceType.Entry).ToList(),
            //    SaleMeats = data.Where(x => x.PriceType == (int)PriceType.Sale).ToList(),

            //};
            //return returnedData;
            throw new NotImplementedException();
        }
    }
}
