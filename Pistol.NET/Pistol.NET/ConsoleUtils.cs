using System;
using System.Collections.Generic;
using System.Linq;

namespace Pistol.NET
{
  public static class ConsoleUtils
  {
    public static string Ask(string message, IEnumerable<string> allowedWords)
    {
      while (true)
      {
        Console.Write(message);
        var input = Console.ReadLine() ?? "";

        if (allowedWords.Contains(input))
          return input;
      }
    }

    public static string Ask(string message, int minimumLength, int maximumLength)
    {
      while (true)
      {
        Console.Write(message);
        var input = Console.ReadLine() ?? "";

        if (input.Length >= minimumLength && input.Length <= maximumLength)
          return input;
      }
    }
  }
}
