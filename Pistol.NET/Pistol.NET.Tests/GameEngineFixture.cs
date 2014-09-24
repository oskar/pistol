using NUnit.Framework;

namespace Pistol.NET.Tests
{
  [TestFixture]
  public class GameEngineFixture
  {
    [Test]
    public void TestBangOneOnTwo()
    {
      // Arrange
      var gameEngine = new GameEngine();

      // Act
      var victimGun = gameEngine.BangOneOnTwo(1, 1, 1);

      // Assert
      Assert.That(victimGun, Is.Not.EqualTo(Gun.None));
    }
  }
}
