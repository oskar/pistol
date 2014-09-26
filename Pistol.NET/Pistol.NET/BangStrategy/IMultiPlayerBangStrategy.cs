using System;

namespace Pistol.NET.BangStrategy
{
  public interface IMultiPlayerBangStrategy
  {
    Tuple<Gun, int, Gun> Bang(int shooterLeftGun, int shooterRightGun, Tuple<int?, int?>[] victims);
    Tuple<int, Gun> Bang(int shooterGun, Tuple<int?, int?>[] victims);
  }
}
