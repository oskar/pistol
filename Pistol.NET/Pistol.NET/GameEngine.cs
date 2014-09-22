using System;

namespace Pistol.NET
{
  public class GameEngine
  {
    private static readonly Random rnd_ = new Random();

    public static void ComputerBang(Player shooter, Player victim)
    {
      if (shooter.IsPlayerDead)
        throw new InvalidOperationException("Shooter is dead and cannot shoot.");

      if (victim.IsPlayerDead)
        throw new InvalidOperationException("Victim is already dead and cannot be shot at.");

      if (victim.IsLeftHandDead)
      {
        // Shoot at victim right hand

        int shooterHand;

        // Decide which hand to shoot with
        if (shooter.IsLeftHandDead) shooterHand = shooter.RightHand;
        else if (shooter.IsRightHandDead) shooterHand = shooter.LeftHand;
        else shooterHand = rnd_.NextBoolean() ? shooter.RightHand : shooter.LeftHand;

        victim.ApplyDamage(Gun.Right, shooterHand);
      }
      else if (victim.IsRightHandDead)
      {
        // Shoot at victim left hand

        int shooterHand;

        // Decide which hand to shoot with
        if (shooter.IsLeftHandDead) shooterHand = shooter.RightHand;
        else if (shooter.IsRightHandDead) shooterHand = shooter.LeftHand;
        else shooterHand = rnd_.NextBoolean() ? shooter.RightHand : shooter.LeftHand;

        victim.ApplyDamage(Gun.Left, shooterHand);
      }
      else
      {
        int shooterHand;

        // Decide which hand to shoot with
        if (shooter.IsLeftHandDead) shooterHand = shooter.RightHand;
        else if (shooter.IsRightHandDead) shooterHand = shooter.LeftHand;
        else shooterHand = rnd_.NextBoolean() ? shooter.RightHand : shooter.LeftHand;

        // Decide which hand to shoot at
        var victimHand = rnd_.NextBoolean() ? Gun.Left : Gun.Right;

        victim.ApplyDamage(victimHand, shooterHand);
      }
    }
  }
}
