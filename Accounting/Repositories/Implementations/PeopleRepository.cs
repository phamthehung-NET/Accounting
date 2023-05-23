using Accounting.Common;
using Accounting.Data;
using Accounting.Model;
using Accounting.Model.DTO;
using Accounting.Repositories.Interfaces;

namespace Accounting.Repositories.Implementations
{
    public class PeopleRepository : IPeopleRepository
    {
        private readonly AccountingDbContext context;

        public PeopleRepository(AccountingDbContext _context)
        {
            context = _context;
        }

        public bool AddPerson(Person res)
        {
            res.IsDeleted = false;
            context.Add(res);
            context.SaveChanges();
            return res.Id > 0;
        }

        public bool DeletePerson(int id)
        {
            var person = context.People.FirstOrDefault(x => x.Id == id);
            if(person != null)
            {
                person.IsDeleted = true;
                RecycleBin recycle = new()
                {
                    ObjectId = id,
                    Type = (int)Constants.RecycleBinObjectType.Person,
                    CreatedDate = DateTime.Now,
                };
                context.Add(recycle);
                context.SaveChanges();
                return recycle.Id > 0;
            }
            return false;
        }

        public Pagination<PersonDTO> GetAllPeople(string keyword, string order, bool? isSource, int pageIndex, int pageSize)
        {
            var people = context.People.Select(x => new PersonDTO
            {
                Id = x.Id,
                Name = x.Name,
                Address = x.Address,
                IsDeleted = x.IsDeleted,
                PhoneNumber = x.PhoneNumber,
                Source = x.Source,
            });

            if(!string.IsNullOrEmpty(keyword))
            {
                people.Where(x => x.Name.ToLower().Contains(keyword.ToLower())
                    || x.PhoneNumber.ToLower().Contains(keyword.ToLower())
                    || x.Address.ToLower().Contains(keyword.ToLower()));
            }
            if(isSource != null)
            {
                people.Where(x => x.Source == isSource);
            }

            people.Where(x => x.IsDeleted == false);

            return people.OrderBy(order).Paginate(pageIndex, pageSize);
        }

        public Person GetPersonDetail(int id)
        {
            return context.People.FirstOrDefault(x => x.Id == id);
        }

        public bool UpdatePerson(Person res)
        {
            var person = context.People.FirstOrDefault(x => x.Id == res.Id);
            if(person != null)
            {
                person.Address = res.Address;
                person.Name = res.Name;
                person.PhoneNumber = res.PhoneNumber;
                person.Source = res.Source;
                context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
