using NUnit.Framework;

namespace Pistol.NET.Tests
{
  using System.Collections.Generic;

  [TestFixture]
  public class RandomBangStrategyFixture
  {
    [Test]
    public void TestBangOneOnTwo_NeverReturnsNone()
    {
      // Arrange
      var bangStrategy = new RandomBangStrategy();
      var victimGuns = new List<Gun>();

      // Act
      for (int i = 0; i < 100; i++)
      {
        victimGuns.Add(bangStrategy.BangOneOnTwo(1, 1, 1));
      }

      // Assert
      Assert.That(victimGuns, Is.All.Not.EqualTo(Gun.None));
    }

    [Test]
    public void TestBangTwoOnOne_NeverReturnsNone()
    {
      // Arrange
      var bangStrategy = new RandomBangStrategy();
      var shooterGuns = new List<Gun>();

      // Act
      for (int i = 0; i < 100; i++)
      {
        shooterGuns.Add(bangStrategy.BangTwoOnOne(1, 1, 1));
      }

      // Assert
      Assert.That(shooterGuns, Is.All.Not.EqualTo(Gun.None));
    }

    [Test]
    public void TestBang_NeverReturnsNone()
    {
      // Arrange
      var bangStrategy = new RandomBangStrategy();
      var shooterGuns = new List<Gun>();
      var victimGuns = new List<Gun>();

      // Act
      for (int i = 0; i < 100; i++)
      {
        var res = bangStrategy.Bang(1, 1, 1, 1);
        shooterGuns.Add(res.Item1);
        victimGuns.Add(res.Item2);
      }

      // Assert
      Assert.That(shooterGuns, Is.All.Not.EqualTo(Gun.None));
    }
  }
}
