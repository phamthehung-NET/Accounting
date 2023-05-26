using Accounting.Data;
using Accounting.Model;
using Accounting.Repositories.Interfaces;
using Accounting.Utilities;

namespace Accounting.Repositories.Implementations
{
    public class HistoryRepository : IHistoryRepository
    {
        private readonly AccountingDbContext context;

        public HistoryRepository(AccountingDbContext _context)
        {
            context = _context;
        }

        public Pagination<History> GetAllHistoryByObject(int objectId, HistoryType type)
        {
            return context.Histories.Where(x => x.ObjectId == objectId && x.Type == (int)type).OrderBy(x => x.Id).Paginate(1, 100000);
        }
    }
}
