using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Connectivity;
using Splat;
using XamForms.Shared.Enums;
using XamForms.Shared.Extensions;

namespace XamForms.Shared
{
  /// <summary>
  /// Handles monitoring and checking of internet connectivity state.
  /// Makes heavy use of James Montemagno's connectivity plugin from 
  /// https://github.com/xamarin/XamarinComponents
  /// </summary>
  public static class SharedNetwork
  {

    public static NetworkConnectedState CurrentConnectedState { get; private set; }

    /// <summary>
    /// Simple connectivity check via xplat connectivity plugin
    /// </summary>
    public static bool IsConnectedToNetwork => CurrentConnectedState == NetworkConnectedState.Connected;

    public static void Init()
    {
      SetupNetworkConnectivityMonitoring();
    }

    /// <summary>
    /// First checks for connectivity, and then checks to see if it can connect to a given host name
    /// </summary>
    /// <param name="url">Web Url to hit (the host name, with or without http(s)://) </param>
    /// <param name="port">Defaults to http port 80</param>
    /// <param name="timeoutMs"></param>
    /// <returns></returns>
    public static async Task<bool> IsWebHostReachable(string url, int port = 80, int timeoutMs = 1000)
    {
      bool connected = IsConnectedToNetwork;

      if (!connected)
      {
        LogHost.Default.Info("Connectivity: Not connected to network/internet");
        return false;
      }

      var last = url.LastIndexOf(':');
      if (last > 6)
      {
        var elements = url.Substring(last).Split(new[] { ':', '/' }, StringSplitOptions.RemoveEmptyEntries);
        port = int.Parse(elements[0]);
        url = url.Replace($":{port}", string.Empty);
      }

      LogHost.Default.Info($"Testing reachability of {url} on port {port} with {timeoutMs}ms timeout...");
      bool result = await CrossConnectivity.Current.IsRemoteReachable(url, port, timeoutMs);
      LogHost.Default.Info($"{url} is {(result ? string.Empty : "not ")}reachable");
      return result;
    }

    /// <summary>
    /// Checks for presence of network connection AND if connected, whether it can see our web api
    /// service. Expensive to call (could be several hundred milliseconds before it returns) so
    /// try not to call it repeatedly (in a loop for instance)
    /// </summary>
    /// <param name="testAddress">Remote address to test (not a url, just a DNS name)</param>
    /// <param name="timeoutMs"></param>
    /// <returns>True if it can see our web service, false if it can't (either no network or our site is down)</returns>
    public static async Task<bool> HasWorkingInternetConnection(string testAddress = null, int timeoutMs = 2000)
    {
      if (!IsConnectedToNetwork) return false;

      string remoteAddress = testAddress.IsEmpty() ? SharedConstants.GetSiteBaseBaseUrl() : testAddress;
      return await IsWebHostReachable(remoteAddress, timeoutMs: timeoutMs);
    }

    private static void SetupNetworkConnectivityMonitoring()
    {
      SetCurrentConnectedState(CrossConnectivity.Current.IsConnected);
      CrossConnectivity.Current.ConnectivityChanged += (sender, args) =>
      {
        SetCurrentConnectedState(args.IsConnected);
      };
    }

    private static void SetCurrentConnectedState(bool isConnected)
    {
      LogHost.Default.Info($"Connectivity State Changed - IsConnected: {isConnected}");
      CurrentConnectedState = isConnected ? NetworkConnectedState.Connected : NetworkConnectedState.Disconnected;
    }

  }
}
