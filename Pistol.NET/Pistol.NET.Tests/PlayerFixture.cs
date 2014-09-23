using NUnit.Framework;
using System;

namespace Pistol.NET.Tests
{
  [TestFixture]
  public class PlayerFixture
  {
    [Test]
    public void TestPlayerConstructor()
    {
      // Act
      var player = new Player("");

      // Assert
      Assert.That(player.Name, Is.EqualTo(""));
      Assert.That(player.LeftGun, Is.EqualTo(1));
      Assert.That(player.RightGun, Is.EqualTo(1));
    }

    [Test]
    public void TestPlayerApplyDamage()
    {
      // Arrange
      var player = new Player("");

      // Act
      Assert.Throws<InvalidOperationException>(() => player.ApplyDamage(Gun.None, 2));
      Assert.Throws<ArgumentOutOfRangeException>(() => player.ApplyDamage(Gun.Left, 0));
      Assert.Throws<ArgumentOutOfRangeException>(() => player.ApplyDamage(Gun.Left, 5));
    }
  }
}
