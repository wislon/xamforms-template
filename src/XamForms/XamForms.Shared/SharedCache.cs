using System;
using System.Collections.Generic;
using System.IO;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Akavache;
using Akavache.Sqlite3;
using Splat;
using XamForms.Shared.Interfaces;

namespace XamForms.Shared
{
  public static class SharedCache
  {
    private static bool _initialised;
    private static IPlatformInfo _platformInfo;

    private const string SharedCacheFileName = "SharedCache.db";

    private static ISecureBlobCache _localCache;

    public const string CacheKeyFeatureToggles = "FeatureToggles";
    public const string CacheKeyRemoteConfiguration = "RemoteConfiguration";
    public const string CacheKeyAppVersion = "AppVersion";
    public const string CacheKeyUserModel = "UserModel";

    public static void Init()
    {
      if (_initialised) return;

      _platformInfo = Locator.Current.GetService<IPlatformInfo>();

      BlobCache.ApplicationName = SharedConstants.SharedAkavacheCacheName;
      BlobCache.EnsureInitialized();
      string cacheFileName = Path.Combine(_platformInfo.UserDataPath, SharedCacheFileName);
      _localCache = new SQLiteEncryptedBlobCache(cacheFileName, scheduler: TaskPoolScheduler.Default);
      _initialised = true;
    }

    /// <summary>
    /// MUST BE AWAITED: Automatically serialises and stores the object using the Akavache key-value store 
    /// (an SQLite database),which was initialised when you inititialsed the IBlobCache 
    /// with your device's platspec user directory. 
    /// </summary>
    /// <typeparam name="T">Type of object</typeparam>
    /// <param name="key">String value to store this object against so you can retrieve it later</param>
    /// <param name="objectToCache">The object to store. The object can be a class, or a primitive, 
    /// it doesn't matter. In the case of an object, only public properties will be de/serialised</param>
    /// <param name="absoluteExpiry"></param>
    /// <returns></returns>
    public static async Task PutObjectInCache<T>(string key, T objectToCache, DateTimeOffset? absoluteExpiry = null)
    {
      await _localCache.InsertObject(key, objectToCache, absoluteExpiry);
    }

    /// <summary>
    /// MUST BE AWAITED: invalidates the object in the cache, and then physically deletes it.
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public static async Task DeleteObjectFromCache(string key)
    {
      await _localCache.InvalidateObject<int>(key);
      await _localCache.Vacuum();
    }

    /// <summary>
    /// MUST BE AWAITED: Attempts to locate an object you may have previously stored, and if has it,
    /// it'll deserialise and return it for you. If it's been invalidated (i.e deleted), 
    /// or was never actually stored, it'll return the default object you provide
    /// instead.
    /// </summary>
    /// <typeparam name="T">Type of object you're expecting</typeparam>
    /// <param name="key">String value youm stored this object against originally</param>
    /// <param name="defaultObject">The object to return if the item doesn't exist in the store.</param>
    /// <returns></returns>
    public static async Task<T> GetObjectFromCache<T>(string key, T defaultObject)
    {
      var returnObject = await _localCache.GetObject<T>(key).Catch<T, KeyNotFoundException>(ex => Observable.Return(defaultObject));
      return returnObject;
    }

    public static async Task<T> GetOrFetchObject<T>(string key, Task<T> fetchDataFunction, DateTime? absoluteExpiration = null)
    {
      var result = await _localCache.GetOrFetchObject<T>(key, async () => await fetchDataFunction, absoluteExpiration);
      return result;
    }

    public static async Task Invalidate(string cacheKeyName)
    {
      await _localCache.Invalidate(cacheKeyName);
    }

    public static async Task Invalidate(string[] cacheKeyNames)
    {
      await _localCache.Invalidate(cacheKeyNames);
    }

    public static async Task Vacuum()
    {
      await _localCache.Vacuum();
    }

    public static async Task ShutDown()
    {
      await BlobCache.Shutdown();
    }

  }
}