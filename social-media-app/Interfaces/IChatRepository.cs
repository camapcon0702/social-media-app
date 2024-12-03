using social_media_app.Models;

namespace social_media_app.IRepository
{
    public interface IChatRepository
    {
        Task<Chat> CreateChatAsync(User user, User user2);
        Task<Chat> FindChatByIdAsync(int chatId);
        Task<Chat> FindUsersChatAsync(int userId);
    }
}
