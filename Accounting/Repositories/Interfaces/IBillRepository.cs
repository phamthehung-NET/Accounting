using Accounting.Model.DTO;
using Accounting.Utilities;

namespace Accounting.Repositories.Interfaces
{
    public interface IBillRepository
    {
        Pagination<BillDTO> GetAll(string keyword, DateTime? startDate, DateTime? endDate, int pageIndex, int pageSize, PriceType priceType, bool? isPaid);

        bool AddBill(BillDTO res);

        bool UpdateBillItems(BillDTO res);

        bool DeleteBill(int id);

        bool PayingBill(int id, decimal totalPrice);

        bool AddMeatToBill(int id, decimal weight, int billId, PriceType priceType);

        bool RemoveMeatFromBill(int meatpriceId);

        BillDTO GetBillDetail(int id);

        Pagination<MeatBillPriceDTO> GetAllMeatOfBill(int billId);
    }
}
