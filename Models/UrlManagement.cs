namespace Urlshort.Models;

public class UrlManagement
{
    public int Id { get; set; }
    public string Url { get; set; } = string.Empty;
    public string CompactUrl { get; set; } = string.Empty;
}