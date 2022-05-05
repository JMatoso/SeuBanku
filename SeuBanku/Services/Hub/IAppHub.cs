namespace SeuBanku.Services.Hub
{
    public interface IAppHub
    {
        Task Result(object data);
        Task BadRequest(string message);
        Task Successful(string message, object? additionalData = null);
    }
}
