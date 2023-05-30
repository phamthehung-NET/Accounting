using Accounting.Model;
using Accounting.Utilities;

namespace Accounting.Repositories.Interfaces
{
    public interface ILogRepository
    {
        void Add(Log log);

        Pagination<Log> GetAll(string keyword, string order, DateTime? startDate, DateTime? endDate, int pageIndex, int pageSize);
    }
}
