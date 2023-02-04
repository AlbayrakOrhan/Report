using System.Text.Json;
using Report.Application.Interfaces;
using Report.Infrastructure.ContactService.Models.Response;

namespace Report.Infrastructure.ContactService;

public class ContactServiceCommunicator : IContactServiceCommunicator
{
    private readonly HttpClient _httpClient;
    private readonly ICommunicatorBase _communicatorBase;

    public ContactServiceCommunicator(HttpClient httpClient, ICommunicatorBase communicatorBase)
    {
        _httpClient = httpClient;
        _communicatorBase = communicatorBase;
    }

    public async Task<ReportDataResponse?> GetReportAsync()
    {
        const string resource = "reports";
        var (stringResult, httpResponseMessage) = await _communicatorBase.GetAsJson(_httpClient, resource);
        try
        {
            return JsonSerializer.Deserialize<ReportDataResponse>(stringResult);
        }
        catch (Exception e)
        {
            throw new Exception("Http communication exception");
        }
    }
}