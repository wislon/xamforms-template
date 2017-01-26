using System.IO;
using SQLite;
using XamForms.Shared.Interfaces;

namespace XamForms.Platform.SQLite
{
  /// <summary>
  /// Custom platform-specific ISQlite implementation we made for 
  /// unit testing and windows-based file management 
  /// (see http://developer.xamarin.com/guides/cross-platform/xamarin-forms/working-with/databases/).
  /// </summary>
  public class SQLitePlatform : ISQLite
  {
    public SQLiteConnection GetConnection(string dbFileName, bool failIfFileDoesNotExist = false)
    {
      if (failIfFileDoesNotExist && !DatabaseFileExists(dbFileName))
      {
        string msg = $"File '{dbFileName}' doesn't exist. Does it still need to be generated/downloaded?";
        throw new FileNotFoundException(msg);
      }
      var connection = new SQLiteConnection(dbFileName);
      return connection;
    }

    /// <summary>
    /// <strong>Warning:</strong> Unless you want to keep this file open for the duration of your app <br/>
    /// You should probably be using the sync version (<see cref="GetConnection"/>. <br/>
    /// This one tends to keep the file open until the app shuts down, effectively <br/>
    /// locking it against updates, wih no way to close it.<br/>
    /// </summary>
    /// <param name="dbFileName"></param>
    /// <param name="failIfFileDoesNotExist"></param>
    /// <returns></returns>
    public SQLiteAsyncConnection GetAsyncConnection(string dbFileName, bool failIfFileDoesNotExist = false)
    {
      if (failIfFileDoesNotExist && !DatabaseFileExists(dbFileName))
      {
        string msg = $"File '{dbFileName}' doesn't exist. Does it still need to be generated/downloaded?";
        throw new FileNotFoundException(msg);
      }
      var connection = new SQLiteAsyncConnection(dbFileName);
      return connection;
    }

    public bool DatabaseFileExists(string databaseFileName)
    {
      return File.Exists(databaseFileName);
    }

  }
}