using HCS.ApplicationContext;
using HCS.DataAccess.IRepository;
using HCS.Domain;
using HCS.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
                .Include(u => u.Contact)
                .Include(u => u.Role)
                .FirstOrDefaultAsync( u => u.Email.Equals(email));

            if (user != null)
            {
                UserJWTModel profile = new()
                {
                    Email = user.Email,
                    RoleId = user.RoleId,
                    UserId = user.UserId,
                    IsDeleted = user.IsDeleted
                };

                profile.UserName = user.Contact != null ? user.Contact.Name : string.Empty;  

                if(user.Role != null)
                {
                    profile.RoleName = user.Role.RoleName;
                }
                return profile;
            }
            return null;
        }

        public async Task<List<User>> GetAllDoctorByCategoryIdAsync(int categoryId)
        {
            IQueryable<User> query = _dbSet;
            return await query
                .Where(x => x.CategoryId == categoryId)
                .Include(x => x.Contact)
                .Include(x => x.MedicalRecordDoctors)
                .Include(x => x.Role)
                .ToListAsync();
        }
    }
}
