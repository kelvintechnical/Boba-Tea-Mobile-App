namespace BobaTeaApp.Mobile.Services;

public sealed class ConnectivityService
{
    public bool HasInternet => Connectivity.Current.NetworkAccess == NetworkAccess.Internet;
}
