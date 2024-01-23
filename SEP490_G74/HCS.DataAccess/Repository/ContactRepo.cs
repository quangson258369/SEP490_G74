using HCS.ApplicationContext;
using HCS.DataAccess.IRepository;
using HCS.Domain.Models;

namespace HCS.DataAccess.Repository
{
    public interface IContactRepo : IGenericRepo<Contact>
    {
       
    }
    public class ContactRepo : GenericRepo<Contact>, IContactRepo
    {
        public ContactRepo(HCSContext context) : base(context)
        {
        }
        
    }
}
