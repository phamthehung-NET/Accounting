using Accounting.Common;
using Accounting.Data;
using Accounting.Model;
using Accounting.Model.DTO;

namespace Accounting.Repositories
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
                    Prozen = req.Prozen,
                    Type = req.Type,
                    IsDeleted = false
                };
                context.Meats.Add(meat);
                context.SaveChanges();

                return meat.Id > 0;
            }
            return false;
        }

        public Pagination<MeatDTO> GetAllMeats(string keyword, int? type, bool? prozen, int pageIndex, int pageSize)
        {
            var meats = context.Meats.Select(x => new MeatDTO
            {
                Id = x.Id,
                Name = x.Name,
                Type = x.Type,
                Prozen = x.Prozen,
                IsDeleted = x.IsDeleted,
                YesterdayEntryPrice = context.MeatPrices.FirstOrDefault(y => y.MeatId == x.Id && y.ActiveDate.Value.Date.CompareTo(DateTime.Now.AddDays(-1).Date) == 0 && y.PriceType == Constants.PRICE_TYPE_ENTRY).Price,
                TodayEntryPrice = context.MeatPrices.FirstOrDefault(y => y.MeatId == x.Id && y.ActiveDate.Value.Date.CompareTo(DateTime.Now.Date) == 0 && y.PriceType == Constants.PRICE_TYPE_ENTRY).Price,
                YesterdaySalePrice = context.MeatPrices.FirstOrDefault(y => y.MeatId == x.Id && y.ActiveDate.Value.Date.CompareTo(DateTime.Now.AddDays(-1).Date) == 0 && y.PriceType == Constants.PRICE_TYPE_SALE).Price,
                TodaySalePrice = context.MeatPrices.FirstOrDefault(y => y.MeatId == x.Id && y.ActiveDate.Value.Date.CompareTo(DateTime.Now.Date) == 0 && y.PriceType == Constants.PRICE_TYPE_SALE).Price,
            });

            if (!string.IsNullOrEmpty(keyword))
            {
                meats = meats.Where(x => x.Name.ToLower().Contains(keyword.ToLower()));
            }
            if (type != null)
            {
                meats = meats.Where(x => x.Type == type);
            }
            if (prozen != null)
            {
                meats = meats.Where(x => x.Prozen == prozen);
            }

            meats = meats.Where(x => x.IsDeleted == false);

            var pagination = HelperFunctions.GetPaging(pageIndex, pageSize, meats.OrderByDescending(x => x.Id).ToList());
            return pagination;
        }

        public MeatDTO GetMeatDetail(int id)
        {
            var meat = context.Meats.Select(x => new MeatDTO
            {
                Id = x.Id,
                IsDeleted = x.IsDeleted,
                Name = x.Name,
                Prozen = x.Prozen,
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
                meatDb.Prozen = req.Prozen;
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
                    Type = Constants.RECYCLE_BIN_TYPE_MEAT,
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
            var db = context.Meats.FirstOrDefault(x => x.Name.Equals(meat.Name) && x.Type == meat.Type && x.Prozen == meat.Prozen);
            if (db != null)
            {
                if(meat.Id > 0 && db.Id == meat.Id)
                {
                    return false;
                }
                return true;
            }
            return false;
        }
    }
}
