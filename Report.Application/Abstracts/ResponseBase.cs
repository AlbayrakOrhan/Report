namespace Report.Application.Abstracts;

public class ResponseBase
{
    public bool Success { get; set; }
    public int MessageCode { get; set; }
    public string Message { get; set; }
    public List<KeyValuePair<string, string[]>> Errors { get; set; }
}