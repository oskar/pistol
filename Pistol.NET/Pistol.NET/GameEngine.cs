using System;

namespace Pistol.NET
{
  public class GameEngine : IGameEngine
  {
    private static readonly Random rnd_ = new Random();

    private static Gun GetRandomGun()
    {
      return rnd_.NextBoolean() ? Gun.Right : Gun.Left;
    }

    public Tuple<Gun, Gun> Bang(int shooterLeftGun, int shooterRightGun, int victimLeftGun, int victimRightGun)
    {
      return new Tuple<Gun, Gun>(GetRandomGun(), GetRandomGun());
    }

    public Gun BangOneOnTwo(int shooterGun, int victimLeftGun, int victimRightGun)
    {
      return GetRandomGun();
    }

    public Gun BangTwoOnOne(int shooterLeftGun, int shooterRightGun, int victimGun)
    {
      return GetRandomGun();
    }
  }
}
