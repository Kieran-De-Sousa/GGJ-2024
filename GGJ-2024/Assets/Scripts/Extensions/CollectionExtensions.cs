using System;
using System.Collections.Generic;

/// <summary>
/// Some functions to manipulate collections.
/// </summary>
public static class CollectionUtils
{
    private static readonly Random random = new();

    public static T RandomElement<T>(this List<T> list)
    {
        return list[random.Next(0, list.Count)];
    }

    public static void Shuffle<T>(this List<T> list)
    {
        // https://www.geeksforgeeks.org/shuffle-a-given-array-using-fisher-yates-shuffle-algorithm/

        int n = list.Count;
        for (int i = n - 1; i > 0; i--)
        {
            int j = random.Next(0, i + 1);
            T temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
    }
}
