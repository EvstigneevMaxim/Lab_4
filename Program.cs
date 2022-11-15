using Algorithm;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

/*
 * Пусть имеется текст, состоящий из слов. Необходимо разбить текст на отдельные слова и провести их сортировку в лексиграфическом порядке. +
 * Причем одинаковые слова будут в отсорториванной последовательности идти друг за другом. Надо выбрать два алгоритма сортировки, +
 * один из которых имеет оценку времени работы О(N2), другой ещё более эффективный алгоритм именно для строк. +
 * 
 * После получения отсортированного массива необходимо пройтись по нему и для каждого уникального слова пдсчитатть сколько раз оно встречается. +
 * Результаты вывести на экран +
 * 
 * Далее необходимо провести эксперименты с использованием обоих алгоритмов по сортировке текстов различной длинны (100, 500, 1000, 2000, 5000 слов или более).
 * Причём для каждого эксперимента произвести замеры времени сортировки. Получившиеся данные оформить в таблицу.
 * 
 * Тексты использовать на английском
 */


namespace AlgsLab4
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] arr = ReadData();

            /*Console.WriteLine("BEFORE:");
            for (int i = 0; i < arr.Length; i++) Console.WriteLine("{0}. {1}", i + 1, arr[i]);*/

            Stopwatch bubbleTime = new Stopwatch();
            
            bubbleTime.Start();
            BubbleSortString(arr);
            bubbleTime.Stop();

            Console.WriteLine($"BubbleSort: \nTime in Milliseconds:\t{bubbleTime.ElapsedMilliseconds}");


            /*string[] arr1 = ReadData();

            Stopwatch quickTime = new();

            quickTime.Start();
            
            QuickSort(arr1, 0, arr1.Length);

            quickTime.Stop();

            Console.WriteLine($"QuickSort:\nTime in Milliseconds:\t{quickTime.ElapsedMilliseconds}");*/

            /*Console.WriteLine();
            Console.WriteLine("AFTER:");
            for (int i = 0; i < arr.Length; i++) Console.WriteLine("{0}. {1}", i + 1, arr[i]);*/
            //Console.ReadKey();

            Dictionary<string, int> valuePairs = GetWordRepeatsCount(arr1);
            foreach (var pair in valuePairs)
            {
                if (pair.Value > 1)
                    Console.WriteLine($"Уникальное слово:{pair.Key}, количество его повторений:{pair.Value}");
                else
                    continue;
            }
            Console.WriteLine($"Количество повторений остальных слов: 1");*/

        }

        public static string[] ReadData()
        {
            string path = @$"{Environment.CurrentDirectory}\Example_100.txt";
            FileInfo file1 = new FileInfo(path);
            string[] words = null;
            using (StreamReader streamRead = new StreamReader(file1.OpenRead()))
            {
                string text = File.ReadAllText(path);
                words = text.Split(new string[] { " ", "\r\n", "\t", ",", ".", "(", ")", "\"", "-", "—", ";", ":" },
                    StringSplitOptions.RemoveEmptyEntries);
            }
            return words;
        }

        static void QuickSort(string[] a, int l, int r)
        {
            string temp;
            var x = a[l + (r - l) / 2];
            int i = l;
            int j = r-1;

            while (i <= j)
            {
                while (string.CompareOrdinal(a[i].ToLower(), x.ToLower()) < 0) i++;
                while (string.CompareOrdinal(a[j].ToLower(), x.ToLower()) > 0) j--;
                if (i <= j)
                {
                    temp = a[i];
                    a[i] = a[j];
                    a[j] = temp;
                    i++;
                    j--;
                }
            }
            if (i < r)
                QuickSort(a, i, r);

            if (l < j)
                QuickSort(a, l, j);
        }

        static void BubbleSortString(string[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                for (int j = 0; j < arr.Length - 1; j++)
                {
                    if (NeedToReOrder(arr[j], arr[j + 1]))
                    {
                        Swap(ref arr[j], ref arr[j + 1]);
                        /*string s = arr[j];
                        arr[j] = arr[j + 1];
                        arr[j + 1] = s;*/
                    }
                }
            }
        }

        protected static bool NeedToReOrder(string s1, string s2)
        {
            for (int i = 0; i < (s1.Length > s2.Length ? s2.Length : s1.Length); i++)
            {
                if (s1.ToLower().ToCharArray()[i] < s2.ToLower().ToCharArray()[i]) return false;
                if (s1.ToLower().ToCharArray()[i] > s2.ToLower().ToCharArray()[i]) return true;
            }
            return false;
        }

        static void Swap(ref string element1, ref string element2)
        {
            var temp = element1;
            element1 = element2;
            element2 = temp;
        }

        /* 
         * После получения отсортированного массива необходимо пройтись по нему и для каждого уникального слова подсчитать, сколько раз оно встречается.
         * Результаты вывести на экран
         */
        public static Dictionary<string, int> GetWordRepeatsCount(string[] sortedArray)
        {
            var wordRepeats = new Dictionary<string, int>();
            for (int i = 0; i < sortedArray.Length; i++)
            {
                if (wordRepeats.ContainsKey(sortedArray[i]))
                {
                    wordRepeats[sortedArray[i]]++;
                }
                else
                {
                    wordRepeats.Add(sortedArray[i], 1);
                }
            }

            return wordRepeats;
        }
    }
}
