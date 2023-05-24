using Accounting.Common;
using Accounting.Data;
using Accounting.Model;
using Accounting.Model.DTO;
using Accounting.Repositories.Interfaces;

namespace Accounting.Repositories.Implementations
{
    public class BillRepository : IBillRepository
    {
        private readonly AccountingDbContext context;

        public BillRepository(AccountingDbContext _context)
        {
            context = _context;
        }

        public bool AddBill(BillDTO res)
        {
            var currentDate = DateTime.Now;
            var billDb = context.Bills.FirstOrDefault(x => x.PersonId == res.PersonId && x.ActiveDate.Value.Date == currentDate.Date && x.Type == res.Type);
            if(billDb == null)
            {
                Bill bill = new()
                {
                    PersonId = res.PersonId,
                    Type = res.Type,
                    ActiveDate = currentDate,
                    CreatedDate = currentDate,
                    ModifiedDate = currentDate,
                    IsPaid = false,
                };

                context.Add(bill);
                context.SaveChanges();
                return bill.Id > 0;
            }
            return false;
        }

        public bool DeleteBill(int id)
        {
            throw new NotImplementedException();
        }

        public Pagination<BillDTO> GetAll(string keyword, DateTime? startDate, DateTime? endDate, int pageIndex, int pageSize)
        {
            var bills = (from b in context.Bills
                         join p in context.People on b.PersonId equals p.Id
                         join mbp in context.MeatBillPrices on b.Id equals mbp.BillId into meatBillPrices
                         from mbp in meatBillPrices.DefaultIfEmpty()
                         join m in context.Meats on mbp.MeatId equals m.Id into meats
                         from m in meats.DefaultIfEmpty()
                         where !string.IsNullOrEmpty(keyword) ? p.Name.Contains(keyword) : true
                             && (startDate != null && endDate == null ?
                                    (b.ActiveDate.Value.Date > startDate.Value.Date && b.ActiveDate.Value.Date <= DateTime.Now.Date)
                                    : startDate == null && endDate != null ?
                                        (b.ActiveDate.Value.Date <= endDate.Value.Date && b.ActiveDate.Value.Date > endDate.Value.Date.AddDays(-5))
                                            : startDate != null && endDate != null ? (b.ActiveDate.Value.Date > startDate.Value.Date && b.ActiveDate.Value.Date <= endDate.Value.Date) : true)
                         select new { b.Id, b.PersonId, b.Type, p.Name, b.ActiveDate, b.CreatedDate, b.ModifiedDate, b.IsPaid, mbp.MeatId, meatBillId = mbp.Id, mbp.Price, mbp.PriceType, mbp.Weight, meatName = m.Name, meatType = m.Type })
                        .GroupBy(x => new { x.Id, x.PersonId, x.Type, x.Name, x.ActiveDate, x.CreatedDate, x.ModifiedDate, x.IsPaid })
                        .Select(x => new BillDTO
                        {
                            Id = x.Key.Id,
                            PersonId = x.Key.PersonId,
                            Type = x.Key.Type,
                            ActiveDate = x.Key.ActiveDate,
                            CreatedDate = x.Key.CreatedDate,
                            ModifiedDate = x.Key.ModifiedDate,
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
                            }).Where(y => y.Id != null),
                        });

            return bills.Paginate(pageIndex, pageSize);
        }

        public bool PayingBill(int id)
        {
            throw new NotImplementedException();
        }

        public bool UpdateBill(BillDTO bill)
        {
            throw new NotImplementedException();
        }

        public bool AddMeatToBill(int id, decimal weight, int billId, PriceType priceType)
        {
            var currentDate = DateTime.Now;
            var bill = context.Bills.FirstOrDefault(x => x.Id == billId);
            var meat = context.Meats.FirstOrDefault(x => x.Id == id);
            var meatPrice = context.MeatPrices.FirstOrDefault(x => x.ActiveDate.Value.Date.CompareTo(currentDate.Date) == 0 && x.Id == id && x.PriceType == (int)priceType);
            if(bill != null && meat != null && meatPrice != null)
            {
                MeatBillPrice meatBill = new()
                {
                    BillId = billId,
                    MeatId = meat.Id,
                    ActiveDate = bill.ActiveDate,
                    Weight = weight,
                    CreatedDate = currentDate,
                    ModifiedDate = currentDate,
                    PriceType = (int)priceType,
                    Price = meatPrice.Price
                };

                context.Add(meatBill);
                context.SaveChanges();
                return meatBill.Id > 0;
            }
            return false;
        }
    }
}
