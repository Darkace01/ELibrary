using ELibrary.Core;
using ELibrary.Data.Contract;
using ELibrary.Data.Implementation;
using ELibrary.Service.Contract;

namespace ELibrary.Service.Implementation;

public class TagService : ITagService
{
    private readonly UnitOfWork _uow;
    public TagService(IUnitOfWork uow)
    {
        _uow = uow as UnitOfWork;
    }
    public async Task Add(Tag tag)
    {
        if (!CheckTagName(tag.Name))
            return;

        tag.Name = tag.Name.ToLower();
        _uow.TagRepo.Add(tag);
        await _uow.Save();
    }

    public async Task AddRange(IEnumerable<Tag> tags)
    {
        _uow.TagRepo.AddRange(tags);
        await _uow.Save();
    }

    public IQueryable<Tag> Get()
    {
        return _uow.TagRepo.GetAll();
    }

    public Tag Get(int id)
    {
        return _uow.TagRepo.Get(id);
    }

    public IQueryable<Tag> GetFeatured()
    {
        return _uow.TagRepo.GetAll().Where(t => t.IsFeatured);
    }

    public async Task Update(Tag tag, string previousName)
    {
        if (!CheckTagName(tag.Name, previousName))
            return;

        tag.Name = tag.Name.ToLower();
        _uow.TagRepo.Update(tag);
        await _uow.Save();
    }

    public bool CheckTagName(string tagName, string previousName = "")
    {
        if (!ValidateTagDetails(tagName))
            return false;

        if (tagName == previousName)
            return true;

        var count = _uow.TagRepo.GetAll().Where(t => t.Name.ToLower() == tagName.ToLower()).Count();
        return count > 0 ? false : true;
    }

    private bool ValidateTagDetails(string tag)
    {
        if (string.IsNullOrEmpty(tag) || string.IsNullOrWhiteSpace(tag) || !char.IsLetterOrDigit(tag[0]))
            return false;

        return true;
    }
}
