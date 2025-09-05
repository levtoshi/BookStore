namespace BLL.Services.BookStatisticServices
{
    public interface IBookStatisticService
    {
        Task SetToDefault();
        Task SetBookStatisticFilterAsync(int periodFilterID);
    }
}