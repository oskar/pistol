using CommandLine;
using CommandLine.Text;

namespace Pistol.NET
{
  public class Options
  {
    [Option("mode", DefaultValue = "HumanVsComputer")]
    public string Mode { get; set; }

    [HelpOption]
    public string GetUsage()
    {
      var help = new HelpText()
      {
        Heading = new HeadingInfo("Pistol", "0.1"),
        Copyright = new CopyrightInfo("Oskar Hermansson", 2014),
        //AdditionalNewLineAfterOption = true,
        AddDashesToOption = true
      };
      help.AddPreOptionsLine("Usage: Pistol.exe -mode HumanVsComputer");
      help.AddOptions(this);

      return help;
    }
  }
}
