using StackExchange.Redis;
using Urlshort.Models;

public class UrlShortenerService
{
    private readonly IDatabase _cache;
    private readonly ApiDbContext _dbContext;
    private readonly Random _random;

    public UrlShortenerService(IConnectionMultiplexer redis, ApiDbContext dbContext, Random random)
    {
        _cache = redis.GetDatabase();
        _dbContext = dbContext;
        _random = random;
    }

    public async Task<string> ShortenUrlAsync(string longUrl, HttpContext ctx)
    {
        // Validate URL
        if (!Uri.TryCreate(longUrl, UriKind.Absolute, out var input) || input == null)
        {
            throw new ArgumentException("Invalid URL");
        }

        // Check cache
        var cachedUrl = await _cache.StringGetAsync(longUrl);
        if (!string.IsNullOrEmpty(cachedUrl))
        {
            return cachedUrl;
        }

        // Generate short URL
        var randomString = GenerateRandomString();
        var shortUrl = $"{ctx.Request.Scheme}://{ctx.Request.Host}/{randomString}";

        // Save to database
        var urlEntity = new UrlManagement { Url = longUrl, CompactUrl = randomString };
        try
        {
            await _dbContext.Urls.AddAsync(urlEntity);
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            // Log the error (you can use a logging framework)
            throw new Exception("An error occurred while saving the URL", ex);
        }

        // Save to cache
        await _cache.StringSetAsync(longUrl, shortUrl);

        return shortUrl;
    }

    private string GenerateRandomString()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUV1234567890@az";
        return new string(Enumerable.Repeat(chars, 8)
            .Select(x => x[_random.Next(x.Length)]).ToArray());
    }
}