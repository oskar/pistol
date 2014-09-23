using System;

namespace Pistol.NET
{
  public class Player
  {
    public const int MaxDamage = 5;
    public const int MaxNameLength = 20;

    public Player(string name)
    {
      if (name == null)
        throw new ArgumentNullException("name");

      if (name.Length > MaxNameLength)
        name = name.Substring(0, MaxNameLength);

      Name = name;
      LeftGun = 1;
      RightGun = 1;
    }

    public bool IsPlayerDead
    {
      get { return IsLeftGunDead && IsRightGunDead; }
    }

    public bool IsLeftGunDead
    {
      get { return LeftGun >= MaxDamage; }
    }

    public bool IsRightGunDead
    {
      get { return RightGun >= MaxDamage; }
    }

    public int LeftGun { get; private set; }
    public int RightGun { get; private set; }
    public string Name { get; private set; }

    public void ApplyDamage(Gun gun, int damage)
    {
      if (damage < 1 || damage >= MaxDamage)
      {
        throw new ArgumentOutOfRangeException("damage", "Damage must be 1-4.");
      }

      if (gun == Gun.Left)
      {
        if (IsLeftGunDead)
        {
          throw new InvalidOperationException("Cannot shoot left gun, it is already dead.");
        }

        LeftGun += damage;
      }
      else if (gun == Gun.Right)
      {
        if (IsRightGunDead)
        {
          throw new InvalidOperationException("Cannot shoot right gun, it is already dead.");
        }

        RightGun += damage;
      }
      else
      {
        throw new InvalidOperationException("Can only be called with gun set to Left or Right.");
      }
    }
  }
}
