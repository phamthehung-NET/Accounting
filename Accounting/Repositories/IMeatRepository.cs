using Accounting.Common;
using Accounting.Model.DTO;

namespace Accounting.Repositories
{
    public interface IMeatRepository
    {
        public Pagination<MeatDTO> GetAllMeats(string keyword, int? type, bool? prozen, int pageIndex, int pageSize);

        public bool AddMeat(MeatDTO meat);

        public bool EditMeat(MeatDTO req);

        public bool DeleteMeat(int id);

        public MeatDTO GetMeatDetail(int id);
    }
}
