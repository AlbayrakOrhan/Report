namespace Report.Application.Interfaces;

public interface ICommunicatorBase
{
    Task<(string, HttpResponseMessage)> PostAsJson(HttpClient client, string resource, object requestModel);

    Task<(string, HttpResponseMessage)> PutAsJson(HttpClient client, string resource, object requestModel);

    Task<(string, HttpResponseMessage)> GetAsJson(HttpClient client, string resource);
}