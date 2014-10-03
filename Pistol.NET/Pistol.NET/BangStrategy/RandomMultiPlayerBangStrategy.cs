using System;

namespace Pistol.NET.BangStrategy
{
  public class RandomMultiPlayerBangStrategy : IMultiPlayerBangStrategy
  {
    private readonly Random rnd_ = new Random();

    public Tuple<Gun, int, Gun> Bang(int shooterLeftGun, int shooterRightGun, Tuple<int?, int?>[] victims)
    {
      // Get a random victim
      var victimIndex = rnd_.Next(victims.Length);
      var victim = victims[victimIndex];

      // Decide which gun to shoot at
      var victimGun = GetVictimGun(victim);

      var shooterGun = GetRandomGun();

      return new Tuple<Gun, int, Gun>(shooterGun, victimIndex, victimGun);
    }

    public Tuple<int, Gun> Bang(int shooterGun, Tuple<int?, int?>[] victims)
    {
      // Get a random victim
      var victimIndex = rnd_.Next(victims.Length);
      var victim = victims[victimIndex];

      // Decide which gun to shoot at
      var victimGun = GetVictimGun(victim);

      return new Tuple<int, Gun>(victimIndex, victimGun);
    }

    private Gun GetRandomGun()
    {
      return rnd_.NextBoolean() ? Gun.Right : Gun.Left;
    }

    private Gun GetVictimGun(Tuple<int?, int?> victim)
    {
      // Decide which gun to shoot at
      Gun victimGun;
      if (victim.Item1.HasValue && victim.Item2.HasValue)
      {
        victimGun = GetRandomGun();
      }
      else if (victim.Item1.HasValue)
      {
        victimGun = Gun.Left;
      }
      else if (victim.Item2.HasValue)
      {
        victimGun = Gun.Right;
      }
      else
      {
        throw new InvalidOperationException("Victim is not alive.");
      }

      return victimGun;
    }
  }
}
