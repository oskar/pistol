using System.Collections.Generic;
using NUnit.Framework;
using Pistol.NET.BangStrategy;

namespace Pistol.NET.Tests
{
  [TestFixture]
  public class CleverBangStrategyFixture
  {
    [Test]
    public void TwoOnOne_Choose_left_if_enough_to_kill()
    {
      var bangStrategy = new CleverBangStrategy();
      Assert.That(bangStrategy.BangTwoOnOne(2, 1, 3), Is.EqualTo(Gun.Left));
      Assert.That(bangStrategy.BangTwoOnOne(3, 1, 2), Is.EqualTo(Gun.Left));
      Assert.That(bangStrategy.BangTwoOnOne(4, 1, 2), Is.EqualTo(Gun.Left));
      Assert.That(bangStrategy.BangTwoOnOne(5, 1, 3), Is.EqualTo(Gun.Left));
    }

    [Test]
    public void TwoOnOne_Choose_right_if_enough_to_kill()
    {
      var bangStrategy = new CleverBangStrategy();
      Assert.That(bangStrategy.BangTwoOnOne(1, 2, 3), Is.EqualTo(Gun.Right));
      Assert.That(bangStrategy.BangTwoOnOne(1, 3, 2), Is.EqualTo(Gun.Right));
      Assert.That(bangStrategy.BangTwoOnOne(1, 4, 2), Is.EqualTo(Gun.Right));
      Assert.That(bangStrategy.BangTwoOnOne(1, 5, 3), Is.EqualTo(Gun.Right));
    }

    [Test]
    public void TwoOnOne_Make_victim_gun_odd_size_to_avoid_split()
    {
      var bangStrategy = new CleverBangStrategy();

      Assert.That(bangStrategy.BangTwoOnOne(1, 2, 1), Is.EqualTo(Gun.Right));
      Assert.That(bangStrategy.BangTwoOnOne(2, 1, 1), Is.EqualTo(Gun.Left));

      Assert.That(bangStrategy.BangTwoOnOne(2, 3, 1), Is.EqualTo(Gun.Left));
      Assert.That(bangStrategy.BangTwoOnOne(3, 2, 1), Is.EqualTo(Gun.Right));
    }

    [Test]
    public void TwoOnOne_If_shooter_guns_are_equal_it_doesnt_matter()
    {
      var bangStrategy = new CleverBangStrategy();
      Assert.That(new List<Gun> { Gun.Left, Gun.Right }.Contains(bangStrategy.BangTwoOnOne(1, 1, 3)));
      Assert.That(new List<Gun> { Gun.Left, Gun.Right }.Contains(bangStrategy.BangTwoOnOne(2, 2, 2)));
      Assert.That(new List<Gun> { Gun.Left, Gun.Right }.Contains(bangStrategy.BangTwoOnOne(3, 3, 1)));
    }

    [Test]
    public void TwoOnOne_Minimize_victim_gun()
    {
      var bangStrategy = new CleverBangStrategy();
      Assert.That(bangStrategy.BangTwoOnOne(1, 3, 1), Is.EqualTo(Gun.Left));
      Assert.That(bangStrategy.BangTwoOnOne(3, 1, 1), Is.EqualTo(Gun.Right));
    }



    [Test]
    public void OneOnTwo_Kill_one_gun_if_possible()
    {
      var bangStrategy = new CleverBangStrategy();
      Assert.That(bangStrategy.BangOneOnTwo(1, 3, 4), Is.EqualTo(Gun.Right));
      Assert.That(bangStrategy.BangOneOnTwo(1, 4, 3), Is.EqualTo(Gun.Left));

      Assert.That(bangStrategy.BangOneOnTwo(2, 3, 2), Is.EqualTo(Gun.Left));
      Assert.That(bangStrategy.BangOneOnTwo(2, 2, 3), Is.EqualTo(Gun.Right));
    }

    [Test]
    public void OneOnTwo_If_both_can_be_killed_keep_the_odd_gun_to_avoid_split()
    {
      var bangStrategy = new CleverBangStrategy();
      Assert.That(bangStrategy.BangOneOnTwo(2, 3, 4), Is.EqualTo(Gun.Right));
      Assert.That(bangStrategy.BangOneOnTwo(2, 4, 3), Is.EqualTo(Gun.Left));

      Assert.That(bangStrategy.BangOneOnTwo(3, 2, 3), Is.EqualTo(Gun.Left));
      Assert.That(bangStrategy.BangOneOnTwo(3, 3, 2), Is.EqualTo(Gun.Right));
    }

    [Test]
    public void OneOnTwo_Go_for_smallest_hand()
    {
      var bangStrategy = new CleverBangStrategy();
      Assert.That(bangStrategy.BangOneOnTwo(1, 2, 3), Is.EqualTo(Gun.Left));
      Assert.That(bangStrategy.BangOneOnTwo(1, 3, 2), Is.EqualTo(Gun.Right));

      Assert.That(bangStrategy.BangOneOnTwo(2, 1, 2), Is.EqualTo(Gun.Left));
      Assert.That(bangStrategy.BangOneOnTwo(2, 2, 1), Is.EqualTo(Gun.Right));
    }
  }
}
