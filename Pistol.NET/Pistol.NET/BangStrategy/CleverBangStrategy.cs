using Pistol.NET.Utils;

namespace Pistol.NET.BangStrategy
{
  public class CleverBangStrategy : IBangStrategy
  {
    public System.Tuple<Gun, Gun> Bang(int shooterLeftGun, int shooterRightGun, int victimLeftGun, int victimRightGun)
    {
      throw new System.NotImplementedException();
    }

    public Gun BangOneOnTwo(int shooterGun, int victimLeftGun, int victimRightGun)
    {
      throw new System.NotImplementedException();
    }

    public Gun BangTwoOnOne(int shooterLeftGun, int shooterRightGun, int victimGun)
    {
      if (shooterLeftGun == shooterRightGun)
        return Gun.Left;

      var victimIfShootingWithLeft = shooterLeftGun + victimGun;
      var victimIfShootingWithRight = shooterRightGun + victimGun;

      if (victimIfShootingWithLeft >= 5)
        return Gun.Left;

      if (victimIfShootingWithRight >= 5)
        return Gun.Right;

      if (MathUtils.IsOdd(victimIfShootingWithLeft))
        return Gun.Left;

      if (MathUtils.IsOdd(victimIfShootingWithRight))
        return Gun.Right;

      return shooterLeftGun < shooterRightGun ? Gun.Left : Gun.Right;
    }
  }
}
