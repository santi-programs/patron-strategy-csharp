// ============================================================
// Trabajo Práctico: Patrón Strategy
// Alumno: Santino Loray
// Materia: Trabajo de Diploma
// Universidad Abierta Interamericana (UAI)
// ============================================================
// Código de Ejemplo del Mundo Real - Patrón Strategy
// Fuente: https://www.dofactory.com/net/strategy-design-pattern
// ============================================================

using System;
using System.Collections.Generic;

namespace DoFactory.GangOfFour.Strategy.RealWorld
{
    /// <summary>
    /// MainApp startup class for Real-World
    /// Strategy Design Pattern.
    /// </summary>
    class MainApp
    {
        /// <summary>
        /// Entry point into console application.
        /// </summary>
        static void Main()
        {
            // Two contexts following different strategies

            SortedList studentRecords = new SortedList();

            studentRecords.Add("Samual");
            studentRecords.Add("Jimmy");
            studentRecords.Add("Sandra");
            studentRecords.Add("Dave");
            studentRecords.Add("Christopher");

            studentRecords.SetSortStrategy(new QuickSort());
            studentRecords.Sort();

            studentRecords.SetSortStrategy(new ShellSort());
            studentRecords.Sort();

            studentRecords.SetSortStrategy(new MergeSort());
            studentRecords.Sort();

            // Wait for user
            Console.ReadKey();
        }
    }

    /// <summary>
    /// The 'Strategy' abstract class
    /// </summary>
    abstract class SortStrategy
    {
        public abstract void Sort(List<string> list);
    }

    /// <summary>
    /// A 'ConcreteStrategy' class
    /// </summary>
    class QuickSort : SortStrategy
    {
        public override void Sort(List<string> list)
        {
            list.Sort(); // Default sort acts as QuickSort
            Console.WriteLine("QuickSorted list ");
        }
    }

    /// <summary>
    /// A 'ConcreteStrategy' class
    /// </summary>
    class ShellSort : SortStrategy
    {
        public override void Sort(List<string> list)
        {
            // Shell sort implementation
            int n = list.Count;
            int gap = n / 2;

            while (gap > 0)
            {
                for (int i = gap; i < n; i++)
                {
                    string temp = list[i];
                    int j = i;

                    while (j >= gap && string.Compare(list[j - gap], temp) > 0)
                    {
                        list[j] = list[j - gap];
                        j -= gap;
                    }
                    list[j] = temp;
                }
                gap /= 2;
            }
            Console.WriteLine("ShellSorted list ");
        }
    }

    /// <summary>
    /// A 'ConcreteStrategy' class
    /// </summary>
    class MergeSort : SortStrategy
    {
        public override void Sort(List<string> list)
        {
            // Merge sort implementation
            if (list.Count <= 1)
                return;

            MergeSortHelper(list, 0, list.Count - 1);
            Console.WriteLine("MergeSorted list ");
        }

        private void MergeSortHelper(List<string> list, int left, int right)
        {
            if (left < right)
            {
                int mid = (left + right) / 2;
                MergeSortHelper(list, left, mid);
                MergeSortHelper(list, mid + 1, right);
                Merge(list, left, mid, right);
            }
        }

        private void Merge(List<string> list, int left, int mid, int right)
        {
            List<string> leftPart = new List<string>();
            List<string> rightPart = new List<string>();

            for (int i = left; i <= mid; i++)
                leftPart.Add(list[i]);
            for (int i = mid + 1; i <= right; i++)
                rightPart.Add(list[i]);

            int l = 0, r = 0, k = left;

            while (l < leftPart.Count && r < rightPart.Count)
            {
                if (string.Compare(leftPart[l], rightPart[r]) <= 0)
                    list[k++] = leftPart[l++];
                else
                    list[k++] = rightPart[r++];
            }

            while (l < leftPart.Count) list[k++] = leftPart[l++];
            while (r < rightPart.Count) list[k++] = rightPart[r++];
        }
    }

    /// <summary>
    /// The 'Context' class
    /// </summary>
    class SortedList
    {
        private List<string> _list = new List<string>();
        private SortStrategy _sortStrategy;

        public void SetSortStrategy(SortStrategy sortStrategy)
        {
            this._sortStrategy = sortStrategy;
        }

        public void Add(string name)
        {
            _list.Add(name);
        }

        public void Sort()
        {
            _sortStrategy.Sort(_list);

            // Iterate over list and display results
            foreach (string name in _list)
            {
                Console.WriteLine(" " + name);
            }
            Console.WriteLine();
        }
    }
}
