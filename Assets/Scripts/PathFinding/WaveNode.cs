using System.Collections.Generic;
using System.Linq;
using TreeEditor;

namespace pathFinding
{
    public class WaveNode<T> where T : class
    {
        public WaveNode<T> Father { get; private set; }
        public readonly T Item;
        public readonly bool IsRoot;
        public int Distance { get; protected set; }

        protected readonly List<WaveNode<T>> Children;

        public WaveNode(T item, int distance = 0, WaveNode<T> father = null)
        {
            if (father == null)
                IsRoot = true;
            else
                Father = father;

            Item = item;
            Children = new List<WaveNode<T>>();
            Distance = distance;
        }

        public void AddChild(WaveNode<T> child) => Children.Add(child);

        public IEnumerable<WaveNode<T>> GetTails()
        {
            var tail = new List<WaveNode<T>>();

            if (Children.Count > 0)
                for (int i = 0; i < Children.Count; i++)
                    tail.AddRange(Children[i].GetTails());
            else
                tail.Add(this);

            return tail;
        }

        public IReadOnlyList<WaveNode<T>> GetPath()
        {
            var result = new List<WaveNode<T>>();
            if (!IsRoot)
                result.AddRange(Father.GetPath());
            result.Add(this);
            return result;
        }

        public WaveNode<T> Get(T item)
        {
            if (item == Item)
                return this;

            if (Children.Count == 0)
                return null;

            foreach (var child in Children)
            {
                if (child.Item == item)
                    return child;

                WaveNode<T> result = child.Get(item);
                if (result != null)
                    return result;
            }
            return default;
        }

        public void UpdateFather(WaveNode<T> father, int distanceBetweenCells)
        {
            Father.RemoveChind(this);
            Father = father;
            Father.AddChild(this);
            UpdateDistance(Father.Distance + distanceBetweenCells);
        }

        private void UpdateDistance(int value)
        {
            foreach (var child in Children)
            {
                int distanceBetweenCells = Distance - child.Distance;
                child.UpdateDistance(value + distanceBetweenCells);
            }
            Distance = value;
        }

        private void RemoveChind(WaveNode<T> child) => Children.Remove(child);
    }
}