using NUnit.Framework;

namespace Pistol.NET.Tests
{
  [TestFixture]
  public class GameEngineFixture
  {
    [Test]
    public void TestComputerBangDoesntModifyShooter()
    {
      // Arrange
      var player1 = new Player("");
      var player2 = new Player("");

      Assert.That(player1.Name, Is.EqualTo(""));
      Assert.That(player1.LeftGun, Is.EqualTo(1));
      Assert.That(player1.RightGun, Is.EqualTo(1));

      // Act
      GameEngine.ComputerBang(player1, player2);

      // Assert
      Assert.That(player1.Name, Is.EqualTo(""));
      Assert.That(player1.LeftGun, Is.EqualTo(1));
      Assert.That(player1.RightGun, Is.EqualTo(1));
    }
  }
}
