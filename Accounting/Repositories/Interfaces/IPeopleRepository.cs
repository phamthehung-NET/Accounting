using Accounting.Common;
using Accounting.Model;
using Accounting.Model.DTO;

namespace Accounting.Repositories.Interfaces
{
    public interface IPeopleRepository
    {
        Pagination<PersonDTO> GetAllPeople(string keyword, string order, bool? isSource, int pageIndex, int pageSize);

        bool AddPerson(Person res);

        bool UpdatePerson(Person res);

        bool DeletePerson(int id);

        Person GetPersonDetail(int id);
    }
}
