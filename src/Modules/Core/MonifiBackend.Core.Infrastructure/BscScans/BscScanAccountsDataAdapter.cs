using Microsoft.Extensions.Configuration;
using MonifiBackend.Core.Domain.Accounts;
using MonifiBackend.Core.Domain.BscScans;
using MonifiBackend.Core.Domain.Utility;
using MonifiBackend.Core.Infrastructure.BscScans.Constants;
using System.Net.Http.Headers;
using System.Text.Json;

namespace MonifiBackend.Core.Infrastructure.BscScans;

public class BscScanAccountsDataAdapter : IBscScanAccountsDataPort
{
    private readonly string _bscScanModule;
    private readonly HttpClient _bscScanHttpClient;
    private readonly IConfiguration _configuration;

    /// <inheritdoc />
    public BscScanAccountsDataAdapter(HttpClient bscScanHttpClient, IConfiguration configuration)
    {
        _configuration = configuration;
        _bscScanHttpClient = bscScanHttpClient;

        _bscScanHttpClient.BaseAddress = new Uri(_configuration["ApplicationSettings:BscScanOptions:Uri"]);
        _bscScanHttpClient.DefaultRequestHeaders.Accept.Clear();
        _bscScanHttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MimeTypes.ApplicationJson));

        _bscScanModule = BscScanModule.ACCOUNT.AppendApiKey(_configuration["ApplicationSettings:BscScanOptions:Token"]);
    }

    public async Task<BnbBalance?> GetBnbBalanceAsync(BnbBalanceRequest request)
    {
        var queryParameters = $"{_bscScanModule}{request.ToRequestParameters(AccountsModuleAction.BALANCE)}";
        using var response = await _bscScanHttpClient.GetAsync($"{queryParameters}")
            .ConfigureAwait(false);

        response.EnsureSuccessStatusCode();
        await using var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
        var result = await JsonSerializer.DeserializeAsync<BnbBalance>(responseStream);
        return result;
    }

    /// <inheritdoc />
    public async Task<NormalTransactions?> GetNormalTransactionsByAddressAsync(NormalTransactionsRequest request)
    {
        var queryParameters = $"{_bscScanModule}{request.ToRequestParameters(AccountsModuleAction.TRANSACTION_LIST)}";
        using var response = await _bscScanHttpClient.GetAsync($"{queryParameters}")
            .ConfigureAwait(false);

        response.EnsureSuccessStatusCode();
        await using var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
        var result = await JsonSerializer.DeserializeAsync<NormalTransactions>(responseStream);
        return result;
    }
}
