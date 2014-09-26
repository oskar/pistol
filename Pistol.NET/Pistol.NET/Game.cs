using System;
using System.Collections.Generic;
using System.Linq;
using Pistol.NET.BangStrategy;
using Pistol.NET.Utils;

namespace Pistol.NET
{
  public class Game
  {
    private static readonly Random random_ = new Random();

    private static readonly IList<string> computerNames_ = new List<string>
                                                   {
                                                     "Hail Bop",
                                                     "Default",
                                                     "Firewater",
                                                     "Waveforms",
                                                     "Zumm zumm",
                                                     "WOR",
                                                     "Storm",
                                                     "Silver Rays"
                                                   };

    public void PlayTwoComputersAgainstEachOther()
    {
      // Get a random name for the first computer
      var shooter = new Player(random_.NextItem(computerNames_), new RandomBangStrategy());

      // Create a new list of all computer names excluding the selected for the first computer
      var computerNamesExcludingSelected = computerNames_.ToList();
      computerNamesExcludingSelected.Remove(shooter.Name);

      // Get another random name for the second computer
      var victim = new Player(random_.NextItem(computerNamesExcludingSelected), new RandomBangStrategy());

      var players = new List<Player> { shooter, victim };

      while (!shooter.IsPlayerDead && !victim.IsPlayerDead)
      {
        PlayerUtils.PrintPlayers(players);
        Bang(shooter, victim);

        // Swap turns
        var tmp = shooter;
        shooter = victim;
        victim = tmp;

        Console.ReadLine();
      }

      PlayerUtils.PrintPlayers(players);
      Console.ReadLine();
    }

    public void PlayHumanAgainstComputer()
    {
      var humanName = ConsoleUtils.Ask("Please enter your name [max 20 chars]: ", 1, Player.MaxNameLength);

      var human = new Player(humanName, new HumanBangStrategy(humanName));
      var computer = new Player(random_.NextItem(computerNames_), new RandomBangStrategy());
      var players = new List<Player> { human, computer };

      var isHumanInTurn = true;

      while (!human.IsPlayerDead && !computer.IsPlayerDead)
      {
        PlayerUtils.PrintPlayers(players);

        if (isHumanInTurn)
        {
          Bang(human, computer);
          Console.WriteLine();
        }
        else
        {
          Bang(computer, human);
          Console.ReadLine();
        }

        // Swap turns
        isHumanInTurn = !isHumanInTurn;
      }

      // Game is over, one of the players is dead
      PlayerUtils.PrintPlayers(players);
      Console.WriteLine(human.IsPlayerDead ? "Sorry {0}, you lost!" : "Congratulations {0}, you won!", human.Name);
      Console.ReadLine();
    }

    public void PlayThreePlayers()
    {
      var comp1 = new Player("Comp 1", new RandomBangStrategy());
      var human = new Player("Oskar", new HumanBangStrategy("Oskar"));
      var comp3 = new Player("Comp 3", new RandomBangStrategy());

      var players = new[] { comp1, human, comp3 };

      Play(players);

      PlayerUtils.PrintPlayers(players);
      Console.ReadLine();
    }

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
        // Get next player
        player = player.Next ?? player.List.First;

        if (player == startingPlayer)
          return null;

        if (!player.Value.IsPlayerDead) return player;
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
