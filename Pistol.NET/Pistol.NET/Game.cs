using System;
using System.Collections.Generic;
using System.Linq;
using Pistol.NET.Utils;

namespace Pistol.NET
{
  public class Game
  {
    public void Play(IEnumerable<Player> players)
    {
      var playersRing = new LinkedList<Player>(players);
      var currentPlayer = playersRing.First;

      // Play as long as at least two players are alive
      while (playersRing.Count(p => !p.IsPlayerDead) > 1)
      {
        // Print game status
        PlayerUtils.PrintPlayers(playersRing);

        // Current player shoots at next alive player in line
        Bang(currentPlayer.Value, GetNextAlivePlayer(currentPlayer).Value);

        // Set next alive player to current (important to calculate next alive player again here
        // because the previous next alive player might have been killed by the bang above)
        currentPlayer = GetNextAlivePlayer(currentPlayer);
      }

      PlayerUtils.PrintPlayers(playersRing);
      Console.WriteLine("Game over");
    }

    private static LinkedListNode<Player> GetNextAlivePlayer(LinkedListNode<Player> player)
    {
      var startingPlayer = player;

      while (true)
      {
        // Get next player in line
        player = player.Next ?? player.List.First;

        // Return null when we have tried all players and are back where we started
        // (or if the linked list only contains one item)
        if (player == startingPlayer)
          return null;

        // Return player if alive, otherwise continue searching
        if (!player.Value.IsPlayerDead)
          return player;
      }
    }

    private static void Bang(Player shooter, Player victim)
    {
      if (shooter.IsPlayerDead)
        throw new InvalidOperationException("Shooter is dead and cannot shoot.");

      if (victim.IsPlayerDead)
        throw new InvalidOperationException("Victim is already dead and cannot be shot at.");

      var shooterGun = Gun.None;
      if (shooter.IsLeftGunDead) shooterGun = Gun.Right;
      else if (shooter.IsRightGunDead) shooterGun = Gun.Left;

      var victimGun = Gun.None;
      if (victim.IsLeftGunDead) victimGun = Gun.Right;
      else if (victim.IsRightGunDead) victimGun = Gun.Left;

      if (shooterGun != Gun.None && victimGun != Gun.None)
      {
        // This is the no-brainer move since both guns are fixed
      }
      else if (shooterGun != Gun.None && victimGun == Gun.None)
      {
        var shooterGunDamage = shooter.GetGunDamage(shooterGun);
        victimGun = shooter.BangStrategy.BangOneOnTwo(shooterGunDamage, victim.LeftGun, victim.RightGun);
      }
      else if (shooterGun == Gun.None && victimGun != Gun.None)
      {
        var victimGunDamage = victim.GetGunDamage(victimGun);
        shooterGun = shooter.BangStrategy.BangTwoOnOne(shooter.LeftGun, shooter.RightGun, victimGunDamage);
      }
      else if (shooterGun == Gun.None && victimGun == Gun.None)
      {
        var res = shooter.BangStrategy.Bang(shooter.LeftGun, shooter.RightGun, victim.LeftGun, victim.RightGun);
        shooterGun = res.Item1;
        victimGun = res.Item2;
      }
      else
      {
        throw new InvalidOperationException("");
      }

      // Apply damage
      switch (shooterGun)
      {
        case Gun.Left: victim.ApplyDamage(victimGun, shooter.LeftGun); break;
        case Gun.Right: victim.ApplyDamage(victimGun, shooter.RightGun); break;
      }
    }
  }
}
