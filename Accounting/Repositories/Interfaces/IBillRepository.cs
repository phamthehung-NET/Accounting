using Accounting.Common;
using Accounting.Model.DTO;

namespace Accounting.Repositories.Interfaces
{
    public interface IBillRepository
    {
        Pagination<BillDTO> GetAll(string keyword, DateTime? startDate, DateTime? endDate, int pageIndex, int pageSize);

        bool AddBill(BillDTO res);

        bool UpdateBill(BillDTO res);

        bool DeleteBill(int id);

        bool PayingBill(int id);

        bool AddMeatToBill(int id, decimal weight, int billId, PriceType priceType);
    }
}
