namespace CeoHelper.Services.Services.Interfaces
{
    public interface IRequestService
    {
        Task Like(long requestId);
        Task Dislike(long requestId);
    }
}
