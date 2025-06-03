using WebApi.Midleware;
using WebApi.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IStringProcessingService, StringProcessingService>();
builder.Services.AddSingleton<IRandomApiService, RandomApiService>();
builder.Services.AddSingleton<IRequestThrottlingService, SemaphoreRequestThrottlingService>();
builder.Services.AddHttpClient("RandomApi", client =>
{
    var baseUrl = builder.Configuration.GetValue<string>("RandomApi:BaseUrl") 
                  ?? "https://www.randomnumberapi.com/api/v1.0/random";
    client.BaseAddress = new Uri(baseUrl);
});
var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<RequestThrottlingMiddleware>();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();