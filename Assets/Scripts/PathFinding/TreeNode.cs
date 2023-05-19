using System.Collections.Generic;
using System.Linq;
using TreeEditor;

namespace pathFinding
{
    public class TreeNode<T>
    {
        public readonly TreeNode<T> Father;
        public readonly T Item;
        public readonly bool IsRoot;

        private readonly List<TreeNode<T>> _children;

        public TreeNode(T item, TreeNode<T> father = null)
        {
            if (father == null)
                IsRoot = true;
            else
                Father = father;

            Item = item;
            _children = new List<TreeNode<T>>();
        }

        public void AddChild(TreeNode<T> child) => _children.Add(child);

        public void RemoveChind(TreeNode<T> child) => _children.Remove(child);

        public IEnumerable<TreeNode<T>> GetTails()
        {
            var tail = new List<TreeNode<T>>();

            if (_children.Count > 0)
                for (int i = 0; i < _children.Count; i++)
                    tail.AddRange(_children[i].GetTails());
            else
                tail.Add(this);

            return tail;
        }

        public IReadOnlyList<TreeNode<T>> GetPath()
        {
            var result = new List<TreeNode<T>>();
            if (!IsRoot)
                result.AddRange(Father.GetPath());
            result.Add(this);
            return result;
        }

        public TreeNode<T> Get(T item)
        {
            if(_children.Count == 0)
                return null;

            foreach (var child in _children)
            {
                if(child.Equals(item))
                    return child;

                TreeNode<T> result = child.Get(item);
                if (result != null)
                    return result;
            }
            throw new System.NullReferenceException();
        }
    }
}