using System;
using System.Collections.Generic;

namespace Pistol.NET
{
  class Program
  {
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
      var players = new List<Player> { shooter, victim };

      while (!shooter.IsPlayerDead && !victim.IsPlayerDead)
      {
        PrintPlayers(players);
        GameEngine.ComputerBang(shooter, victim);

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
          GameEngine.ComputerBang(computer, human);
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
            victim.ApplyDamage(Gun.Left, shooter.LeftHand);
            return;
          }
        }
        else if (command == "LR")
        {
          if (!shooter.IsLeftHandDead && !victim.IsRightHandDead)
          {
            victim.ApplyDamage(Gun.Right, shooter.LeftHand);
            return;
          }
        }
        else if (command == "RL")
        {
          if (!shooter.IsRightHandDead && !victim.IsLeftHandDead)
          {
            victim.ApplyDamage(Gun.Left, shooter.RightHand);
            return;
          }
        }
        else if (command == "RR")
        {
          if (!shooter.IsRightHandDead && !victim.IsRightHandDead)
          {
            victim.ApplyDamage(Gun.Right, shooter.RightHand);
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
  }
}
