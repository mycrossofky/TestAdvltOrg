using System;

class Program {
  public static void arrayToDifferent(int[] chips  ) {
    int summ = 0;

    for (int i = 0; i < chips.Length; i++) {
      summ += chips[i];
    }
    if (summ % chips.Length != 0) {
      Console.WriteLine("Chips number are not enough for every players");
      Environment.Exit(1);
    }
    int avg = summ / chips.Length;
    for (int i = 0; i < chips.Length; i++) {
      chips[i] -= avg;
    }
  }

  public static int move(int[] chips, int pos, int count, int inc) {

    if (count == 0) return 0;
    int moves = 0;
    chips[pos] = chips[pos] - count;
    for (int i = 0; i < chips.Length; i++) {
      moves += count;
      pos = (pos + chips.Length + inc) % chips.Length;

      if (chips[pos] >= 0) continue;
      while (chips[pos] < 0 && count > 0) {
        chips[pos] = chips[pos] + 1;
        count--;
      }
      if (count == 0) break;
    }
    return moves;
  }
  public static int testFull(int[] chips) {

    int pos;
    for (pos = 0; pos < chips.Length; pos++) {
      if (chips[pos] > 0) break;
    }
    if (pos == chips.Length) return 0;
    int[] c2 = new int[chips.Length];
    int maxMoves = -1;

    for (int left = chips[pos]; left >= 0; left--) {
      int right = chips[pos] - left;
      int moves = 0;
      for (int i = 0; i < chips.Length; i++)
        c2[i] = chips[i];
      moves = move(c2, pos, left, -1);
      moves += move(c2, pos, right, 1);
      if (maxMoves != -1 && moves > maxMoves) continue;
      moves += testFull(c2);

      if (moves < maxMoves || maxMoves == -1)
        maxMoves = moves;

    }
    return maxMoves;
  }

  public static void Main(string[] args) {
    String s = Console.ReadLine();
    int k = s.IndexOf("[");
    s = s.Substring(k + 1, s.IndexOf("]") - k - 1);
    string[] strings = s.Split(',');
    int[] chips = Array.ConvertAll < string, int > (strings, int.Parse);
    int count = chips.Length;

    if (count <= 1) {
      Console.WriteLine(0);
      return;
    }
    arrayToDifferent(chips);

    if (count == 2) {
      Console.WriteLine(Math.Abs(chips[0]));
      return;
    }

    int moves = testFull(chips);
    Console.WriteLine(moves);
    return;

  }
}
