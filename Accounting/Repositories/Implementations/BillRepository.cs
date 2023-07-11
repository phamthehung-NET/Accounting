using Accounting.Data;
using Accounting.Model;
using Accounting.Model.DTO;
using Accounting.Pages;
using Accounting.Repositories.Interfaces;
using Accounting.Utilities;
using Microsoft.Extensions.Localization;

namespace Accounting.Repositories.Implementations
{
    public class BillRepository : IBillRepository
    {
        private readonly AccountingDbContext context;
        private readonly IStringLocalizer<Resource> Lres;
        private readonly bool IsLeapYear;

        public BillRepository(AccountingDbContext _context, IStringLocalizer<Resource> _Lres)
        {
            context = _context;
            Lres = _Lres;
            IsLeapYear = bool.Parse(context.Settings.FirstOrDefault(x => x.Name.Equals(Constants.IsLeapYearSetting)).Value);
        }

        public async Task<bool> AddBill(BillDTO res, DateTime? currentDate)
        {
            currentDate ??= DateTime.Now;
            var currentLunarDate = HelperFunctions.GetLunarDate(IsLeapYear, currentDate.Value);
            var billDb = context.Bills.FirstOrDefault(x => x.PersonId == res.PersonId && x.ActiveDate.Value.Date == currentDate.Value.Date && x.Type == res.Type && x.IsDeleted == false);
            if (billDb == null)
            {
                Bill bill = new()
                {
                    PersonId = res.PersonId,
                    Type = res.Type,
                    ActiveDate = currentDate.Value,
                    CreatedDate = currentDate.Value,
                    ModifiedDate = currentDate.Value,
                    LunarActiveDate = currentLunarDate,
                    LunarCreatedDate = currentLunarDate,
                    LunarModifiedDate = currentLunarDate,
                    IsPaid = false,
                };

                await context.AddAsync(bill);
                await context.SaveChangesAsync();

                if (bill.Id > 0)
                {
                    History history = new()
                    {
                        ObjectId = bill.Id,
                        Action = (int)HistoryAction.Create,
                        Content = Lres["BillIsCreated"],
                        CreatedDate = currentDate.Value,
                        LunarCreatedDate = currentLunarDate,
                        Type = (int)HistoryType.Bill,
                    };
                    await context.AddAsync(history);
                    await context.SaveChangesAsync();
                }

                return bill.Id > 0;
            }
            return false;
        }

        public bool DeleteBill(int id)
        {
            var currentDate = DateTime.Now;
            var currentLunarDate = HelperFunctions.GetLunarDate(IsLeapYear, currentDate);
            var bill = context.Bills.FirstOrDefault(x => x.Id == id);
            if (bill != null)
            {
                bill.IsDeleted = true;
                History history = new()
                {
                    ObjectId = id,
                    Action = (int)HistoryAction.Remove,
                    Content = $"{Lres["BillIsRemoved"]}",
                    CreatedDate = currentDate,
                    LunarCreatedDate = currentLunarDate,
                    Type = (int)HistoryType.Bill,
                };

                RecycleBin recycle = new()
                {
                    ObjectId = id,
                    Type = (int)RecycleBinObjectType.Bill,
                    CreatedDate = currentDate,
                    LunarCreatedDate = currentLunarDate,
                };
                context.Add(history);
                context.Add(recycle);
                context.SaveChanges();
                return true;
            }

            return false;
        }

