using System;
using System.Collections.Generic;
using System.Linq;

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
      var shooter = new Player(random_.NextItem(computerNames_));

      // Create a new list of all computer names excluding the selected for the first computer
      var computerNamesExcludingSelected = computerNames_.ToList();
      computerNamesExcludingSelected.Remove(shooter.Name);

      // Get another random name for the second computer
      var victim = new Player(random_.NextItem(computerNamesExcludingSelected));

      var players = new List<Player> { shooter, victim };

      var bangStrategy = new RandomBangStrategy();

      while (!shooter.IsPlayerDead && !victim.IsPlayerDead)
      {
        PlayerUtils.PrintPlayers(players);
        Bang(bangStrategy, shooter, victim);

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
      var human = new Player(humanName);
      var computer = new Player(random_.NextItem(computerNames_));
      var players = new List<Player> { human, computer };
      var humanBangStrategy = new HumanBangStrategy(human.Name);
      var computerBangStrategy = new RandomBangStrategy();

      var isHumanInTurn = true;

      while (!human.IsPlayerDead && !computer.IsPlayerDead)
      {
        PlayerUtils.PrintPlayers(players);

        if (isHumanInTurn)
        {
          Bang(humanBangStrategy, human, computer);
          Console.WriteLine();
        }
        else
        {
          Bang(computerBangStrategy, computer, human);
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

    private static void Bang(IBangStrategy bangStrategy, Player shooter, Player victim)
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
        victimGun = bangStrategy.BangOneOnTwo(shooterGunDamage, victim.LeftGun, victim.RightGun);
      }
      else if (shooterGun == Gun.None && victimGun != Gun.None)
      {
        var victimGunDamage = victim.GetGunDamage(victimGun);
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

      switch (shooterGun)
      {
        case Gun.Left: victim.ApplyDamage(victimGun, shooter.LeftGun); break;
        case Gun.Right: victim.ApplyDamage(victimGun, shooter.RightGun); break;
      }
    }
  }
}
