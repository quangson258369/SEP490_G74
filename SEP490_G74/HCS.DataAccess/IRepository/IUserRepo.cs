using HCS.Domain;
using HCS.Domain.Models;

namespace HCS.DataAccess.IRepository
{
    public interface IUserRepo : IGenericRepo<User>
    {
        public Task<UserJWTModel?> GetProfile(string email);
        public Task<List<User>> GetAllDoctorByCategoryIdAsync(int categoryId);

    }
}
