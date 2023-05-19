namespace pathFinding
{
    public class WaveItem<T>
    {
        public readonly T Item;
        public readonly int Distance;

        public WaveItem(T item, int distance)
        {
            Item = item;
            Distance = distance;
        }
    }
}