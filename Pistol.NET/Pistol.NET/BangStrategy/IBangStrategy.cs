using System;

namespace Pistol.NET.BangStrategy
{
  public interface IBangStrategy
  {
    Tuple<Gun, Gun> Bang(int shooterLeftGun, int shooterRightGun, int victimLeftGun, int victimRightGun);
    Gun BangOneOnTwo(int shooterGun, int victimLeftGun, int victimRightGun);
    Gun BangTwoOnOne(int shooterLeftGun, int shooterRightGun, int victimGun);
  }
}
