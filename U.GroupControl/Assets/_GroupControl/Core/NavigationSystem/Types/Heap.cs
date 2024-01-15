using System;
using System.Collections.Generic;

namespace _GroupControl.Core.NavigationSystem.Types
{
    public class Heap<T> where T : IComparable<T>
    {
        private List<T> items = new List<T>();

        private int LeftNode(int index) => (index + 1) * 2 - 1;
        private int RightNode(int index) => (index + 1) * 2;
        private int ParentNode(int index) => (index + 1) / 2 - 1;

        private void Swap(int index1, int index2) =>
            (items[index1], items[index2]) = (items[index2], items[index1]);

        private void BuildMinHeap()
        {
            for (int i = items.Count / 2; i >= 0; i--)
                MinHeapify(i);
        }

        private void MinHeapify(int index)
        {
            int left = LeftNode(index), right = RightNode(index);
            int smallest = left < items.Count && items[left].CompareTo(items[index]) > -1 ? left : index;
            if (right < items.Count && items[right].CompareTo(items[smallest]) > -1)
                smallest = right;

            if (smallest != index)
            {
                Swap(index, smallest);
                MinHeapify(smallest);
            }
        }

        public void Add(T item)
        {
            items.Add(item);
            BuildMinHeap();
        }

        public T Min() => items.Count == 0 ? throw new InvalidOperationException("Heap is empty") : items[0];

        public T Pop()
        {
            if (items.Count == 0)
                throw new InvalidOperationException("Heap is empty");

            T temp = items[0];
            items[0] = items[items.Count - 1];
            items.RemoveAt(items.Count - 1);
            BuildMinHeap();
            return temp;
        }

        public int Count => items.Count;
        public bool Contains(T item) => items.Contains(item);
    }
}