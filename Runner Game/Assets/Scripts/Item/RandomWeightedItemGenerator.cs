using System.Collections.Generic;
using UnityEngine;

public class RandomWeightedItemGenerator
{
    private WeightedItem[] _weightedItems;

    public RandomWeightedItemGenerator(params WeightedItem[] weightedItems)
    {
        _weightedItems = weightedItems;
    }

    public Item GetRandomItem()
    {
        List<Item> list = new List<Item>();
        foreach(var item in _weightedItems)
        {
            for (int i = 0; i < item.Weight; i++)
            {
                list.Add(item.ItemType);
            }
        }

        var randomNumber = Random.Range(0, list.Count);
        return list[randomNumber];
    }
}
