using System.Collections.Generic;
using System.Threading.Tasks;

namespace XamForms.Shared.Interfaces
{
  /// <summary>
  /// Allows platspec implementations of System.IO.File methods (System.IO.File doesn't exist in 
  /// Portable Class Libraries - 
  /// see https://stackoverflow.com/questions/25460581/portable-class-library-does-not-support-system-io-why).
  /// Only implement what you need for this, as you go
  /// </summary>
  public interface IPlatformFile
  {
    bool Exists(string fileName);

    Task AppendAllLines(string fileName, IEnumerable<string> lines);
    Task AppendAllText(string fileName, string text);

    byte[] ReadAllBytes(string fileName);

    void WriteAllBytes(string fileName, byte[] bytes);

    /// <summary>
    /// Delete a file, given a fully qualified path to it  (will check for presence first)
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    void DeleteFile(string fileName);

    /// <summary>
    /// Delete a file, given a fully qualified path to it  (will check for presence first)
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    Task DeleteFileAsync(string fileName);

    /// <summary>
    /// Deletes a set of files (given a set of fully qualified file paths)
    /// </summary>
    /// <param name="fileNames">A list of fully qualified file names (will check for presence first)</param>
    /// <returns></returns>
    Task DeleteFiles(IEnumerable<string> fileNames);

    /// <summary>
    /// Moves (renames) a file from one name to another (within the same directory).
    /// If the destination already exists, it will be deleted first 
    /// (if forceOverWrite is set to true)
    /// </summary>
    /// <param name="newestLookupFile"></param>
    /// <param name="currentLookupFile"></param>
    /// <param name="forceOverWrite"></param>
    void Move(string newestLookupFile, string currentLookupFile, bool forceOverWrite = false);
  }
}