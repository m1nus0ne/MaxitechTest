namespace WebApi.Services;

public interface IRandomApiService
{
    public Task<int> GetRandomNumber(int maxExclusive);
}