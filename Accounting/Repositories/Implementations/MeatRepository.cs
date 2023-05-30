using Accounting.Utilities;
using Accounting.Data;
using Accounting.Model;
using Accounting.Model.DTO;
using Accounting.Repositories.Interfaces;

namespace Accounting.Repositories.Implementations
{
    public class MeatRepository : IMeatRepository
    {
        private readonly AccountingDbContext context;

        public MeatRepository(AccountingDbContext _context)
        {
            context = _context;
        }

        public bool AddMeat(MeatDTO req)
        {
            if (!IsMeatExits(req))
            {
                Meat meat = new()
                {
                    Name = req.Name,
                    Frozen = req.Frozen,
                    Type = req.Type,
                    IsDeleted = false
                };
                context.Meats.Add(meat);
                context.SaveChanges();

                return meat.Id > 0;
            }
            return false;
        }

        public Pagination<MeatDTO> GetAllMeats(string keyword, int? type, bool? frozen, int pageIndex, int pageSize, string order)
        {
            var meats = context.Meats.Select(x => new MeatDTO
            {
                Id = x.Id,
                Name = x.Name,
                Type = x.Type,
                Frozen = x.Frozen,
                IsDeleted = x.IsDeleted,
                YesterdayEntryPrice = context.MeatPrices.FirstOrDefault(y => y.MeatId == x.Id && y.ActiveDate.Value.Date.CompareTo(DateTime.Now.AddDays(-1).Date) == 0 && y.PriceType == (int)PriceType.Entry).Price,
                TodayEntryPrice = context.MeatPrices.FirstOrDefault(y => y.MeatId == x.Id && y.ActiveDate.Value.Date.CompareTo(DateTime.Now.Date) == 0 && y.PriceType == (int)PriceType.Entry).Price,
                YesterdaySalePrice = context.MeatPrices.FirstOrDefault(y => y.MeatId == x.Id && y.ActiveDate.Value.Date.CompareTo(DateTime.Now.AddDays(-1).Date) == 0 && y.PriceType == (int)PriceType.Sale).Price,
                TodaySalePrice = context.MeatPrices.FirstOrDefault(y => y.MeatId == x.Id && y.ActiveDate.Value.Date.CompareTo(DateTime.Now.Date) == 0 && y.PriceType == (int)PriceType.Sale).Price,
            });

            if (!string.IsNullOrEmpty(keyword))
            {
                meats = meats.Where(x => x.Name.ToLower().Contains(keyword.ToLower()));
            }
            if (type != null)
            {
                meats = meats.Where(x => x.Type == type);
            }
            if (frozen != null)
            {
                meats = meats.Where(x => x.Frozen == frozen);
            }

            meats = meats.Where(x => x.IsDeleted == false);

            return meats.OrderBy(order).Paginate(pageIndex, pageSize);
        }

        public MeatDTO GetMeatDetail(int id)
        {
            var meat = context.Meats.Select(x => new MeatDTO
            {
                Id = x.Id,
                IsDeleted = x.IsDeleted,
                Name = x.Name,
                Frozen = x.Frozen,
                Type = x.Type,
            }).FirstOrDefault(x => x.Id == id);
            if (meat != null)
            {
                return meat;
            }
            return null;
        }

        public bool EditMeat(MeatDTO req)
        {
            var meatDb = context.Meats.FirstOrDefault(x => x.Id == req.Id);
            if (meatDb != null && !IsMeatExits(req))
            {
                meatDb.Name = req.Name;
                meatDb.Type = req.Type;
                meatDb.Frozen = req.Frozen;
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool DeleteMeat(int id)
        {
            var meatDb = context.Meats.FirstOrDefault(x => x.Id == id);
            if (meatDb != null)
            {
                meatDb.IsDeleted = true;
                RecycleBin recycle = new()
                {
                    ObjectId = id,
                    Type = (int)RecycleBinObjectType.Meat,
                    CreatedDate = DateTime.Now,
                };
                context.RecycleBins.Add(recycle);
                context.SaveChanges();
                return recycle.Id > 0;
            }
            return false;
        }

        private bool IsMeatExits(MeatDTO meat)
        {
            var db = context.Meats.FirstOrDefault(x => x.Name.Equals(meat.Name) && x.Type == meat.Type && x.Frozen == meat.Frozen && x.IsDeleted == false);
            if (db != null)
            {
                if (meat.Id > 0 && db.Id == meat.Id)
                {
                    return false;
                }
                return true;
            }
            return false;
        }
    }
}
