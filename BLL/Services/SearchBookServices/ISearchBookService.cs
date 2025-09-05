namespace BLL.Services.SearchBookServices
{
    public interface ISearchBookService
    {
        Task SetToDefault();
        Task SetBookSearchFilterAsync(string value, int filterID);
    }
}