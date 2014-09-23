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
          GameEngine.ComputerBang(computer, human);
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
      var leftHandStatus = player.IsLeftHandDead ? "-" : player.LeftHand.ToString(CultureInfo.InvariantCulture);
      var rightHandStatus = player.IsRightHandDead ? "-" : player.RightHand.ToString(CultureInfo.InvariantCulture);

      Console.WriteLine("{0}  {1}  {2}", playerName, leftHandStatus, rightHandStatus);
    }
  }
}
