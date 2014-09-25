
namespace Pistol.NET
{
  using System;
  using System.Collections.Generic;

  class Program
  {
    static void Main(string[] args)
    {
      var game = new Game();
      var options = new Options();
      if (CommandLine.Parser.Default.ParseArguments(args, options))
      {
        var mode = options.Mode;
        if (string.IsNullOrEmpty(mode))
        {
          mode = ConsoleUtils.Ask("Please enter game mode: ", 1, 10);
        }

        var players = new List<Player>();

        foreach (var letter in mode)
        {
          if (letter == 'H')
          {
            var playerName = ConsoleUtils.Ask("Please enter your name: ", 1, Player.MaxNameLength);
            players.Add(new Player(playerName, new HumanBangStrategy(playerName)));
          }
          else if (letter == 'C')
          {
            players.Add(new Player("Computer", new RandomBangStrategy()));
          }
          else
          {
            throw new ArgumentOutOfRangeException();
          }
        }

        game.Play(players);

        Console.ReadLine();
      }
    }
  }
}
