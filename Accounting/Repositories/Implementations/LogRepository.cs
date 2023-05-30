using Accounting.Data;
using Accounting.Model;
using Accounting.Repositories.Interfaces;
using Accounting.Utilities;

namespace Accounting.Repositories.Implementations
{
    public class LogRepository : ILogRepository
    {
        private readonly AccountingDbContext context;

        public LogRepository(AccountingDbContext _context)
        {
            context = _context;
        }

        public void Add(Log log)
        {
            context.Add(log);
            context.SaveChanges();
        }

        public Pagination<Log> GetAll(string keyword, string order, DateTime? startDate, DateTime? endDate, int pageIndex, int pageSize)
        {
            return context.Logs
                .Where(x => (x.Id.ToString().Contains(keyword.ToLower())
                    || x.Location.ToLower().Contains(keyword.ToLower())
                    || x.StackTrace.ToLower().Contains(keyword.ToLower())
                    || x.Message.ToLower().Contains(keyword.ToLower()))
                    && (startDate != null && endDate == null ?
                                    (x.CreatedDate.Date.CompareTo(startDate.Value.Date) >= 0 && x.CreatedDate.Date.CompareTo(DateTime.Now.Date) <= 0)
                                    : startDate == null && endDate != null ?
                                        (x.CreatedDate.Date.CompareTo(endDate.Value.Date) <= 0 && x.CreatedDate.Date.CompareTo(endDate.Value.Date.AddDays(-5)) >= 0)
                                            : startDate == null || endDate == null || (x.CreatedDate.Date.CompareTo(startDate.Value.Date) >= 0 && x.CreatedDate.Date.CompareTo(endDate.Value.Date) <= 0)))
                .OrderBy(order).Paginate(pageIndex, pageSize);
        }
    }
}
