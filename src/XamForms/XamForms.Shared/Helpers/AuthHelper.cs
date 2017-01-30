using System.Threading.Tasks;
using XamForms.Shared.Interfaces;

namespace XamForms.Shared.Helpers
{
  public class AuthHelper : IAuthenticationHelper
  {
    public AuthHelper()
    {

    }

    public async Task<string> GetToken()
    {
      // TODO retrieve token from remote source
      string placeholderToken = await Task.FromResult(string.Empty);

#if DEBUG
      // this.Log().Debug($"Access Token is {ar.Token}");
#endif
      return placeholderToken;
    }
  }
}