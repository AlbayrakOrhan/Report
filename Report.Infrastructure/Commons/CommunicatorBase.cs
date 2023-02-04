using System.Net.Http.Json;
using Report.Application.Interfaces;

namespace Report.Infrastructure.Commons;

public class CommunicatorBase : ICommunicatorBase
{
    public async Task<(string, HttpResponseMessage)> PostAsJson(HttpClient client, string resource, object requestModel)
    {
        var httpResponseMessage = await client.PostAsJsonAsync(resource, requestModel);
        var result = await httpResponseMessage.Content.ReadAsStringAsync();
        return (result, httpResponseMessage);
    }

    public async Task<(string, HttpResponseMessage)> PutAsJson(HttpClient client, string resource, object requestModel)
    {
        var httpResponseMessage = await client.PutAsJsonAsync(resource, requestModel);
        var result = await httpResponseMessage.Content.ReadAsStringAsync();
        return (result, httpResponseMessage);
    }

    public async Task<(string, HttpResponseMessage)> GetAsJson(HttpClient client, string resource)
    {
        using var httpResponseMessage = await client.GetAsync(resource);
        var result = await httpResponseMessage.Content.ReadAsStringAsync();
        return (result, httpResponseMessage);
    }
}