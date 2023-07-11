using Accounting.Data;
using Accounting.Model;
using Accounting.Model.DTO;
using Accounting.Pages;
using Accounting.Repositories.Interfaces;
using Accounting.Utilities;
using Microsoft.Extensions.Localization;

namespace Accounting.Repositories.Implementations
{
    public class RecycleBinRepository : IRecycleBinRepository
    {
        private readonly AccountingDbContext context;
        private readonly IStringLocalizer<Resource> Lres;
        private readonly IMeatRepository meatRepository;
        private readonly IPeopleRepository peopleRepository;
        private readonly IBillRepository billRepository;
        private readonly bool IsLeapYear;

        public RecycleBinRepository(AccountingDbContext _context, IStringLocalizer<Resource> _Lres, IMeatRepository _meatRepository, IPeopleRepository _peopleRepository, IBillRepository _billRepository)
        {
            context = _context;
            Lres = _Lres;
            billRepository = _billRepository;
            meatRepository = _meatRepository;
            peopleRepository = _peopleRepository;
            IsLeapYear = bool.Parse(context.Settings.FirstOrDefault(x => x.Name.Equals(Constants.IsLeapYearSetting)).Value);
        }

        public Pagination<RecycleBinDTO> GetAll(string keyword, RecycleBinObjectType? type, string order, int pageIndex, int pageSize)
        {
            var recycleBin = (from r in context.RecycleBins
                              join m in context.Meats on r.ObjectId equals m.Id into meats
                              from m in meats.DefaultIfEmpty()
                              join b in context.Bills on r.ObjectId equals b.Id into bills
                              from b in bills.DefaultIfEmpty()
                              join pb in context.People on b.PersonId equals pb.Id into pbills
                              from pb in pbills.DefaultIfEmpty()
                              join p in context.People on r.ObjectId equals p.Id into people
                              from p in people.DefaultIfEmpty()
                              where r.Type == (int)RecycleBinObjectType.Meat || r.Type == (int)RecycleBinObjectType.Person || r.Type == (int)RecycleBinObjectType.Bill
                              select new RecycleBinDTO
                              {
                                  Id = r.Id,
                                  CreatedDate = r.CreatedDate,
                                  ModifiedDate = r.ModifiedDate,
                                  LunarCreatedDate = r.LunarCreatedDate,
                                  LunarModifiedDate = r.LunarModifiedDate,
                                  ObjectId = r.ObjectId.Value,
                                  Type = r.Type,
                                  ObjectName = r.Type == (int)RecycleBinObjectType.Meat ? 
                                    $"{m.Name} {HelperFunctions.RenderMeatType(Lres, m.Type)}" : r.Type == (int)RecycleBinObjectType.Bill ? 
                                    $"{((PriceType)b.Type == PriceType.Sale ? Lres["SaleBill"] : Lres["EntryBill"])} {Lres["of"]} {pb.Name} {Lres["at"]} {b.ActiveDate.Value.ToString("dd/MM/yyyy")}" : r.Type == (int)RecycleBinObjectType.Person ?
                                    $"{HelperFunctions.HandleDisplayPersonType(Lres, p.Source)} {Lres["named"]} {p.Name}" : string.Empty,
                              });
            if(type != null)
            {
                recycleBin = recycleBin.Where(x => x.Type == (int)type);
            }

            return recycleBin.OrderBy(order).Paginate(pageIndex, pageSize);
        }

        public bool Restore(int objectId, RecycleBinObjectType objectType)
        {
            var currentDate = DateTime.Now;
            var currentLunarDate = HelperFunctions.GetLunarDate(IsLeapYear, currentDate);
            RecycleBin recycleBin = context.RecycleBins.FirstOrDefault(x => x.ObjectId == objectId && x.Type == (int)objectType);
            switch (objectType)
            {
                case RecycleBinObjectType.Meat:
                    var meat = context.Meats.FirstOrDefault(x => x.Id == objectId);
                    if (meat == null || IsMeatExisted(meat))
                    {
                        return false;
                    }
                    meat.IsDeleted = false;

                    context.Remove(recycleBin);
                    context.SaveChanges();
                    return true;
                case RecycleBinObjectType.Person:
                    var person = context.People.FirstOrDefault(x => x.Id == objectId);
                    if (person == null || IsPersonExisted(person))
                    {
                        return false;
                    }
                    person.IsDeleted = false;
                    
                    context.Remove(recycleBin);
                    context.SaveChanges();
                    return true;
                case RecycleBinObjectType.Bill:
                    var bill = context.Bills.FirstOrDefault(x => x.Id == objectId);
                    if(bill == null || IsBillExisted(bill))
                    {
                        return false;
                    }
                    bill.IsDeleted = false;

                    History history = new()
                    {
                        ObjectId = bill.Id,
                        Action = (int)HistoryAction.Restore,
                        Content = $"{Lres["Restore"]} {Lres["Bill"]}",
                        CreatedDate = currentDate,
                        LunarCreatedDate = currentLunarDate,
                        Type = (int)HistoryType.Bill,
                    };

                    context.Add(history);
                    context.Remove(recycleBin);
                    context.SaveChanges();
                    return true;
                default:
                    return false;
            }
        }

        public object GetObjectDetail(int objectId, RecycleBinObjectType objectType)
        {
            return objectType switch
            {
                RecycleBinObjectType.Meat => meatRepository.GetMeatDetail(objectId),
                RecycleBinObjectType.Bill => billRepository.GetBillDetail(objectId),
                RecycleBinObjectType.Person => peopleRepository.GetPersonDetail(objectId),
                _ => null
            };
        }

        private bool IsMeatExisted(Meat meat)
        {
            return context.Meats.Any(x => x.Name.Equals(meat.Name) && x.Frozen == meat.Frozen && x.Type == meat.Type && x.IsDeleted == false);
        }

        private bool IsPersonExisted(Person person)
        {
            return context.People.Any(x => x.Name.Equals(person.Name) && x.Address.Equals(person.Address) && x.Source == person.Source && x.PhoneNumber.Equals(person.PhoneNumber) && x.IsDeleted == false);
        }

        private bool IsBillExisted(Bill bill)
        {
            return context.Bills.Any(x => x.PersonId == bill.PersonId && x.ActiveDate.Value.Date.CompareTo(bill.ActiveDate.Value.Date) == 0 && x.Type == bill.Type && x.IsDeleted == false);
        }
    }
}
