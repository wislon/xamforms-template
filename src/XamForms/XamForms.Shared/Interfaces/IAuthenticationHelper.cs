using System.Threading.Tasks;

namespace XamForms.Shared.Interfaces
{
    /// <summary>
    /// Interface contract for the object you'll be using to get
    /// auth tokens. Can also be used to register your type with a 
    /// DI container
    /// </summary>
    public interface IAuthenticationHelper
    {
        Task<string> GetToken();
    }
}