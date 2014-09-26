using System;
using System.Collections.Generic;

namespace Pistol.NET.Utils
{
  public static class ConsoleUtils
  {
    public static string Ask(string message, ICollection<string> allowedWords)
    {
      return Ask(message, allowedWords.Contains);
    }

    public static string Ask(string message, int minimumLength, int maximumLength)
    {
      return Ask(message, input => input.Length >= minimumLength && input.Length <= maximumLength);
    }

    public static string Ask(string message, Func<string, bool> predicate)
    {
      while (true)
      {
        Console.Write(message);
        var input = Console.ReadLine() ?? "";

        if (predicate(input))
          return input;
      }
    }
  }
}
