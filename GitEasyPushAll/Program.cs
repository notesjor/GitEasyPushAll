using System.Diagnostics;

namespace GitEasyPushAll
{
  internal class Program
  {
    static void Main(string[] args)
    {
      var directories = FindDirectories();
      foreach (var directory in directories)
      {
        try
        {
          Console.WriteLine("Pushing " + directory);
          var process = new Process
          {
            StartInfo = new ProcessStartInfo
            {
              FileName = "git",
              Arguments = "push --all",
              UseShellExecute = false,
              WorkingDirectory = directory,
              CreateNoWindow = true,
              WindowStyle = ProcessWindowStyle.Hidden,
              RedirectStandardOutput = true,
            }
          };
          process.Start();
          Console.WriteLine(process.StandardOutput.ReadToEnd());
          process.WaitForExit();
        }
        catch (Exception ex)
        {
          Console.WriteLine(ex.Message);
          Console.WriteLine(ex.StackTrace);
        }
      }

      Console.WriteLine("!DONE!");
    }

    private static string[] FindDirectories()
      =>
      Directory.GetDirectories(Environment.CurrentDirectory)
        .Where(dir => Directory.Exists(Path.Combine(dir, ".git")))
        .ToArray();
  }
}