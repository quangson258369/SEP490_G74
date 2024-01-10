using HCS.ApplicationContext;
using HCS.DataAccess.IRepository;
using HCS.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace HCS.DataAccess.Repository;

public interface ICategoryRepo : IGenericRepo<Category>
{
    Task<List<Category>> GetCategories();
    Task<bool> RemoveCategoryById(int id);
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

    public async Task<bool> RemoveCategoryById(int id)
    {
        var category = await _dbSet.FindAsync(id);
        if(category is null)
        {
            return false;
        }
        else
        {
            category.IsDeleted = true;
            return true;
        }
    }
}