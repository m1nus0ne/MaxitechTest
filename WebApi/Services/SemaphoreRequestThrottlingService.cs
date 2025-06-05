namespace WebApi.Services;

public class SemaphoreRequestThrottlingService : IRequestThrottlingService
{
    private readonly SemaphoreSlim _semaphore;
    private readonly int _maxRequests;

    public SemaphoreRequestThrottlingService(IConfiguration configuration)
    {
        _maxRequests = configuration.GetValue<int>("Settings:ParallelLimit");
        _semaphore = new SemaphoreSlim(_maxRequests, _maxRequests);
    }

    public bool TryAcquire()
    {
        return _semaphore.Wait(0);
    }

    public void Release()
    {
        _semaphore.Release();
    }
}