using System.Threading.Tasks;

namespace XamForms.Shared.Interfaces
{
  /// <summary>
  /// Abstraction interface for System.IO.Directory, since this doesn't exist in
  /// PCLs.
  /// </summary>
  public interface IPlatformDirectory
  {
    Task<string[]> GetFiles(string path, string searchPattern, bool topDirectoryOnly = true);

  }
}