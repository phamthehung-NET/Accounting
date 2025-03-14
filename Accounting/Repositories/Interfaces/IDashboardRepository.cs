using Accounting.Model.DTO;
using Accounting.Utilities;
using BlazorDateRangePicker;

namespace Accounting.Repositories.Interfaces
{
    public interface IDashboardRepository
    {
        DashboardDTO GetWastedWeight(DateTime? date);

        List<DashboardDTO> GetChartData();

        decimal GetRestMeatInADay(DateTime dateTime);

        List<(DateTime, decimal)> GetRestMeatInDays(DateRange range);

        List<DebtDashboardDTO> GetDebtData(DasboardDebtFilterType type, int numberItemTake = 0);
    }
}
