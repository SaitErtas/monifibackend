using MonifiBackend.Core.Domain.TronNetworks.Accounts;

namespace MonifiBackend.Core.Domain.TronNetworks;

public interface ITronNetworkAccountsDataPort
{
    /// <summary>
    /// Get Tron for a Single Address
    /// </summary>
    /// <param name="request">BnbBalanceRequest Model</param>
    /// <returns>Returns the BNB balance of a given address.</returns>
    Task<Account?> GetAccountsAsync(string address);
}
