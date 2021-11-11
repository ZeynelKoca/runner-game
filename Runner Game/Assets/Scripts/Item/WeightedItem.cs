public class WeightedItem
{
    public Item ItemType { get; }
    public int Weight { get; }

    public WeightedItem(Item itemType, int weight)
    {
        ItemType = itemType;
        Weight = weight;
    }
}