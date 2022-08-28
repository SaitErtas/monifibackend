namespace MonifiBackend.WalletModule.Domain.Users;

public interface IUserQueryDataPort
{
    Task<int> GetUserCountAsync();
    Task<int> GetReferanceCountAsync(int id);
    Task<User> GetUserAsync(int id);
    Task<List<User>> GetMeFirstNetworkAsync(int id);
    Task<List<User>> GetAllNetworkAsync(List<int> ids);

    Task<decimal> GetTotalSaleAsync(int id);
    Task<decimal> GetTotalBonusAsync(int id);

}
