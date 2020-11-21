using System.Threading.Tasks;
using System.Collections.Generic;
using PortalRandkowy.API.Models;
using PortalRandkowy.API.Helpers;

namespace PortalRandkowy.API.Data
{
    public interface IUserRepository : IGenericRepository
    {
        Task<PagedList<User>> GetUsers(UserParams userParams);
        Task<User> GetUser(int id);

        Task<Photo> GetPhoto (int id);

        Task<Photo> GetMeinPhotoForUser(int userId);
        Task<Like> GetLike(int userId, int recipientId);

        Task<Message> GetMessage(int id);
        Task<PagedList<Message>> GetMessagesForUser(MessageParams messageParams);
        Task<IEnumerable<Message>> GetMessageThread(int userId, int recipientId);
    }
}