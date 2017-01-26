using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using XamForms.Shared.Interfaces;

namespace XamForms.Platform.FileSystem
{
  public class PlatformFile : IPlatformFile
  {
    public bool Exists(string fileName)
    {
      return System.IO.File.Exists(fileName);
    }

    public byte[] ReadAllBytes(string fileName)
    {
      return System.IO.File.ReadAllBytes(fileName);
    }

    public void WriteAllBytes(string fileName, byte[] bytes)
    {
      System.IO.File.WriteAllBytes(fileName, bytes);
    }

    public Task AppendAllLines(string fileName, IEnumerable<string> lines)
    {
      return Task.Run(() => System.IO.File.AppendAllLines(fileName, lines.ToArray()));
    }

    public Task AppendAllText(string fileName, string text)
    {
      return Task.Run(() => System.IO.File.AppendAllText(fileName, text));
    }

    public void DeleteFile(string fileName)
    {
      InternalDeleteFile(fileName);
    }

    public Task DeleteFileAsync(string fileName)
    {
      return Task.Run(() => InternalDeleteFile(fileName));
    }

    public Task DeleteFiles(IEnumerable<string> fileNames)
    {
      return Task.Run(() =>
      {
        foreach (var fileName in fileNames)
        {
          InternalDeleteFile(fileName);
        }
      });
    }

    public void Move(string oldFileName, string newFileName, bool forceOverWrite = false)
    {
      if (forceOverWrite && File.Exists(newFileName))
      {
        InternalDeleteFile(newFileName);
      }
      System.IO.File.Move(oldFileName, newFileName);
    }


    private void InternalDeleteFile(string fileName)
    {
      if (File.Exists(fileName))
      {
        System.IO.File.Delete(fileName);
      }
    }
  }
}