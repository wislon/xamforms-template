using SQLite;

namespace XamForms.Shared.Interfaces
{
  /// <summary>
  /// Dummy interface so we can inject the platform
  /// version instance of SQLite
  /// </summary>
  public interface ISQLite
  {
    /// <summary>
    /// Makes a platform-specific SQLiteConnection. Optional 
    /// parameter to fail the connection if the files doesn't already exist
    /// (otherwise SQLite creates the file by default)
    /// </summary>
    /// <param name="dbFileName"></param>
    /// <param name="failIfFileDoesNotExist"></param>
    /// <returns></returns>
    SQLiteConnection GetConnection(string dbFileName, bool failIfFileDoesNotExist = false);


    /// <summary>
    /// Makes an async-compatible platform-specific SQLiteAsyncConnection. Optional 
    /// parameter to fail the connection if the files doesn't already exist
    /// (otherwise SQLite creates the file by default)
    /// </summary>
    /// <param name="dbFileName"></param>
    /// <param name="failIfFileDoesNotExist"></param>
    /// <returns></returns>
    SQLiteAsyncConnection GetAsyncConnection(string dbFileName, bool failIfFileDoesNotExist = false);

    /// <summary>
    /// Simple method to make a platform-specific call to see if the 
    /// database file actually exists (maybe to do something before actually opening
    /// the connection).
    /// </summary>
    /// <param name="dbFileName"></param>
    /// <returns></returns>
    bool DatabaseFileExists(string dbFileName);
  }
}