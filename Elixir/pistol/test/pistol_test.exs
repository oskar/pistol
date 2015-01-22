defmodule PistolTest do
  use ExUnit.Case

  test "Two-on-one: Choose left if enough to kill" do
    assert Pistol.bang([2, 1], 3) == :left
    assert Pistol.bang([3, 1], 2) == :left
    assert Pistol.bang([4, 1], 2) == :left
    assert Pistol.bang([5, 1], 4) == :left
  end

  test "Two-on-one: Choose right if enough to kill" do
    assert Pistol.bang([1, 2], 3) == :right
    assert Pistol.bang([1, 3], 2) == :right
    assert Pistol.bang([1, 4], 2) == :right
    assert Pistol.bang([1, 5], 3) == :right
  end

  test "Two-on-one: Make victim odd size to avoid split" do
    assert Pistol.bang([1, 2], 2) == :left
    assert Pistol.bang([2, 1], 2) == :right

    assert Pistol.bang([2, 3], 1) == :left
    assert Pistol.bang([3, 2], 1) == :right
  end

  test "Two-on-one: If shooter hands are equal it doesn't matter" do
    assert Enum.member? [:left, :right], Pistol.bang([1, 1], 3)
    assert Enum.member? [:left, :right], Pistol.bang([2, 2], 2)
    assert Enum.member? [:left, :right], Pistol.bang([3, 3], 1)
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
end
