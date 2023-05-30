using Accounting.Model.DTO;
using Accounting.Utilities;

namespace Accounting.Repositories.Interfaces
{
    public interface IRecycleBinRepository
    {
        Pagination<RecycleBinDTO> GetAll(string keyword, RecycleBinObjectType? type, string order, int pageIndex, int pageSize);

        bool Restore(int objectId, RecycleBinObjectType objectType);

        object GetObjectDetail(int objectId, RecycleBinObjectType objectType);
    }
}
