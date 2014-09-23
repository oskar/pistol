using System;

namespace Pistol.NET
{
  using System.Collections.Generic;

  public static class RandomExtensions
  {
    public static bool NextBoolean(this Random rnd)
    {
      return rnd.Next() % 2 == 0;
    }

    public static T NextItem<T>(this Random rnd, IList<T> items)
    {
      if (items == null)
        throw new ArgumentNullException("items");

      if (items.Count < 1)
        throw new ArgumentOutOfRangeException("items", "Items must contain at least one item.");

      return items[rnd.Next(items.Count)];
    }
  }
}
