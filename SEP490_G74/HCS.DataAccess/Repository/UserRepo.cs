using HCS.ApplicationContext;
using HCS.DataAccess.IRepository;
using HCS.Domain;
using HCS.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCS.DataAccess.Repository
{
    public class UserRepo : GenericRepo<User>, IUserRepo
    {
        public UserRepo(HCSContext context) : base(context)
        {
        }

        public async Task<UserJWTModel?> GetProfile(string email)
        {
            IQueryable<User> query = _dbSet;
            var user = await query
                .Include(u => u.Contacts)
                .Include(u => u.Role)
                .FirstOrDefaultAsync( u => u.Email.Equals(email));

            if (user != null)
            {
                UserJWTModel profile = new()
                {
                    Email = user.Email,
                    RoleId = user.RoleId,
                    UserId = user.UserId
                };

                if(user.Contacts is { Count: > 0 } )
                {
                    profile.UserName = user.Contacts.First().Name;
                }

                if(user.Role != null)
                {
                    profile.RoleName = user.Role.RoleName;
                }
                return profile;
            }
            return null;
        }

        
    }
}
