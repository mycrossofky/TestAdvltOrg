using System;

class Program {
	private static int test3(int[] chips) {
		arrayToDifferent(chips);

		int oneMove = 0;
		int moves = 0;
		while (true) {
			for (int i = 0; i < chips.Length; i++) {
				oneMove = nextMove3(chips, i);
				if (oneMove > 0) {
					moves += oneMove;
					break;
				}
			}

			if (oneMove <= 0) { //no Moves
				for (int i = 0; i < chips.Length; i++)
				if (chips[i] > 0) {
					oneMove = move(chips, i, -1); //move left                            
					break;
				}

				if (oneMove == 0) break;
				moves += oneMove;
			}
		};

		return moves;
	}

	private static int nextMove3(int[] chips, int pos) {
		if (chips[pos] <= 0) return 0;
		int left = 0;
		int right = 0;
		int rValue = chips[pos];
		int lValue = rValue;
		for (int i = 1; i < chips.Length; i++) {
			right += Math.Abs(rValue);
			rValue += chips[(pos + i) % chips.Length];
			left += Math.Abs(lValue);
			lValue += chips[(chips.Length + pos - i) % chips.Length];
		}
		if (left != right) {
			return move(chips, pos, left - right);
		}
		return 0;
	}

	private static int move(int[] chips, int pos, int direction) {
		if (chips[pos] <= 0) return 0;
		int newPos = pos;
		direction = direction < 0 ? -1 : 1;

		for (int i = 0; i < chips.Length; i++) {
			newPos = (newPos + chips.Length + direction) % chips.Length;
			if (chips[newPos] < 0) {
				chips[newPos]++;
				chips[pos]--;
				return i + 1;
			}
		}
		return 0;
	}

	private static void arrayToDifferent(int[] chips) {
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
	static void Main() {
		String s = Console.ReadLine();
		int k = s.IndexOf("[");
		s = s.Substring(k + 1, s.IndexOf("]") - k - 1);
		string[] strings = s.Split(',');
		int[] chips = Array.ConvertAll < string,
		int > (strings, int.Parse);
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

		int moves = test3(chips);
		Console.WriteLine(moves);
		return;
	}

}
