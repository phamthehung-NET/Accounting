using Accounting.Model.DTO;
using Accounting.Utilities;

namespace Accounting.Repositories.Interfaces
{
    public interface IBillRepository
    {
        Pagination<BillDTO> GetAll(string keyword, DateTime? startDate, DateTime? endDate, int pageIndex, int pageSize, PriceType priceType, bool? isPaid);

        Task<bool> AddBill(BillDTO res, DateTime? currentDate);

        bool UpdateBillItems(BillDTO res);

        bool DeleteBill(int id);

        bool PayingBill(int id, decimal totalPrice, decimal totalAmount);

        Task<bool> AddMeatToBill(int id, decimal weight, int billId, PriceType priceType);

        bool RemoveMeatFromBill(int meatpriceId);

        BillDTO GetBillDetail(int id);

        Pagination<MeatBillPriceDTO> GetAllMeatOfBill(int billId);

        List<DebtDTO> GetDebtByPerson(int personId);

        bool AddRestMeat(int billId, decimal weight);
    }
}
