namespace DLL.Repositories.SearchBookRepositories
{
    public interface ISearchBookRepository
    {
        Task SetToDefault();
        Task SetBookSearchFilterAsync(string value, int filterID);
    }
}