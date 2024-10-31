using System.Net;
using LanguageExt.Common;

namespace WebApplication1.Repository;

public static class ApiHelper
{
    //Get
    public static async Task<Result<T>> GetAsync<T>(string requestUri, CancellationToken ct)
    {
        HttpClient client = new HttpClient();
        client.BaseAddress = new Uri("http://localhost:5001");
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
