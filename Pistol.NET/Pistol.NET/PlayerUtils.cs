using System;
using System.Collections.Generic;
using System.Globalization;

namespace Pistol.NET
{
  public static class PlayerUtils
  {
    public static void PrintPlayers(IEnumerable<Player> players)
    {
      foreach (var player in players)
      {
        PrintPlayer(player);
      }
    }

    public static void PrintPlayer(Player player)
    {
      var playerName = player.Name.PadRight(Player.MaxNameLength);
      var leftGunStatus = player.IsLeftGunDead ? "-" : player.LeftGun.ToString(CultureInfo.InvariantCulture);
      var rightGunStatus = player.IsRightGunDead ? "-" : player.RightGun.ToString(CultureInfo.InvariantCulture);

      Console.WriteLine("{0}  {1}  {2}", playerName, leftGunStatus, rightGunStatus);
    }
  }
}