        public Pagination<BillDTO> GetAll(string keyword, DateTime? startDate, DateTime? endDate, int pageIndex, int pageSize, PriceType priceType, bool? isPaid)
        {
            var bills = (from b in context.Bills
                         join p in context.People on b.PersonId equals p.Id
                         join mbp in context.MeatBillPrices on b.Id equals mbp.BillId into meatBillPrices
                         from mbp in meatBillPrices.DefaultIfEmpty()
                         join m in context.Meats on mbp.MeatId equals m.Id into meats
                         from m in meats.DefaultIfEmpty()
                         where (string.IsNullOrEmpty(keyword) || p.Name.Contains(keyword))
                             && (startDate != null && endDate == null ?
                                    (b.ActiveDate.Value.Date.CompareTo(startDate.Value.Date) >= 0 && b.ActiveDate.Value.Date.CompareTo(DateTime.Now.Date) <= 0)
                                    : startDate == null && endDate != null ?
                                        (b.ActiveDate.Value.Date.CompareTo(endDate.Value.Date) <= 0 && b.ActiveDate.Value.Date.CompareTo(endDate.Value.Date.AddDays(-5)) >= 0)
                                            : startDate == null || endDate == null || (b.ActiveDate.Value.Date.CompareTo(startDate.Value.Date) >= 0 && b.ActiveDate.Value.Date.CompareTo(endDate.Value.Date) <= 0))
                            && (isPaid == null || b.IsPaid == isPaid)
                            && b.Type == (int)priceType && !b.IsDeleted
                         select new { b.Id, b.PersonId, b.Type, p.Name, b.ActiveDate, b.CreatedDate, b.ModifiedDate, b.LunarCreatedDate, b.LunarActiveDate, b.LunarModifiedDate, b.IsPaid, mbp.MeatId, meatBillId = mbp.Id, mbp.Price, mbp.PriceType, mbp.Weight, meatName = m.Name, meatType = m.Type })
                        .GroupBy(x => new { x.Id, x.PersonId, x.Type, x.Name, x.ActiveDate, x.CreatedDate, x.ModifiedDate, x.LunarActiveDate, x.LunarCreatedDate, x.LunarModifiedDate, x.IsPaid })
                        .Select(x => new BillDTO
                        {
                            Id = x.Key.Id,
                            PersonId = x.Key.PersonId,
                            Type = x.Key.Type,
                            ActiveDate = x.Key.ActiveDate,
                            CreatedDate = x.Key.CreatedDate,
                            ModifiedDate = x.Key.ModifiedDate,
                            LunarActiveDate = x.Key.LunarActiveDate,
                            LunarCreatedDate = x.Key.LunarCreatedDate,
                            LunarModifiedDate = x.Key.LunarModifiedDate,
                            IsPaid = x.Key.IsPaid,
                            PersonName = x.Key.Name,
                            Items = x.Select(y => new MeatBillPriceDTO
                            {
                                Id = y.meatBillId,
                                BillId = y.Id,
                                Price = y.Price,
                                MeatName = y.meatName,
                                MeatType = y.meatType,
                                PriceType = y.PriceType,
                                Weight = y.Weight,
                                MeatId = y.MeatId,
                                ActiveDate = y.ActiveDate,
                                CreatedDate = y.CreatedDate,
                                ModifiedDate = y.ModifiedDate,
                                LunarActiveDate = y.LunarActiveDate,
                                LunarCreatedDate = y.LunarCreatedDate,
                                LunarModifiedDate = y.LunarModifiedDate,
                            }).Where(y => y.Id != null),
                        });

            return bills.OrderByDescending(x => x.Id).Paginate(pageIndex, pageSize);
        }

        public bool PayingBill(int id, decimal totalPrice)
        {
            var currentDate = DateTime.Now;
            var currentLunarDate = HelperFunctions.GetLunarDate(IsLeapYear, currentDate);
            var bill = context.Bills.FirstOrDefault(x => x.Id == id);
            if (bill != null)
            {
                bill.IsPaid = true;
                History history = new()
                {
                    ObjectId = id,
                    Action = (int)HistoryAction.Pay,
                    Content = $"{Lres["Paid"]} {Lres["billwithprice"]}: {totalPrice:n0}.000 VND",
                    CreatedDate = currentDate,
                    LunarCreatedDate = currentLunarDate,
                    Type = (int)HistoryType.Bill,
                };
                context.Add(history);
                context.SaveChanges();
                return true;
            }

            return false;
        }

