using System;
using System.Collections.Generic;

namespace Pistol.NET
{
  class Program
  {
    static Random rnd_ = new Random();

    static void Main(string[] args)
    {
      var options = new Options();
      if (CommandLine.Parser.Default.ParseArguments(args, options))
      {
        if (options.Mode == "HumanVsComputer")
        {
          PlayHumanAgainstComputer();
        }
        else
        {
          PlayTwoComputersAgainstEachOther();
        }
      }
    }

    static void PlayTwoComputersAgainstEachOther()
    {
      var shooter = new Player("Hailbop");
      var victim = new Player("Zum-zum");
      var players = new List<Player>() { shooter, victim };

      while (!shooter.IsPlayerDead && !victim.IsPlayerDead)
      {
        PrintPlayers(players);
        ComputerBang(shooter, victim);

        // Swap turns
        var tmp = shooter;
        shooter = victim;
        victim = tmp;

        Console.ReadLine();
      }

      PrintPlayers(players);
      Console.ReadLine();
    }

    static void PlayHumanAgainstComputer()
    {
      Console.Write("Please enter your name: ");
      var humanName = Console.ReadLine();

      var human = new Player(humanName);
      var computer = new Player("Dr. Löky");
      var players = new List<Player>() { human, computer };

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
          ComputerBang(computer, human);
          Console.ReadLine();
        }

        // Swap turns
        isHumanInTurn = !isHumanInTurn;
      }

      // Game is over, one of the players is dead
      PrintPlayers(players);

      if (human.IsPlayerDead)
      {
        Console.WriteLine("Sorry {0}, you lost!", human.Name);
      }
      else
      {
        Console.WriteLine("Congratulations {0}, you won!", human.Name);
      }

      Console.ReadLine();
    }

    static void HumanBang(Player shooter, Player victim)
    {
      while (true)
      {
        Console.Write("{0}, you turn [LL, LR, RL, RR]: ", shooter.Name);

        // Ask for what hand to shoot with and which hand to shoot at
        var command = Console.ReadLine();

        if (!string.IsNullOrEmpty(command))
        {
          command = command.ToUpper();
        }

        if (command == "LL")
        {
          if (!shooter.IsLeftHandDead && !victim.IsLeftHandDead)
          {
            victim.LeftHand += shooter.LeftHand;
            return;
          }
        }
        else if (command == "LR")
        {
          if (!shooter.IsLeftHandDead && !victim.IsRightHandDead)
          {
            victim.RightHand += shooter.LeftHand;
            return;
          }
        }
        else if (command == "RL")
        {
          if (!shooter.IsRightHandDead && !victim.IsLeftHandDead)
          {
            victim.LeftHand += shooter.RightHand;
            return;
          }
        }
        else if (command == "RR")
        {
          if (!shooter.IsRightHandDead && !victim.IsRightHandDead)
          {
            victim.RightHand += shooter.RightHand;
            return;
          }
        }

        Console.WriteLine("Invalid move, try again");
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
      if (player.IsPlayerDead)
      {
        Console.WriteLine("{0}:\tDEAD!", player.Name);
        return;
      }

      var leftHandStatus = player.IsLeftHandDead ? "-" : player.LeftHand.ToString();
      var rightHandStatus = player.IsRightHandDead ? "-" : player.RightHand.ToString();
      Console.WriteLine("{0}:\t{1}\t{2}", player.Name, leftHandStatus, rightHandStatus);
    }

    static void ComputerBang(Player shooter, Player victim)
    {
      if (shooter.IsPlayerDead)
        throw new InvalidOperationException("Shooter is dead and cannot shoot.");

      if (victim.IsPlayerDead)
        throw new InvalidOperationException("Victim is already dead and cannot be shot at.");

      if (victim.IsLeftHandDead)
      {
        // Shoot at victim right hand

        if (shooter.IsLeftHandDead)
        {
          victim.RightHand += shooter.RightHand;
          return;
        }
        else if (shooter.IsRightHandDead)
        {
          victim.RightHand += shooter.LeftHand;
          return;
        }
        else
        {
          // Shooter has both hands alive, decide which hand to use when shooting at victim's right hand

          // For now just choose one hand at random
          victim.RightHand += rnd_.Next() % 2 == 0 ? shooter.RightHand : shooter.LeftHand;
          return;
        }
      }
      else if (victim.IsRightHandDead)
      {
        // Shoot at victim left hand

        if (shooter.IsLeftHandDead)
        {
          victim.LeftHand += shooter.RightHand;
          return;
        }
        else if (shooter.IsRightHandDead)
        {
          victim.LeftHand += shooter.LeftHand;
          return;
        }
        else
        {
          // Shooter has both hands alive, decide which hand to use when shooting at victim's right hand

          // For now just choose one hand at random
          victim.LeftHand += rnd_.Next() % 2 == 0 ? shooter.RightHand : shooter.LeftHand;
          return;
        }
      }
      else
      {
        // Decide which hand to shoot at
        var shooterHand = rnd_.Next() % 2 == 0 ? shooter.RightHand : shooter.LeftHand;

        if (rnd_.Next() % 2 == 0)
        {
          victim.LeftHand += shooterHand;
          return;
        }
        else
        {
          victim.RightHand += shooterHand;
          return;
        }
      }
    }
  }
}
