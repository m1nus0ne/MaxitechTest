namespace WebApi.Services;

public interface IRequestThrottlingService
{
    public bool TryAcquire();
    public void Release();
}