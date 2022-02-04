using System;

class Test {
	public static void arrayToDifferent(int[] chips, int count) {
		int summ = 0;
		for (int i = 0; i < count; i++) {
			summ += chips[i];
		}
		if (summ % count != 0) {
			Console.WriteLine("Chips number are not enough for every players");
			//std::cout << "Chips number are not enough for every players\n";
			Environment.Exit(1);
		}
		int avg = summ / count;
		for (int i = 0; i < count; i++) {
			chips[i] -= avg;
		}
	}

	public static int MaxPos(int[] chips, int count) {
		int max = chips[0];
		int maxPos = 0;
		for (int i = 1; i < count; i++)
		if (chips[i] > max) {
			max = chips[i];
			maxPos = i;
		}
		return maxPos;
	}
	public static int LeftOrRight(int[] chips, int size, int pos, int count = -1) {
		if (count == 0) return 0;
		if (count < 0) count = size - 2;
		int left = 0,
		right = 0;
		for (int i = 0; i < count; i++) {
			right += chips[(pos + 1 + i) % size];
			left += chips[(size + pos - 1 - i) % size];
		}
		if (left == right) return LeftOrRight(chips, size, pos, count - 1);
		return left - right; //-left +right 0-anywhere
	}
	public static int move(int[] chips, int size, int pos, int direction) {
		int newPos = pos;
		direction = direction < 0 ? -1 : 1;

		for (int i = 0; i < size; i++) {
			newPos = (newPos + size + direction) % size;
			if (chips[newPos] < 0) {
				chips[newPos]++;
				chips[pos]--;
				return i + 1;
			}
		}
		return 0;
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
			//std::cout << "0";
			return;
		}
		arrayToDifferent(chips, count);

		if (count == 2) {
			Console.WriteLine(Math.Abs(chips[0]));
			//std::cout << abs(chips[0]);      
			return;
		}

		int moves = 0;
		while (true) {
			int maxPos = MaxPos(chips, count);
			if (chips[maxPos] == 0) {
				Console.WriteLine(moves);
				//std::cout << moves <<"\n";
				break;
			}
			int direction = LeftOrRight(chips, count, maxPos);
			int oneMove = move(chips, count, maxPos, direction);
			moves += oneMove;
		}
		return;

	}
}
