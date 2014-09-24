using System;
using System.Collections.Generic;
using System.Globalization;

namespace Pistol.NET
{
  using System.Linq;

  class Program
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

    static void Main(string[] args)
    {
      var bangStrategy = new RandomBangStrategy();
      var options = new Options();
      if (CommandLine.Parser.Default.ParseArguments(args, options))
      {
        if (options.Mode == "HumanVsComputer")
        {
          PlayHumanAgainstComputer(bangStrategy);
        }
        else
        {
          PlayTwoComputersAgainstEachOther(bangStrategy);
        }
      }
    }

    static void PlayTwoComputersAgainstEachOther(IBangStrategy bangStrategy)
    {
      // Get a random name for the first computer
      var shooter = new Player(random_.NextItem(computerNames_));

      // Create a new list of all computer names excluding the selected for the first computer
      var computerNamesExcludingSelected = computerNames_.ToList();
      computerNamesExcludingSelected.Remove(shooter.Name);

      // Get another random name for the second computer
      var victim = new Player(random_.NextItem(computerNamesExcludingSelected));

      var players = new List<Player> { shooter, victim };

      while (!shooter.IsPlayerDead && !victim.IsPlayerDead)
      {
        PrintPlayers(players);
        ComputerBang(bangStrategy, shooter, victim);

        // Swap turns
        var tmp = shooter;
        shooter = victim;
        victim = tmp;

        Console.ReadLine();
      }

      PrintPlayers(players);
      Console.ReadLine();
    }

    static void PlayHumanAgainstComputer(IBangStrategy bangStrategy)
    {
      string humanName = null;
      while (string.IsNullOrEmpty(humanName))
      {
        Console.Write("Please enter your name [max 20 chars]: ");
        humanName = Console.ReadLine();
      }

      var human = new Player(humanName);
      var computer = new Player(random_.NextItem(computerNames_));
      var players = new List<Player> { human, computer };

      var isHumanInTurn = true;

      while (!human.IsPlayerDead && !computer.IsPlayerDead)
      {
        PrintPlayers(players);

        if (isHumanInTurn)
        {
          HumanBang(human, computer);
          Console.WriteLine();
        }
        else
        {
          ComputerBang(bangStrategy, computer, human);
          Console.ReadLine();
        }

        // Swap turns
        isHumanInTurn = !isHumanInTurn;
      }

      // Game is over, one of the players is dead
      PrintPlayers(players);

      Console.WriteLine(human.IsPlayerDead ? "Sorry {0}, you lost!" : "Congratulations {0}, you won!", human.Name);

      Console.ReadLine();
    }

    static void ComputerBang(IBangStrategy bangStrategy, Player shooter, Player victim)
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
        var shooterGunDamage = GunDamageFromPlayer(shooter, shooterGun);
        victimGun = bangStrategy.BangOneOnTwo(shooterGunDamage, victim.LeftGun, victim.RightGun);
      }
      else if (shooterGun == Gun.None && victimGun != Gun.None)
      {
        var victimGunDamage = GunDamageFromPlayer(victim, victimGun);
        shooterGun = bangStrategy.BangTwoOnOne(shooter.LeftGun, shooter.RightGun, victimGunDamage);
      }
      else if (shooterGun == Gun.None && victimGun == Gun.None)
      {
        var res = bangStrategy.Bang(shooter.LeftGun, shooter.RightGun, victim.LeftGun, victim.RightGun);
        shooterGun = res.Item1;
        victimGun = res.Item2;
      }
      else
      {
        throw new InvalidOperationException("");
      }

      Shoot(shooter, shooterGun, victim, victimGun);
    }

    static int GunDamageFromPlayer(Player player, Gun gun)
    {
      switch (gun)
      {
        case Gun.Left: return player.LeftGun;
        case Gun.Right: return player.RightGun;
        default: throw new ArgumentOutOfRangeException("gun");
      }
    }

    public static void Shoot(Player shooter, Gun shooterGun, Player victim, Gun victimGun)
    {
      if (shooterGun == Gun.None)
        throw new ArgumentException("Shooter gun must be Left or Right", "shooterGun");

      if (victimGun == Gun.None)
        throw new ArgumentException("Victim gun must be Left or Right", "shooterGun");

      switch (shooterGun)
      {
        case Gun.Left: victim.ApplyDamage(victimGun, shooter.LeftGun); break;
        case Gun.Right: victim.ApplyDamage(victimGun, shooter.RightGun); break;
      }
    }

    static void HumanBang(Player shooter, Player victim)
    {
      while (true)
      {
        Console.Write("{0}, you turn [LL, LR, RL, RR]: ", shooter.Name);

        // Ask for what gun to shoot with and which gun to shoot at
        var command = Console.ReadLine();

        if (!string.IsNullOrEmpty(command))
        {
          command = command.ToUpper();
        }

        if (command == "LL")
        {
          if (!shooter.IsLeftGunDead && !victim.IsLeftGunDead)
          {
            victim.ApplyDamage(Gun.Left, shooter.LeftGun);
            return;
          }
        }
        else if (command == "LR")
        {
          if (!shooter.IsLeftGunDead && !victim.IsRightGunDead)
          {
            victim.ApplyDamage(Gun.Right, shooter.LeftGun);
            return;
          }
        }
        else if (command == "RL")
        {
          if (!shooter.IsRightGunDead && !victim.IsLeftGunDead)
          {
            victim.ApplyDamage(Gun.Left, shooter.RightGun);
            return;
          }
        }
        else if (command == "RR")
        {
          if (!shooter.IsRightGunDead && !victim.IsRightGunDead)
          {
            victim.ApplyDamage(Gun.Right, shooter.RightGun);
            return;
          }
        }
      }
    }

    static void PrintPlayers(IEnumerable<Player> players)
    {
      foreach (var player in players)
      {
        PrintPlayer(player);
      }
    }

    static void PrintPlayer(Player player)
    {
      var playerName = player.Name.PadRight(Player.MaxNameLength);
      var leftGunStatus = player.IsLeftGunDead ? "-" : player.LeftGun.ToString(CultureInfo.InvariantCulture);
      var rightGunStatus = player.IsRightGunDead ? "-" : player.RightGun.ToString(CultureInfo.InvariantCulture);

      Console.WriteLine("{0}  {1}  {2}", playerName, leftGunStatus, rightGunStatus);
    }
  }
}
