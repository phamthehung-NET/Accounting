using Accounting.Model.DTO;
using Accounting.Utilities;

namespace Accounting.Repositories.Interfaces
{
    public interface IMeatRepository
    {
        Pagination<MeatDTO> GetAllMeats(string keyword, int? type, bool? frozen, int pageIndex, int pageSize, string order);

        bool AddMeat(MeatDTO meat);

        bool EditMeat(MeatDTO req);

        bool DeleteMeat(int id);

        MeatDTO GetMeatDetail(int id);
    }
}
