using System;

namespace Pistol.NET.BangStrategy
{
  public class HumanMultiPlayerBangStrategy : IMultiPlayerBangStrategy
  {
    public Tuple<Gun, int, Gun> Bang(int shooterLeftGun, int shooterRightGun, Tuple<int?, int?>[] victims)
    {
      throw new NotImplementedException();
    }

    public Tuple<int, Gun> Bang(int shooterGun, Tuple<int?, int?>[] victims)
    {
      throw new NotImplementedException();
    }
  }
}
