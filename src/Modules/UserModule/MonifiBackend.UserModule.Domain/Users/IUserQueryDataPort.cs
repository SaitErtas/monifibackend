using MonifiBackend.Core.Application.Abstractions;
using MonifiBackend.Core.Domain.Resources;

namespace MonifiBackend.UserModule.Domain.Users
{
    public interface IUserQueryDataPort : IQueryDataPort
    {
        Task<QueryResult<User>> GetListAsync(QueryObject userQuery);
        Task<User> GetAsync(int id);
        Task<User> GetAsync(string email, string password);
        Task<User> GetUserConfirmationCodeAsync(string confirmationCode);
        Task<bool> CheckUserEmailAsync(string email);
        Task<User> GetReferanceCodeUserAsync(string referanceCode);
        Task<bool> CheckUserReferanceCodeAsync(string referanceCode);
        Task<bool> CheckUserConfirmationCodeAsync(string confirmationCode);
        Task<User> GetEmailAsync(string email);
    }
}
