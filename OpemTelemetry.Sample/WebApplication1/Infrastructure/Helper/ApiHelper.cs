using System.Net;
using LanguageExt.Common;
using Microsoft.CodeAnalysis.Options;
using Microsoft.Extensions.Options;

namespace WebApplication1.Infrastructure.Helper;

public class ApiHelper
{
    private readonly IHttpClientFactory _clientFactory;

    public ApiHelper(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }

    public async Task<Result<T>> GetAsync<T>(string requestUri, CancellationToken ct)
    {
        var client = _clientFactory.CreateClient("Default");
        var response = await client.GetAsync(requestUri, ct);
        var responseMessage = response.EnsureSuccessStatusCode();

        if (responseMessage.IsSuccessStatusCode is false)
        {
            return new Result<T>(new Exception("取得失敗"));
        }

        if (responseMessage.StatusCode == HttpStatusCode.NoContent)
        {
            return default(T)!;
        }

        var result = await response.Content.ReadFromJsonAsync<T>(ct);
        return result;
    }
}
