namespace ELibrary.Data.Contract
{
    public interface IUnitOfWork
    {
        Task Save();
    }
}
