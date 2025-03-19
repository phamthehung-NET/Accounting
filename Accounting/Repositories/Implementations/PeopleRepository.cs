using Accounting.Data;
using Accounting.Model;
using Accounting.Model.DTO;
using Accounting.Repositories.Interfaces;
using Accounting.Utilities;

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
            var currentDate = DateTime.Now;
            var currentLunarDate = HelperFunctions.ConvertSolarToLunar(currentDate);
            var person = context.People.FirstOrDefault(x => x.Id == id);
            if (person != null)
            {
                person.IsDeleted = true;
                RecycleBin recycle = new()
                {
                    ObjectId = id,
                    Type = (int)RecycleBinObjectType.Person,
                    CreatedDate = currentDate,
                    LunarCreatedDate = currentLunarDate,
                };
                context.Add(recycle);
                context.SaveChanges();
                return recycle.Id > 0;
            }
            return false;
        }

        public Pagination<PersonDTO> GetAllPeople(string keyword, string order, bool? isSource, int pageIndex, int pageSize)
        {
            //var people = (from p in context.People
            //              join b in context.Bills on p.Id equals b.PersonId into bills
            //              from bill in bills.DefaultIfEmpty()
            //              join mbp in context.MeatBillPrices on bill.Id equals mbp.BillId into meatbills
            //              from mbps in meatbills.DefaultIfEmpty()
            //              select new
            //              {
            //                  p.Id,
            //                  p.Address,
            //                  p.IsDeleted,
            //                  p.Name,
            //                  p.PhoneNumber,
            //                  p.Source,
            //                  NearestTransaction = bill.ActiveDate,
            //                  NearestTransactionId = bill.Id,
            //                  LunarNearestTransaction = bill.LunarActiveDate,
            //                  billId = bill.Id,
            //                  bill.IsPaid,
            //                  bill.PaidAmount,
            //                  deletedBill = bill.IsDeleted,
            //                  mbps.Weight,
            //                  mbps.Price,
            //                  mbps.PriceType
            //              }).GroupBy(x => new { x.Id, x.Name, x.Source, x.Address, x.IsDeleted, x.PhoneNumber })
            //             .Select(x => new PersonDTO
            //             {
            //                 Id = x.Key.Id,
            //                 Address = x.Key.Address,
            //                 IsDeleted = x.Key.IsDeleted,
            //                 Name = x.Key.Name,
            //                 PhoneNumber = x.Key.PhoneNumber,
            //                 Source = x.Key.Source,
            //                 NearestTransaction = x.Select(y => new NearestTransaction
            //                 {
            //                     Id = y.NearestTransactionId,
            //                     ActivateDate = y.NearestTransaction,
            //                     LunarActiveDate = y.LunarNearestTransaction
            //                 }).Where(x => x.ActivateDate != null).OrderByDescending(x => x.ActivateDate).FirstOrDefault(),
            //                 Bills = x.Where(y => y.billId > 0 && !y.IsPaid && !y.IsDeleted.Value && y.PriceType == (int)PriceType.Sale).Select(y => new BillDTO
            //                 {
            //                     Id = y.billId,
            //                     IsDeleted = y.IsDeleted.Value,
            //                     Items = x.Where(z => z.billId > 0 && !z.IsPaid && !z.IsDeleted.Value && z.PriceType == (int)PriceType.Sale).Select(z => new MeatBillPriceDTO
            //                     {
            //                         Price = z.Price,
            //                         Weight = z.Weight,
            //                         BillId = z.billId
            //                     }),
            //                     PaidAmount = y.PaidAmount
            //                 }),
            //             });

            var people = context.People
                .Select(person => new PersonDTO
                {
                    Id = person.Id,
                    Name = person.Name,
                    Address = person.Address,
                    IsDeleted = person.IsDeleted,
                    PhoneNumber = person.PhoneNumber,
                    Source = person.Source,
                    NearestTransaction = context.Bills.Where(x => x.PersonId == person.Id && !x.IsDeleted && x.ActiveDate != null).OrderByDescending(x => x.ActiveDate).Select(y => new NearestTransaction
                    {
                        Id = y.Id,
                        ActivateDate = y.ActiveDate,
                        LunarActiveDate = y.LunarActiveDate,
                    }).FirstOrDefault(),
                    Bills = context.Bills
                        .Where(bill => bill.PersonId == person.Id && bill.Id > 0 && !bill.IsPaid && !bill.IsDeleted)
                        .Select(bill => new BillDTO
                        {
                            Id = bill.Id,
                            ActiveDate = bill.ActiveDate,
                            LunarActiveDate = bill.LunarActiveDate,
                            PaidAmount = bill.PaidAmount,
                            Items = context.MeatBillPrices
                                .Where(meat => meat.BillId == bill.Id && meat.PriceType == (int)PriceType.Sale)
                                .Select(mbp => new MeatBillPriceDTO
                                {
                                    Id = mbp.Id,
                                    Weight = mbp.Weight,
                                    Price = mbp.Price
                                }).AsEnumerable()
                        }).AsEnumerable()
                });

            if (!string.IsNullOrEmpty(keyword))
            {
                people = people.Where(x => x.Name.ToLower().Contains(keyword.ToLower())
                   || x.PhoneNumber.ToLower().Contains(keyword.ToLower())
                   || x.Address.ToLower().Contains(keyword.ToLower()));
            }
            if (isSource != null)
            {
                people = people.Where(x => x.Source == isSource);
            }

            people = people.Where(x => x.IsDeleted == false);

            return people.OrderBy(order).Paginate(pageIndex, pageSize);
        }

        public Person GetPersonDetail(int id)
        {
            return context.People.FirstOrDefault(x => x.Id == id);
        }

        public bool UpdatePerson(Person res)
        {
            var person = context.People.FirstOrDefault(x => x.Id == res.Id);
            if (person != null)
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
