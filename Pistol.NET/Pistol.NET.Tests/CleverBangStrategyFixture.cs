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
    public void TwoOnOne_Make_victim_odd_size_to_avoid_split()
    {
      var bangStrategy = new CleverBangStrategy();

      Assert.That(bangStrategy.BangTwoOnOne(1, 2, 1), Is.EqualTo(Gun.Right));
      Assert.That(bangStrategy.BangTwoOnOne(2, 1, 1), Is.EqualTo(Gun.Left));

      Assert.That(bangStrategy.BangTwoOnOne(2, 3, 1), Is.EqualTo(Gun.Left));
      Assert.That(bangStrategy.BangTwoOnOne(3, 2, 1), Is.EqualTo(Gun.Right));
    }

    [Test]
    public void TwoOnOne_If_shooter_hands_are_equal_it_doesnt_matter()
    {
      var bangStrategy = new CleverBangStrategy();
      Assert.That(new List<Gun> { Gun.Left, Gun.Right }.Contains(bangStrategy.BangTwoOnOne(1, 1, 3)));
      Assert.That(new List<Gun> { Gun.Left, Gun.Right }.Contains(bangStrategy.BangTwoOnOne(2, 2, 2)));
      Assert.That(new List<Gun> { Gun.Left, Gun.Right }.Contains(bangStrategy.BangTwoOnOne(3, 3, 1)));
    }

    [Test]
    public void TwoOnOne_Minimize_victim_hand()
    {
      var bangStrategy = new CleverBangStrategy();
      Assert.That(bangStrategy.BangTwoOnOne(1, 3, 1), Is.EqualTo(Gun.Left));
      Assert.That(bangStrategy.BangTwoOnOne(3, 1, 1), Is.EqualTo(Gun.Right));
    }

    /*

  test "Two-on-one: Minimize victim hand" do
    assert Pistol.bang([1, 3], 1) == :left
    assert Pistol.bang([3, 1], 1) == :right
  end



  test "One-on-two: Kill one hand if possible" do
    assert Pistol.bang(1, [3, 4]) == :right
    assert Pistol.bang(1, [4, 3]) == :left

    assert Pistol.bang(2, [3, 2]) == :left
    assert Pistol.bang(2, [2, 3]) == :right
  end

  test "One-on-two: If both can be killed keep the odd hand to avoid split" do
    assert Pistol.bang(2, [3, 4]) == :right
    assert Pistol.bang(2, [4, 3]) == :left

    assert Pistol.bang(3, [2, 3]) == :left
    assert Pistol.bang(3, [3, 2]) == :right
  end

  test "One-on-two: Go for smallest hand" do
    assert Pistol.bang(1, [2, 3]) == :left
    assert Pistol.bang(1, [3, 2]) == :right

    assert Pistol.bang(2, [1, 2]) == :left
    assert Pistol.bang(2, [2, 1]) == :right
  end
     */
  }
}
