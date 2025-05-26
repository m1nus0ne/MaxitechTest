namespace MaxitechTest;

public static class SortingAlgorithms
{
    public static string QuickSort(string input)
    {
        if (string.IsNullOrEmpty(input)) return input;
        var array = input.ToCharArray();
        QuickSort(array, 0, array.Length - 1);
        return new string(array);
    }

    private static void QuickSort(char[] array, int left, int right)
    {
        if (left < right)
        {
            int pivotIndex = Partition(array, left, right);
            QuickSort(array, left, pivotIndex - 1);
            QuickSort(array, pivotIndex + 1, right);
        }
    }

    private static int Partition(char[] array, int left, int right)
    {
        var pivot = array[right];
        var i = left - 1;

        for (int j = left; j < right; j++)
        {
            if (array[j] <= pivot)
            {
                i++;
                (array[i], array[j]) = (array[j], array[i]);
            }
        }

        (array[i + 1], array[right]) = (array[right], array[i + 1]);
        return i + 1;
    }

    public static string TreeSort(string input)
    {
        if (string.IsNullOrEmpty(input)) return input;

        var root = new Node(input[0]);
        for (int i = 1; i < input.Length; i++)
        {
            root.Insert(input[i]);
        }

        var sortedList = new List<char>();
        root.InOrderTraversal(sortedList);
        return new string(sortedList.ToArray());
    }
}