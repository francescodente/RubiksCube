using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubiksCube
{
    public class AlgorithmCollection : ICollection<AlgorithmViewPair>
    {
        private List<AlgorithmViewPair> _pairs;

        public AlgorithmCollection()
        {
            _pairs = new List<AlgorithmViewPair>();
        }

        public void Add(AlgorithmViewPair item)
        {
            _pairs.Add(item);
        }

        public void Clear()
        {
            _pairs.Clear();
        }

        public bool Contains(AlgorithmViewPair item)
        {
            return _pairs.Contains(item);
        }

        public void CopyTo(AlgorithmViewPair[] array, int arrayIndex)
        {
            _pairs.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return _pairs.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(AlgorithmViewPair item)
        {
            return _pairs.Remove(item);
        }

        public IEnumerator<AlgorithmViewPair> GetEnumerator()
        {
            return _pairs.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public void Optimize()
        {
            foreach (AlgorithmViewPair pair in _pairs)
                for (int i = 0; i < pair.Algorithm.Count; i++)
                {
                    if (i < pair.Algorithm.Count - 1 && (int)pair.Algorithm[i] == -(int)pair.Algorithm[i + 1])
                    {
                        pair.Algorithm.RemoveAt(i);
                        pair.Algorithm.RemoveAt(i);
                        i--;
                    }
                    else if (i < pair.Algorithm.Count - 2 && pair.Algorithm[i] == pair.Algorithm[i + 1] && pair.Algorithm[i] == pair.Algorithm[i + 2])
                    {
                        pair.Algorithm[i] = (Move)(-(int)pair.Algorithm[i]);
                        pair.Algorithm.RemoveAt(i + 1);
                        pair.Algorithm.RemoveAt(i + 1);
                        i--;
                    }
                }

            for (int i = 0; i < _pairs.Count; i++)
            {
                if (_pairs[i].Algorithm.Count <= 0)
                    _pairs.RemoveAt(i);

                i--;
            }
        }
    }
}
