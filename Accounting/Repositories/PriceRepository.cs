using Accounting.Common;
using Accounting.Data;
using Accounting.Model;

namespace Accounting.Repositories
{
    public class PriceRepository : IPriceRepository
    {
        private readonly AccountingDbContext context;

        public PriceRepository(AccountingDbContext _context)
        {
            context = _context;
        }

        public bool UpdateItemPrice(Dictionary<int, int> inputEntryPrice, Dictionary<int, int> inputSalePrice)
        {
            if (!inputEntryPrice.Any() && !inputSalePrice.Any())
            {
                return false;
            }
            List<MeatPrice> priceList = new();
            foreach (var item in inputEntryPrice)
            {
                var entryDb = context.MeatPrices.FirstOrDefault(x => x.MeatId == item.Key && x.ActiveDate.Value.Date.CompareTo(DateTime.Now.Date) == 0 && x.PriceType == (int)Constants.PriceType.Entry);
                if (entryDb == null)
                {
                    MeatPrice meatPrice = new()
                    {
                        MeatId = item.Key,
                        Price = item.Value,
                        ActiveDate = DateTime.Now,
                        CreatedDate = DateTime.Now,
                        PriceType = (int)Constants.PriceType.Entry,
                    };
                    priceList.Add(meatPrice);
                }
                else
                {
                    entryDb.Price = item.Value;
                }
            }

            foreach (var item in inputSalePrice)
            {
                var saleDb = context.MeatPrices.FirstOrDefault(x => x.MeatId == item.Key && x.ActiveDate.Value.Date.CompareTo(DateTime.Now.Date) == 0 && x.PriceType == (int)Constants.PriceType.Sale);
                if (saleDb == null)
                {
                    MeatPrice meatPrice = new()
                    {
                        MeatId = item.Key,
                        Price = item.Value,
                        ActiveDate = DateTime.Now,
                        CreatedDate = DateTime.Now,
                        PriceType = (int)Constants.PriceType.Sale,
                    };
                    priceList.Add(meatPrice);
                }
                else
                {
                    saleDb.Price = item.Value;
                }
            }

            context.MeatPrices.AddRange(priceList);
            context.SaveChanges();
            return true;
        }
    }
}
