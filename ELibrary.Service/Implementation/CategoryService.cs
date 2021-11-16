using ELibrary.Core;
using ELibrary.Data.Contract;
using ELibrary.Data.Implementation;
using ELibrary.Service.Contract;

namespace ELibrary.Service.Implementation;

public class CategoryService : ICategoryService
{
    private readonly UnitOfWork _uow;
    public CategoryService(IUnitOfWork uow)
    {
        _uow = uow as UnitOfWork;
    }

    public async Task Add(Category category)
    {
        if (!ValidateBusinessCategoryDetails(category))
            throw new Exception(); // todo: return appropriate exception

        if (!CheckIfCategoryNameExists(category.Name))
        {
            _uow.CategoryRepo.Add(category);
            await _uow.Save();
        }
    }

    public Category Get(int id, bool includeRelationships = false)
    {
        if (includeRelationships)
            return _uow.CategoryRepo.GetInclude(id);
        else
            return _uow.CategoryRepo.Get(id);
    }

    public IQueryable<Category> GetAll(bool includeRelationships)
    {
        if (includeRelationships)
            return _uow.CategoryRepo.GetAllInclude();
        else
            return _uow.CategoryRepo.GetAll();
    }

    public IQueryable<Category> GetAllFeatured(bool includeRelationships)
    {
        if (includeRelationships)
            return _uow.CategoryRepo.FindInclude(b => b.IsFeatured);
        else
            return _uow.CategoryRepo.Find(b => b.IsFeatured);
    }

    public async Task Update(Category category, string previousName)
    {
        if (!ValidateBusinessCategoryDetails(category))
            throw new Exception(); // todo: return appropriate exception

        if (!CheckIfCategoryNameExists(category.Name, previousName))
        {
            _uow.CategoryRepo.Update(category);
            await _uow.Save();
        }
    }

    public async Task Update(Category category)
    {
        if (!ValidateBusinessCategoryDetails(category))
            throw new Exception(); // todo: return appropriate exception

        _uow.CategoryRepo.Update(category);
        await _uow.Save();
    }

    public async Task Delete(Category category)
    {
        _uow.CategoryRepo.Remove(category);
        await _uow.Save();
    }

    public Category GetDefaultBusinessCategory()
    {
        return _uow.CategoryRepo.GetAllInclude().Where(b => b.DefaultCategory).First();
    }

    private bool ValidateBusinessCategoryDetails(Category category)
    {
        if (category == null)
            return false;

        if (string.IsNullOrEmpty(category.Name) || string.IsNullOrWhiteSpace(category.Name) || !char.IsLetterOrDigit(category.Name[0]))
            return false;
        return true;
    }

    private bool CheckIfCategoryNameExists(string name, string previousName = "")
    {
        if (name == previousName)
            return false;

        var categoryCount = _uow.CategoryRepo.GetAll().Where(c => name.ToLower() == c.Name.ToLower()).Count();
        if (categoryCount > 0)
            return true;

        return false;
    }
}
