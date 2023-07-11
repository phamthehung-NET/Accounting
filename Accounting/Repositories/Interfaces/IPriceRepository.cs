using Accounting.Model.DTO;

namespace Accounting.Repositories.Interfaces
{
    public interface IPriceRepository
    {
        Task<bool> UpdateItemPrice(Dictionary<int, int?> inputEntryPrice, Dictionary<int, int?> inputSalePrice, DateTime? date);
    }
}
