using System;

namespace Pistol.NET
{
  public static class RandomExtensions
  {
    public static bool NextBoolean(this Random rnd)
    {
      return rnd.Next() % 2 == 0;
    }
  }
}