        public bool UpdateBillItems(BillDTO res)
        {
            var currentDate = DateTime.Now;
            var currentLunarDate = HelperFunctions.GetLunarDate(IsLeapYear, currentDate);

            foreach (var item in res.Items)
            {
                var itemDb = context.MeatBillPrices.FirstOrDefault(x => x.BillId == res.Id && x.Id == item.Id);
                var meat = context.Meats.FirstOrDefault(x => x.Id == item.MeatId);
                History history = new()
                {
                    Action = (int)HistoryAction.EditItemPrice,
                    Content = $"{Lres["Update"]} {itemDb.Weight} kg {meat.Name} {HelperFunctions.RenderMeatType(Lres, meat.Type)} {Lres["withPrice"]} {itemDb.Price.Value.ToString("n0")} {Lres["changeTo"]} {item.Weight} kg {meat.Name} {HelperFunctions.RenderMeatType(Lres, meat.Type)} {Lres["withPrice"]} {item.Price}",
                    CreatedDate = currentDate,
                    LunarCreatedDate = currentLunarDate,
                    ObjectId = itemDb.BillId.Value,
                    Type = (int)HistoryType.Bill,
                };

                if (itemDb != null && item.Price != null && item.Price.Value > 0 && item.Weight != null && item.Weight > 0)
                {
                    if(item.Price != itemDb.Price || item.Weight != itemDb.Weight)
                    {
                        if(item.Price != itemDb.Price)
                        {
                            itemDb.Price = item.Price;
                        }
                        if(item.Weight.Value != itemDb.Weight)
                        {
                            itemDb.Weight = item.Weight.Value;
                        }
                        context.Add(history);
                    }
                }
                else
                {
                    return false;
                }
            }
            context.SaveChanges();
            return true;
        }

        public async Task<bool> AddMeatToBill(int id, decimal weight, int billId, PriceType priceType)
        {
            var currentDate = DateTime.Now;
            var currentLunarDate = HelperFunctions.GetLunarDate(IsLeapYear, currentDate);
            var bill = context.Bills.FirstOrDefault(x => x.Id == billId);
            var meat = context.Meats.FirstOrDefault(x => x.Id == id);
            var meatPrice = context.MeatPrices.FirstOrDefault(x => x.ActiveDate.Value.Date.CompareTo(currentDate.Date) == 0 && x.MeatId == id && x.PriceType == (int)priceType);
            if (bill != null && meat != null && meatPrice != null)
            {
                MeatBillPrice meatBill = new()
                {
                    BillId = billId,
                    MeatId = meat.Id,
                    ActiveDate = bill.ActiveDate,
                    Weight = weight,
                    CreatedDate = currentDate,
                    ModifiedDate = currentDate,
                    LunarActiveDate = HelperFunctions.GetLunarDate(IsLeapYear, bill.ActiveDate.Value),
                    LunarCreatedDate = currentLunarDate,
                    LunarModifiedDate = currentLunarDate,
                    PriceType = (int)priceType,
                    Price = meatPrice.Price
                };

                await context.AddAsync(meatBill);
                await context.SaveChangesAsync();

                if (meatBill.Id > 0)
                {
                    History history = new()
                    {
                        ObjectId = bill.Id,
                        Action = (int)HistoryAction.AddItem,
                        Content = $"{Lres["Add"]} {weight} kg {meat.Name} {HelperFunctions.RenderMeatType(Lres, meat.Type)} {Lres["withPrice"]} {meatPrice.Price.Value.ToString("n0")}",
                        CreatedDate = currentDate,
                        LunarCreatedDate = currentLunarDate,
                        Type = (int)HistoryType.Bill,
                    };
                    await context.AddAsync(history);
                    await context.SaveChangesAsync();
                }

                return meatBill.Id > 0;
            }
            return false;
        }

