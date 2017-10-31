using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Utilities
{
    public class SortedQueue<T>
    {
        private List<T> _sortedList;
        private IComparer<T> _comparer;

        internal SortedQueue(IEnumerable<T> ienumerable, IComparer<T> comparer)
        {
           _comparer = comparer;
           _sortedList = ienumerable.ToList();
           _sortedList.Sort(comparer);
        }

        internal void Enqueue(T item)
        {
            for (int i = 0; i < _sortedList.Count; i++)
            {
                if (_comparer.Compare(item, _sortedList[i]) < 0)
                {
                    _sortedList.Insert(i, item);
                    return;
                }
            }
            _sortedList.Add(item);
 
        }

        internal T Dequeue()
        {
            T firstItem = _sortedList[0];
            _sortedList.RemoveAt(0);
            return firstItem;
        }

        internal bool Exists(Predicate<T>predicate)
        {
           return  _sortedList.Exists(predicate);
        }

        public override String ToString()
        {
            StringBuilder sb = new StringBuilder("Sorted String: ");
            foreach(T item in _sortedList)
            {
                sb.AppendFormat("{0} ", item.ToString());
            }
            return sb.ToString();
        }
    }
}
