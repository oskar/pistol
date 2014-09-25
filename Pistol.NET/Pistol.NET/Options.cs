using CommandLine;
using CommandLine.Text;

namespace Pistol.NET
{
  public class Options
  {
    [Option("mode", HelpText = "Game mode, use H for human player and C for computer. For example HCC for a game with one player and two computers.")]
    public string Mode { get; set; }

    [HelpOption]
    public string GetUsage()
    {
      var help = new HelpText()
      {
        Heading = new HeadingInfo("Pistol", "0.1"),
        Copyright = new CopyrightInfo("Oskar Hermansson", 2014),
        AddDashesToOption = true
      };
      help.AddPreOptionsLine("Usage: Pistol.exe -mode HC");
      help.AddOptions(this);

      return help;
    }
  }
}
