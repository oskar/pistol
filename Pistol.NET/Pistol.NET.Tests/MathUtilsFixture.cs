using NUnit.Framework;
using Pistol.NET.Utils;

namespace Pistol.NET.Tests
{
  [TestFixture]
  public class MathUtilsFixture
  {
    [Test]
    public void IsOdd()
    {
      Assert.That(MathUtils.IsOdd(-6), Is.False);
      Assert.That(MathUtils.IsOdd(-1), Is.True);
      Assert.That(MathUtils.IsOdd(0), Is.False);
      Assert.That(MathUtils.IsOdd(1), Is.True);
      Assert.That(MathUtils.IsOdd(2), Is.False);
      Assert.That(MathUtils.IsOdd(3), Is.True);
      Assert.That(MathUtils.IsOdd(17), Is.True);
    }
  }
}
