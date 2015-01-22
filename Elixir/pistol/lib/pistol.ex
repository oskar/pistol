require Integer

defmodule Pistol do
  def bang([left, right], victim) do
    cond do
      # Kill if possible
      left + victim >= 5 -> :left
      right + victim >= 5 -> :right

      # Cannot kill victim, try to make victim hand of odd size to avoid split
      Integer.is_odd(left + victim) -> :left
      Integer.is_odd(right + victim) -> :right

      # If shooter hands are equal, it doesn't matter which we use (go with left)
      left == right -> :left

      true -> :nil
    end
  end

  def bang(shooter, [left, right]) do
    cond do
      # TODO: If possible to kill both hands, kill the one with odd number
      # Kill one hand if possible
      shooter + left >= 5 -> :left
      shooter + right >= 5 -> :right
    end
  end
end