using Accounting.Model.DTO;

namespace Accounting.Repositories
{
    public interface IPriceRepository
    {
        public bool UpdateItemPrice(Dictionary<int, int> inputEntryPrice, Dictionary<int, int> inputSalePrice);
    }
}
