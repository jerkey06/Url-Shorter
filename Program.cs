using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Urlshort.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApiDbContext>(options => options.UseSqlServer(connString));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/CompactUrl", async (UrlDto url, ApiDbContext db, HttpContext ctx) =>
{
    //input validating
    if(!Uri.TryCreate(url.Url, UriKind.Absolute, out var input))
        return Results.BadRequest(error:"Invalid Url");
    //provided url short version
    var random = new Random();
    const string chars = "ABCDEFGHIJKLMNOPQRSTUV1234567890@az";
    var randomString = new string(Enumerable.Repeat(chars, 8)
        .Select(x => x[random.Next(x.Length)]).ToArray());
    //Mapping Urls
    var sUrl = new UrlManagement()
    {
        Url = url.Url,
        CompactUrl = randomString,
    };
    //Db save mapping
    db.Add(sUrl);
    db.SaveChanges();
    //construct
    var result = $"{ctx.Request.Scheme}://{ctx.Request.Host}/{sUrl.CompactUrl}";
    
    return Results.Ok(new UrlShortResponseDto()
    {
        Url = result
    });
    
});

app.MapFallback(async (ApiDbContext db, HttpContext ctx) =>
{
    var path = ctx.Request.Path.ToUriComponent().Trim('/');
    var urlMatch = await db.Urls.FirstOrDefaultAsync(x => 
        x.CompactUrl.ToLower().Trim() == path.ToLower().Trim());

    if (urlMatch == null)
        return Results.BadRequest(error:"Invalid request");
    
    return Results.Redirect(urlMatch.Url);
});

app.Run();

class ApiDbContext : DbContext
{
    public virtual DbSet<UrlManagement> Urls { get; set; }

    public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
    { }
}