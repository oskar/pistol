using NUnit.Framework;

namespace Pistol.NET.Tests
{
  using System;
  using System.IO;

  [TestFixture]
  public class ConsoleUtilsFixture
  {
    [Test]
    public void TestAsk()
    {
      using (var outputResult = new StringWriter())
      using (var inputReader = new StringReader("My input"))
      {
        // Arrange
        Console.SetOut(outputResult);
        Console.SetIn(inputReader);

        // Act
        var result = ConsoleUtils.Ask("My question: ", 1, 20);

        // Assert
        Assert.That(result, Is.EqualTo("My input"));
        Assert.That(outputResult.ToString(), Is.EqualTo("My question: "));
      }
    }
  }
}
