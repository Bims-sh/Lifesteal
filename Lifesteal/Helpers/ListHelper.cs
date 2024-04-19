namespace Lifesteal.Helpers;

public class ListHelper
{
    public static void ShuffleList<T>(IList<T> list)
    {
        var random = new Random();
        var n = list.Count;
        while (n > 1)
        {
            n--;
            var k = random.Next(n + 1);
            (list[k], list[n]) = (list[n], list[k]);
        }
    }
    
    public static T GetRandomItem<T>(IReadOnlyList<T> itemList)
    {
        var random = new Random();
        if (itemList.Count <= 0) return default!;

        var randomIndex = random.Next(0, itemList.Count);
        return itemList[randomIndex];
    }
}