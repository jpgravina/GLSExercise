using OilPriceTrend.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IOilPriceService, OilPriceService>();
builder.Services.AddSingleton<IHttpClient>(new OilPriceTrend.Services.HttpClient("https://glsitaly-download.s3.eu-central-1.amazonaws.com/MOBILE_APP/BrentDaily/brent-daily.json"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
