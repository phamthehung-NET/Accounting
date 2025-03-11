using Accounting.Data;
using Accounting.Model;
using Accounting.Repositories.Interfaces;
using Accounting.Utilities;
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

        public async Task<bool> UpdateItemPrice(Dictionary<int, int?> inputEntryPrice, Dictionary<int, int?> inputSalePrice, DateTime? date)
        {
            date ??= DateTime.Now;
            var currentLunarDate = HelperFunctions.ConvertSolarToLunar(date.Value);
            var user = userManager.FindByNameAsync(httpContext.HttpContext.User.Identity.Name).Result;
            if (!inputEntryPrice.Any() && !inputSalePrice.Any())
            {
                List<MeatPrice> addList = new();
                var priceDb = context.MeatPrices.Where(x => x.ActiveDate.Value.Date.CompareTo(date.Value.Date) == 0);
                if (!priceDb.Any())
                {
                    var yesterdayPriceDb = context.MeatPrices.Where(x => x.ActiveDate.Value.Date.CompareTo(date.Value.AddDays(-1).Date) == 0);
                    foreach (var x in yesterdayPriceDb)
                    {
                        if (x.Price == null)
                        {
                            var lastestPrice = context.MeatPrices
                                .Where(y => y.MeatId == x.MeatId && y.ActiveDate.Value.Date.CompareTo(date.Value.Date) < 0 && y.PriceType == x.PriceType && y.Price != null);
                            x.Price = lastestPrice.Any() ? lastestPrice.MaxBy(y => y.ActiveDate).Price : null;
                        }
                        else
                        {
                            MeatPrice meatPrice = new()
                            {
                                MeatId = x.MeatId,
                                ActiveDate = date.Value,
                                CreatedDate = date.Value,
                                LunarActiveDate = currentLunarDate,
                                LunarCreatedDate = currentLunarDate,
                                PriceType = x.PriceType,
                                Price = x.Price != null ? x.Price : null,
                            };
                            addList.Add(meatPrice);
                        }
                    }
                    context.AddRange(addList);
                }
            }
            List<MeatPrice> priceList = new();
            foreach (var item in inputEntryPrice)
            {
                var entryDb = context.MeatPrices.FirstOrDefault(x => x.MeatId == item.Key && x.ActiveDate.Value.Date.CompareTo(date.Value.Date) == 0 && x.PriceType == (int)PriceType.Entry);
                if (entryDb == null)
                {
                    MeatPrice meatPrice = new()
                    {
                        MeatId = item.Key,
                        ActiveDate = date.Value,
                        CreatedDate = date.Value,
                        LunarActiveDate = currentLunarDate,
                        LunarCreatedDate = currentLunarDate,
                        PriceType = (int)PriceType.Entry,
                    };

                    if (item.Value == null)
                    {
                        var lastestPrice = context.MeatPrices
                            .Where(x => x.MeatId == item.Key && x.ActiveDate.Value.Date.CompareTo(date.Value.Date) < 0 && x.PriceType == (int)PriceType.Entry && x.Price != null)
                            .MaxBy(x => x.ActiveDate);

                        meatPrice.Price = lastestPrice != null ? lastestPrice.Price : null;
                    }
                    else
                    {
                        meatPrice.Price = item.Value;
                    }
                    priceList.Add(meatPrice);
                }
                else
                {
                    entryDb.Price = item.Value;
                }
            }

            foreach (var item in inputSalePrice)
            {
                var saleDb = context.MeatPrices.FirstOrDefault(x => x.MeatId == item.Key && x.ActiveDate.Value.Date.CompareTo(date.Value.Date) == 0 && x.PriceType == (int)PriceType.Sale);
                if (saleDb == null)
                {
                    MeatPrice meatPrice = new()
                    {
                        MeatId = item.Key,
                        ActiveDate = date.Value,
                        CreatedDate = date.Value,
                        LunarActiveDate = currentLunarDate,
                        LunarCreatedDate = currentLunarDate,
                        PriceType = (int)PriceType.Sale,
                    };

                    if (item.Value == null)
                    {
                        var lastestPrice = context.MeatPrices
                            .Where(x => x.MeatId == item.Key && x.ActiveDate.Value.Date.CompareTo(date.Value.Date) < 0 && x.PriceType == (int)PriceType.Sale && x.Price != null)
                            .MaxBy(x => x.ActiveDate);

                        meatPrice.Price = lastestPrice != null ? lastestPrice.Price : null;
                    }
                    else
                    {
                        meatPrice.Price = item.Value;
                    }
                    priceList.Add(meatPrice);
                }
                else
                {
                    saleDb.Price = item.Value;
                }
            }

            await context.MeatPrices.AddRangeAsync(priceList);
            user.UpdatedPriceDate = date.Value;
            await userManager.UpdateAsync(user);
            await context.SaveChangesAsync();
            return true;
        }
    }
}
