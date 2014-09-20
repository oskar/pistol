
namespace Pistol.NET
{
  public class Player
  {
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
      get { return LeftHand >= 5; }
    }

    public bool IsRightHandDead
    {
      get { return RightHand >= 5; }
    }

    public int LeftHand { get; set; }
    public int RightHand { get; set; }
    public string Name { get; private set; }
  }
}
