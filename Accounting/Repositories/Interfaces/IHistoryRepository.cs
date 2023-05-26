using Accounting.Model;
using Accounting.Utilities;

namespace Accounting.Repositories.Interfaces
{
    public interface IHistoryRepository
    {
        public Pagination<History> GetAllHistoryByObject(int objectId, HistoryType type);
    }
}
