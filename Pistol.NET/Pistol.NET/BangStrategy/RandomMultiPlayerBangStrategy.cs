using System;

namespace Pistol.NET.BangStrategy
{
  class RandomMultiPlayerBangStrategy : IMultiPlayerBangStrategy
  {
    private readonly Random rnd_ = new Random();

    public Tuple<Gun, int, Gun> Bang(int shooterLeftGun, int shooterRightGun, Tuple<int?, int?>[] victims)
    {
      throw new NotImplementedException();
    }

    public Tuple<int, Gun> Bang(int shooterGun, Tuple<int?, int?>[] victims)
    {
      var victimIndex = rnd_.Next(victims.Length);
      var victim = victims[victimIndex];

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

      return new Tuple<int, Gun>(victimIndex, victimGun);
    }

    private Gun GetRandomGun()
    {
      return rnd_.NextBoolean() ? Gun.Right : Gun.Left;
    }
  }
}
