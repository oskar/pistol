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

      var shooterGun = Gun.None;
      if (shooter.IsLeftGunDead) shooterGun = Gun.Right;
      else if (shooter.IsRightGunDead) shooterGun = Gun.Left;

      var victimGun = Gun.None;
      if (victim.IsLeftGunDead) victimGun = Gun.Right;
      else if (victim.IsRightGunDead) victimGun = Gun.Left;

      if (shooterGun != Gun.None && victimGun != Gun.None)
      {
        // This is the no-brainer move since both guns are fixed
        Shoot(shooter, shooterGun, victim, victimGun);
      }
      else if (shooterGun != Gun.None && victimGun == Gun.None)
      {
        ShooterOneGunVictimTwoGuns(shooter, shooterGun, victim);
      }
      else if (shooterGun == Gun.None && victimGun != Gun.None)
      {
        ShooterTwoGunsVictimOneGun(shooter, victim, victimGun);
      }
      else if (shooterGun == Gun.None && victimGun == Gun.None)
      {
        ShooterTwoGunsVictimTwoGuns(shooter, victim);
      }
      else
      {
        throw new InvalidOperationException("");
      }
    }

    public static void Shoot(Player shooter, Gun shooterGun, Player victim, Gun victimGun)
    {
      if (shooterGun == Gun.None)
        throw new ArgumentException("Shooter gun must be Left or Right", "shooterGun");

      if (victimGun == Gun.None)
        throw new ArgumentException("Victim gun must be Left or Right", "shooterGun");

      switch (shooterGun)
      {
        case Gun.Left: victim.ApplyDamage(victimGun, shooter.LeftGun); break;
        case Gun.Right: victim.ApplyDamage(victimGun, shooter.RightGun); break;
      }
    }

    private static Gun GetRandomGun()
    {
      return rnd_.NextBoolean() ? Gun.Right : Gun.Left;
    }

    private static void ShooterOneGunVictimTwoGuns(Player shooter, Gun shooterGun, Player victim)
    {
      var victimGun = GetRandomGun();

      Shoot(shooter, shooterGun, victim, victimGun);
    }

    private static void ShooterTwoGunsVictimOneGun(Player shooter, Player victim, Gun victimGun)
    {
      var shooterGun = GetRandomGun();

      Shoot(shooter, shooterGun, victim, victimGun);
    }

    private static void ShooterTwoGunsVictimTwoGuns(Player shooter, Player victim)
    {
      var victimGun = GetRandomGun();
      var shooterGun = GetRandomGun();

      Shoot(shooter, shooterGun, victim, victimGun);
    }
  }
}
