using HCS.ApplicationContext;
using HCS.DataAccess.IRepository;
using HCS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCS.DataAccess.Repository
{
    public class ContactRepo : GenericRepo<Contact>, IContactRepo
    {
        public ContactRepo(HCSContext context) : base(context)
        {
        }
    }
}
