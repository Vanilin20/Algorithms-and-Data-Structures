using System;

class Program
{
    public static int comparisons = 0;

    static void Main()
    {
        int[] data = { 42, 8, 60, 19, 3, 55, 12, 31, 68, 24, 49, 37, 71, 5, 27 };

        int[] sortedData = (int[])data.Clone();
        Array.Sort(sortedData);

        Report("linear", sortedData, 3);
        Report("binary", sortedData, 3);

        Report("linear", sortedData, 71);
        Report("binary", sortedData, 71);

        Report("linear", sortedData, 31);
        Report("binary", sortedData, 31);

        Report("linear", sortedData, 1);
        Report("binary", sortedData, 1);

        Report("linear", sortedData, 99);
        Report("binary", sortedData, 99);

        Report("linear", sortedData, 50);
        Report("binary", sortedData, 50);

        Report("linear", new int[] { 42 }, 42);
        Report("binary", new int[] { 42 }, 42);

        Report("linear", new int[] { }, 42);
        Report("binary", new int[] { }, 42);

        Report("binary", data, 55);
    }

    public static int LinearSearch(int[] items, int target)
    {
        for (int i = 0; i < items.Length; i++)
        {
            comparisons++;
            if (items[i] == target)
            {
                return i;
            }
        }
        return -1;
    }

    public static int BinarySearch(int[] items, int target)
    {
        int low = 0;
        int high = items.Length - 1;

        while (low <= high)
        {
            int mid = (low + high) / 2;
            comparisons++;

            if (items[mid] == target)
            {
                return mid;
            }
            else if (items[mid] < target)
            {
                low = mid + 1;
            }
            else
            {
                high = mid - 1;
            }
        }

        return -1;
    }

    public static void Report(string name, int[] items, int target)
    {
        comparisons = 0;
        int index = (name == "linear") 
            ? LinearSearch(items, target) 
            : BinarySearch(items, target);

        Console.WriteLine($"{name,-7} | target: {target,-2} | index: {index,-2} | comparisons: {comparisons}");
    }
}