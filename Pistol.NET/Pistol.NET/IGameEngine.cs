using System;

namespace Pistol.NET
{
  public interface IGameEngine
  {
    Tuple<Gun, Gun> Bang(int shooterLeftGun, int shooterRightGun, int victimLeftGun, int victimRightGun);
    Gun BangOneOnTwo(int shooterGun, int victimLeftGun, int victimRightGun);
    Gun BangTwoOnOne(int shooterLeftGun, int shooterRightGun, int victimGun);
  }
}
