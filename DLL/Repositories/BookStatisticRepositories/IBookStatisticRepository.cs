namespace DLL.Repositories.BookStatisticRepositories
{
    public interface IBookStatisticRepository
    {
        Task SetToDefault();
        Task SetBookStatisticFilterAsync(int periodFilterID);
    }
}