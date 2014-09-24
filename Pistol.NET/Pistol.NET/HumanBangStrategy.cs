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
      var command = ConsoleUtils.Ask(string.Format("Your turn, {0} [LL, LR, RL, RR]: ", name_), "");
      var shooterGun = CommandLetterToGun(command[0]);
      var victimGun = CommandLetterToGun(command[1]);

      return new Tuple<Gun, Gun>(shooterGun, victimGun);
    }

    public Gun BangOneOnTwo(int shooterGun, int victimLeftGun, int victimRightGun)
    {
      var command = ConsoleUtils.Ask(string.Format("Your turn, {0}, you only have one gun [L, R]: ", name_), "");
      var victimGun = CommandLetterToGun(command[0]);

      return victimGun;
    }

    public Gun BangTwoOnOne(int shooterLeftGun, int shooterRightGun, int victimGun)
    {
      var command = ConsoleUtils.Ask(string.Format("Your turn, {0}, opponent only has one gun [L, R]: ", name_), "");
      var shooterGun = CommandLetterToGun(command[0]);

      return shooterGun;
    }

    //static void HumanBang(Player shooter, Player victim)
    //{
    //  while (true)
    //  {
    //    Console.Write("{0}, you turn [LL, LR, RL, RR]: ", shooter.Name);

    //    // Ask for what gun to shoot with and which gun to shoot at
    //    var command = Console.ReadLine();

    //    if (!string.IsNullOrEmpty(command))
    //    {
    //      command = command.ToUpper();
    //    }

    //    if (command == "LL")
    //    {
    //      if (!shooter.IsLeftGunDead && !victim.IsLeftGunDead)
    //      {
    //        victim.ApplyDamage(Gun.Left, shooter.LeftGun);
    //        return;
    //      }
    //    }
    //    else if (command == "LR")
    //    {
    //      if (!shooter.IsLeftGunDead && !victim.IsRightGunDead)
    //      {
    //        victim.ApplyDamage(Gun.Right, shooter.LeftGun);
    //        return;
    //      }
    //    }
    //    else if (command == "RL")
    //    {
    //      if (!shooter.IsRightGunDead && !victim.IsLeftGunDead)
    //      {
    //        victim.ApplyDamage(Gun.Left, shooter.RightGun);
    //        return;
    //      }
    //    }
    //    else if (command == "RR")
    //    {
    //      if (!shooter.IsRightGunDead && !victim.IsRightGunDead)
    //      {
    //        victim.ApplyDamage(Gun.Right, shooter.RightGun);
    //        return;
    //      }
    //    }
    //  }
    //}

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
