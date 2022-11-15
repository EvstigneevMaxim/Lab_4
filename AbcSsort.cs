using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgsLab4
{
	class GFG
	{

		// Вспомогательная функция для получения ASCII
		// значение символа с индексом d
		// в строке
		static int char_at(string str, int d)
		{
			if (str.Length <= d)
				return -1;
			else
				return (int)(str[d]);
		}

		// Функция для сортировки массива с помощью
		// MSD Radix Sort рекурсивно
		static void MSD_sort(string[] str, int lower, int hiest, int d)
		{
			// Условие рекурсивного прерывания
			if (hiest <= lower)
			{
				return;
			}

			// Хранит значения ASCII
			int[] count = new int[256 + 1];

			// Temp создается для того, чтобы легко
			// поменять местами строки в []str
			Dictionary<int, string> temp = new Dictionary<int, string>();

			// Сохраняет вхождения наиболее значимого
			// символа из каждой строки в []count

			for (int i = lower; i <= hiest; i++)
			{
				int c = char_at(str[i], d);
				count[i]++;
			}

			// Изменяет []count так, чтобы []count
			// теперь содержит фактическую позицию
			// из этих цифр в []temp
			for (int r = 0; r < 256; r++)
				count[r + 1] += count[r];

			// Создаёт переменную
			for (int i = lower; i <= hiest; i++)
			{
				int c = char_at(str[i], d);
				temp.Add(count[i]++, str[i]);
			}

			// Копирует все строки temp в []str,
			// так что []str теперь содержит
			// частично отсортированные строки
			for (int i = lower; i <= hiest; i++)
				str[i] = temp[i - lower];

			// Рекурсивно MSD_sort() на каждом
			// частично отсортированные строки, установленные в
			// отсортируйте их по следующему символу
			for (int r = 0; r < 256; r++)
				MSD_sort(str, lower + count[r],
							lower + count[r + 1] - 1,
							d + 1);
		}

		// Функция для печати массива
		static void print(string[] str, int n)
		{
			for (int i = 0; i < n; i++)
			{
				Console.Write(str[i] + " ");
			}
			Console.WriteLine();
		}

		public static void Main1(string[] args)
		{
			string[] str = { "midnight", "badge", "bag",
					"worker", "banner", "wander" };

			int n = str.Length;

			Console.Write("Unsorted array : ");

			print(str, n);

			MSD_sort(str, 0, n - 1, 0);

			Console.Write("Sorted array : ");

			print(str, n);
		}
	}
}

