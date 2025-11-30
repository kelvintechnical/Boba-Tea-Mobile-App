namespace BobaTeaApp.Mobile.Services;

public sealed class SecureStorageService
{
    private const string AccessTokenKey = "access_token";
    private const string RefreshTokenKey = "refresh_token";

    public Task<string?> GetAccessTokenAsync() => SecureStorage.Default.GetAsync(AccessTokenKey);
    public Task SetAccessTokenAsync(string token) => SecureStorage.Default.SetAsync(AccessTokenKey, token);

    public Task<string?> GetRefreshTokenAsync() => SecureStorage.Default.GetAsync(RefreshTokenKey);
    public Task SetRefreshTokenAsync(string token) => SecureStorage.Default.SetAsync(RefreshTokenKey, token);

    public void Clear()
    {
        SecureStorage.Default.Remove(AccessTokenKey);
        SecureStorage.Default.Remove(RefreshTokenKey);
    }
}
