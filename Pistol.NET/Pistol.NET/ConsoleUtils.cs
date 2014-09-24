using System;

namespace Pistol.NET
{
  public static class ConsoleUtils
  {
    public static string Ask(string message, string allowedRegexPattern)
    {
      while (true)
      {
        Console.Write(message);
        var input = Console.ReadLine() ?? "";

        // TODO: Verify input against allowedRegexPattern

        return input;
      }
    }
  }
}
