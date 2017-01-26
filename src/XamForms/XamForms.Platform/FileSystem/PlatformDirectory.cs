using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using XamForms.Shared.Interfaces;

namespace XamForms.Platform.FileSystem
{
  public class PlatformDirectory : IPlatformDirectory
  {
    public Task<string[]> GetFiles(string path, string searchPattern, bool topDirectoryOnly = true)
    {
      return Task.Run(() => Directory.GetFiles(path, searchPattern, topDirectoryOnly ? SearchOption.TopDirectoryOnly : SearchOption.AllDirectories));
    }
  }
}