
namespace Pistol.NET
{
  class Program
  {
    static void Main(string[] args)
    {
      var game = new Game();
      var options = new Options();
      if (CommandLine.Parser.Default.ParseArguments(args, options))
      {
        if (options.Mode == "HumanVsComputer")
        {
          game.PlayHumanAgainstComputer();
        }
        else
        {
          game.PlayTwoComputersAgainstEachOther();
        }
      }
    }
  }
}
