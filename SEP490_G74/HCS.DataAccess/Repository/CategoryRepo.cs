using HCS.ApplicationContext;
using HCS.DataAccess.IRepository;
using HCS.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace HCS.DataAccess.Repository;

public interface ICategoryRepo : IGenericRepo<Category>
{
    Task<List<Category>> GetCategories();
}

public class CategoryRepo : GenericRepo<Category>, ICategoryRepo
{
    public CategoryRepo(HCSContext context) : base(context)
    {
    }

    public Task<List<Category>> GetCategories()
    {
        return _dbSet.ToListAsync();
    }
}