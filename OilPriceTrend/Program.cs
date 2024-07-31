using Microsoft.Extensions.Options;
using OilPriceTrend.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IOilPriceService, OilPriceService>();
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

builder.Services.AddSingleton<IHttpClient>(provider =>
{
    var appSettings = provider.GetRequiredService<IOptions<AppSettings>>().Value;
    return new HttpClientWrapper(appSettings.OilPriceApiUrl);
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

var appSettings = app.Services.GetRequiredService<IOptions<AppSettings>>().Value;
app.Urls.Add(appSettings.Url);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
