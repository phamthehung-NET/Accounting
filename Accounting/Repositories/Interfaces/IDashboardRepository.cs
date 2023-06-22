using Accounting.Model.DTO;

namespace Accounting.Repositories.Interfaces
{
    public interface IDashboardRepository
    {
        DashboardDTO GetWastedWeight(DateTime? date);

        List<DashboardDTO> GetChartData();
    }
}
