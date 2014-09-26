using System;
using System.Collections.Generic;
using System.Linq;
using Pistol.NET.BangStrategy;
using Pistol.NET.Utils;

namespace Pistol.NET
{
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

    private static void Main(string[] args)
    {
      var options = new Options();
      if (CommandLine.Parser.Default.ParseArguments(args, options))
      {
        var mode = options.Mode;
        if (string.IsNullOrEmpty(mode))
        {
          mode = ConsoleUtils.Ask("Please enter game mode: ", input => input.Length > 1 && input.All(c => c == 'H' || c == 'C'));
        }

        var players = new List<Player>();
        var playerNumber = 1;

        foreach (var modeLetter in mode)
        {
          players.Add(CreatePlayerFromModeLetter(modeLetter, playerNumber));
          playerNumber++;
        }

        // Play a game of pistol
        var game = new Game();
        game.Play(players);

        Console.ReadLine();
      }
    }

    private static Player CreatePlayerFromModeLetter(char modeLetter, int playerNumber)
    {
      if (modeLetter == 'H')
      {
        var playerName = ConsoleUtils.Ask(string.Format("Player {0}, please enter your name: ", playerNumber), 1, Player.MaxNameLength);
        return new Player(playerName, new HumanBangStrategy(playerName));
      }

      if (modeLetter == 'C')
      {
        return new Player(random_.NextItem(computerNames_), new RandomBangStrategy());
      }

      throw new InvalidOperationException("Unexpected char in mode string: " + modeLetter);
    }
  }
}
