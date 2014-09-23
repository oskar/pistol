using NUnit.Framework;

namespace Pistol.NET.Tests
{
  using System;
  using System.Collections.Generic;
  using System.Linq;

  [TestFixture]
  public class RandomExtensionsFixture
  {
    [Test]
    public void TestUniformDistribution()
    {
      // Assert that for 1000 random boolean values, 45-55% of them are true

      // Arrange
      var random = new Random();
      var bools = new List<bool>();

      // Act
      for (var i = 0; i < 1000; i++)
      {
        bools.Add(random.NextBoolean());
      }

      // Assert
      var numberOfTrue = bools.Count(b => b);
      Assert.That(numberOfTrue, Is.GreaterThan(450));
      Assert.That(numberOfTrue, Is.LessThan(550));
    }
  }
}
