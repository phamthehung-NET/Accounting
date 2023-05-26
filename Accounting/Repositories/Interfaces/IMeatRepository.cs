using Accounting.Utilities;
using Accounting.Model.DTO;

namespace Accounting.Repositories.Interfaces
{
    public interface IMeatRepository
    {
        public Pagination<MeatDTO> GetAllMeats(string keyword, int? type, bool? frozen, int pageIndex, int pageSize, string order);

        public bool AddMeat(MeatDTO meat);

        public bool EditMeat(MeatDTO req);

        public bool DeleteMeat(int id);

        public MeatDTO GetMeatDetail(int id);
    }
}
