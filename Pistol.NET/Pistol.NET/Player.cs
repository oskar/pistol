
namespace Pistol.NET
{
  using System;

  public class Player
  {
    private const int MaxDamage = 5;

    public Player(string name)
    {
      Name = name;
      LeftHand = 1;
      RightHand = 1;
    }

    public bool IsPlayerDead
    {
      get { return IsLeftHandDead && IsRightHandDead; }
    }

    public bool IsLeftHandDead
    {
      get { return LeftHand >= MaxDamage; }
    }

    public bool IsRightHandDead
    {
      get { return RightHand >= MaxDamage; }
    }

    public int LeftHand { get; private set; }
    public int RightHand { get; private set; }
    public string Name { get; private set; }

    public void ApplyDamage(Gun gun, int damage)
    {
      if (damage < 1 || damage >= MaxDamage)
      {
        throw new ArgumentOutOfRangeException("damage", "Damage must be 1-4.");
      }

      if (gun == Gun.Left)
      {
        if (IsLeftHandDead)
        {
          throw new InvalidOperationException("Cannot shoot left gun, it is already dead.");
        }

        LeftHand += damage;
      }
      else if (gun == Gun.Right)
      {
        if (IsRightHandDead)
        {
          throw new InvalidOperationException("Cannot shoot right gun, it is already dead.");
        }

        RightHand += damage;
      }
      else
      {
        throw new InvalidOperationException("Can only be called with gun set to Left or Right.");
      }
    }
  }
}
