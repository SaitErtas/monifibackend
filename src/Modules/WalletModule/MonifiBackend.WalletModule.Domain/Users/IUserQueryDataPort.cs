namespace MonifiBackend.WalletModule.Domain.Users;

public interface IUserQueryDataPort
{
    Task<int> GetUserCountAsync();
    Task<int> GetReferanceCountAsync(int id);

}
