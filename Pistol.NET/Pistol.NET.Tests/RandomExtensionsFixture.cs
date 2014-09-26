using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Pistol.NET.Tests
{
  [TestFixture]
  public class RandomExtensionsFixture
  {
    private readonly Random random_ = new Random();

    [Test]
    public void TestNextBoolean_UniformDistribution()
    {
      // Assert that for 1000 random boolean values, 45-55% of them are true

      // Arrange
      var bools = new List<bool>();

      // Act
      for (var i = 0; i < 1000; i++)
      {
        bools.Add(random_.NextBoolean());
      }

      // Assert
      var numberOfTrue = bools.Count(b => b);
      Assert.That(numberOfTrue, Is.GreaterThan(450));
      Assert.That(numberOfTrue, Is.LessThan(550));
    }

    [Test]
    public void TestNextItem_EmptyList()
    {
      // Act and assert
      Assert.Throws<ArgumentNullException>(() => random_.NextItem((IList<int>)null));
      Assert.Throws<ArgumentOutOfRangeException>(() => random_.NextItem(new List<int>()));
    }

    [Test]
    public void TestNextItem_UniformDistribution()
    {
      // Assert that for 1000 random items picked from a list of three, 33% ± 5% are of each kind

      // Arrange
      var items = new List<string> { "First", "Second", "Third" };
      var resultingItems = new List<string>();

      // Act
      for (int i = 0; i < 1000; i++)
      {
        resultingItems.Add(random_.NextItem(items));
      }

      // Assert
      var numberOfFirst = resultingItems.Count(s => s == "First");
      var numberOfSecond = resultingItems.Count(s => s == "Second");
      var numberOfThird = resultingItems.Count(s => s == "Third");

      Assert.That(numberOfFirst, Is.EqualTo(333).Within(50));
      Assert.That(numberOfSecond, Is.EqualTo(333).Within(50));
      Assert.That(numberOfThird, Is.EqualTo(333).Within(50));
    }
  }
}