        public bool RemoveMeatFromBill(int meatpriceId)
        {
            var currentDate = DateTime.Now;
            var currentLunarDate = HelperFunctions.GetLunarDate(IsLeapYear, currentDate);

            var meatprice = context.MeatBillPrices.FirstOrDefault(x => x.Id == meatpriceId);
            if (meatprice != null)
            {
                var meat = context.Meats.FirstOrDefault(x => x.Id == meatprice.MeatId);
                History history = new()
                {
                    ObjectId = meatprice.BillId.Value,
                    Action = (int)HistoryAction.RemoveItem,
                    Content = $"{Lres["Remove"]} {meatprice.Weight} kg {meat.Name} {HelperFunctions.RenderMeatType(Lres, meat.Type)} {Lres["withPrice"]} {meatprice.Price.Value.ToString("n0")}",
                    CreatedDate = currentDate,
                    LunarCreatedDate = currentLunarDate,
                    Type = (int)HistoryType.Bill,
                };

                context.Remove(meatprice);
                context.Add(history);
                context.SaveChanges();

                return true;
            }

            return false;
        }

        public BillDTO GetBillDetail(int id)
        {
            return (from b in context.Bills
                    join p in context.People on b.PersonId equals p.Id
                    join mbp in context.MeatBillPrices on b.Id equals mbp.BillId into meatBillPrices
                    from mbp in meatBillPrices.DefaultIfEmpty()
                    join m in context.Meats on mbp.MeatId equals m.Id into meats
                    from m in meats.DefaultIfEmpty()
                    where b.Id == id
                    select new { b.Id, b.PersonId, b.Type, p.Name, b.ActiveDate, b.CreatedDate, b.ModifiedDate, b.LunarActiveDate, b.LunarCreatedDate, b.LunarModifiedDate, b.IsPaid, mbp.MeatId, meatBillId = mbp.Id, mbp.Price, mbp.PriceType, mbp.Weight, meatName = m.Name, meatType = m.Type })
            .GroupBy(x => new { x.Id, x.PersonId, x.Type, x.Name, x.ActiveDate, x.CreatedDate, x.ModifiedDate, x.LunarActiveDate, x.LunarCreatedDate, x.LunarModifiedDate, x.IsPaid })
            .Select(x => new BillDTO
            {
                Id = x.Key.Id,
                PersonId = x.Key.PersonId,
                Type = x.Key.Type,
                ActiveDate = x.Key.ActiveDate,
                CreatedDate = x.Key.CreatedDate,
                ModifiedDate = x.Key.ModifiedDate,
                LunarActiveDate = x.Key.LunarActiveDate,
                LunarCreatedDate = x.Key.LunarCreatedDate,
                LunarModifiedDate = x.Key.LunarModifiedDate,
                IsPaid = x.Key.IsPaid,
                PersonName = x.Key.Name,
                Items = x.Select(y => new MeatBillPriceDTO
                {
                    Id = y.meatBillId,
                    BillId = y.Id,
                    Price = y.Price,
                    MeatName = y.meatName,
                    MeatType = y.meatType,
                    PriceType = y.PriceType,
                    Weight = y.Weight,
                    MeatId = y.MeatId,
                    ActiveDate = y.ActiveDate,
                    CreatedDate = y.CreatedDate,
                    ModifiedDate = y.ModifiedDate,
                    LunarActiveDate = y.LunarActiveDate,
                    LunarCreatedDate = y.LunarCreatedDate,
                    LunarModifiedDate = y.LunarModifiedDate
                }).Where(y => y.Id != null),
            }).FirstOrDefault();
        }

        public Pagination<MeatBillPriceDTO> GetAllMeatOfBill(int billId)
        {
            return (from mbp in context.MeatBillPrices
                    join m in context.Meats on mbp.MeatId equals m.Id
                    where mbp.BillId == billId
                    select new MeatBillPriceDTO
                    {
                        BillId = mbp.BillId,
                        ActiveDate = mbp.ActiveDate,
                        CreatedDate = mbp.CreatedDate,
                        Id = mbp.Id,
                        MeatId = mbp.MeatId,
                        ModifiedDate = mbp.ModifiedDate,
                        LunarModifiedDate = mbp.LunarModifiedDate,
                        LunarActiveDate = mbp.LunarActiveDate,
                        LunarCreatedDate = mbp.LunarCreatedDate,
                        Price = mbp.Price,
                        PriceType = mbp.PriceType,
                        Weight = mbp.Weight,
                        MeatName = m.Name,
                        MeatType = m.Type,
                    }).Paginate(1, 10000);
        }
    }
}
