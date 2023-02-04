using System.Text.Json.Serialization;

namespace Report.Infrastructure.ContactService.Models.Response;

public class ReportDataResponse
{
    [JsonPropertyName("reportData")]
    public List<LocationReportDto> ReportData { get; set; }
}

public class LocationReportDto
{
    [JsonPropertyName("location")]
    public string Location { get; set; }
    
    [JsonPropertyName("personCount")]
    public int PersonCount { get; set; }
    
    [JsonPropertyName("phoneNumberCount")]
    public int PhoneNumberCount { get; set; }
}