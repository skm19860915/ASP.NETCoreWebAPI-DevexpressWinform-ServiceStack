
using System.Threading.Tasks;

namespace xperters.queues
{
    public interface IQueueService
    {
        /// Add message to queue
        /// <param name="messageObject">Message to add</param>
        Task AddMessageAsync<T>(T messageObject);

        Task CompleteAsync(string lockToken);
//        int GetMessagesCount();
    }
}
