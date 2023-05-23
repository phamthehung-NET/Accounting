using Accounting.Common;
using Accounting.Data;
using Accounting.Model;
using Accounting.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Accounting.Repositories.Implementations
{
    public class PriceRepository : IPriceRepository
    {
        private readonly AccountingDbContext context;
        private readonly IHttpContextAccessor httpContext;
        private readonly UserManager<CustomUser> userManager;

        public PriceRepository(AccountingDbContext _context, IHttpContextAccessor _httpContext, UserManager<CustomUser> _userManager)
        {
            context = _context;
            httpContext = _httpContext;
            userManager = _userManager;
        }

        public bool UpdateItemPrice(Dictionary<int, int> inputEntryPrice, Dictionary<int, int> inputSalePrice)
        {
            var user = userManager.FindByNameAsync(httpContext.HttpContext.User.Identity.Name).Result;
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
            user.UpdatedPriceDate = DateTime.Now;
            userManager.UpdateAsync(user);
            context.SaveChanges();
            return true;
        }
    }
}
