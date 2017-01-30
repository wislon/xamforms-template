using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using ModernHttpClient;
using Splat;
using XamForms.Shared.Interfaces;

namespace XamForms.Shared.Helpers
{
  /// <summary>
  /// Inherits from <see cref="NativeMessageHandler"/> so we can still use
  /// ModernHttpClient for SSL stuff (it's faster for SSL connections)
  /// </summary>
  public class AuthenticatedHttpClientHandler : NativeMessageHandler
  {
    private readonly Func<Task<string>> _getToken;

    public AuthenticatedHttpClientHandler(Func<Task<string>> getToken = null) : base(false, false, new NativeCookieHandler())
    {
      if (getToken == null)
      {
        var authHelper = Locator.Current.GetService<IAuthenticationHelper>();
        if (authHelper != null)
        {
          getToken = authHelper.GetToken;
        }
      }

      if (getToken == null) throw new ArgumentNullException(nameof(getToken));
      this._getToken = getToken;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
      // See if the request has an authorize header
      var auth = request.Headers.Authorization;
      if (auth != null)
      {
        var token = await _getToken().ConfigureAwait(false);
        request.Headers.Authorization = new AuthenticationHeaderValue(auth.Scheme, token);
      }

      return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
    }
  }
}