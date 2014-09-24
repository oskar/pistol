using System;

namespace Pistol.NET
{
  public class HumanBangStrategy : IBangStrategy
  {
    private string name_;

    public HumanBangStrategy(string humanName)
    {
      name_ = humanName;
    }

    public Tuple<Gun, Gun> Bang(int shooterLeftGun, int shooterRightGun, int victimLeftGun, int victimRightGun)
    {
      var command = ConsoleUtils.Ask(string.Format("Your turn, {0} [LL, LR, RL, RR]: ", name_), new[] { "LL", "LR", "RL", "RR" });
      var shooterGun = CommandLetterToGun(command[0]);
      var victimGun = CommandLetterToGun(command[1]);

      return new Tuple<Gun, Gun>(shooterGun, victimGun);
    }

    public Gun BangOneOnTwo(int shooterGun, int victimLeftGun, int victimRightGun)
    {
      var command = ConsoleUtils.Ask(string.Format("Your turn, {0}, you only have one gun [L, R]: ", name_), new[] { "L", "R" });
      var victimGun = CommandLetterToGun(command[0]);

      return victimGun;
    }

    public Gun BangTwoOnOne(int shooterLeftGun, int shooterRightGun, int victimGun)
    {
      var command = ConsoleUtils.Ask(string.Format("Your turn, {0}, opponent only has one gun [L, R]: ", name_), new[] { "L", "R" });
      var shooterGun = CommandLetterToGun(command[0]);

      return shooterGun;
    }

    private static Gun CommandLetterToGun(char commandLetter)
    {
      switch (commandLetter)
      {
        case 'L': return Gun.Left;
        case 'R': return Gun.Right;
        default: throw new InvalidOperationException("Only letters L and R allowed.");
      }
    }
  }
}
