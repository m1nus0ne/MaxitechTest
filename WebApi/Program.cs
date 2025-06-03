using WebApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Регистрация сервисов
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IStringProcessingService, StringProcessingService>();
builder.Services.AddSingleton<IRandomApiService, RandomApiService>();
builder.Services.AddHttpClient("RandomApi", client =>
{
    var baseUrl = builder.Configuration.GetValue<string>("RandomApi:BaseUrl") 
                  ?? "https://www.randomnumberapi.com/api/v1.0/random";
    client.BaseAddress = new Uri(baseUrl);
});
var app = builder.Build();

// Настройка конвейера запросов
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();