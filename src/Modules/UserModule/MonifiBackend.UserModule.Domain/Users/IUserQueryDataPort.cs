using MonifiBackend.Core.Application.Abstractions;
using MonifiBackend.Core.Domain.Resources;

namespace MonifiBackend.UserModule.Domain.Users
{
    public interface IUserQueryDataPort : IQueryDataPort
    {
        Task<QueryResult<User>> GetListAsync(QueryObject userQuery);
        Task<User> GetAsync(int id);
        Task<User> GetAsync(string email, string password);
        Task<bool> GetAsync(string email);
        Task<User> GetEmailAsync(string email);
    }
}
