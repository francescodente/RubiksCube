using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubiksCube
{
    public class CubeSolution : ICollection<AlgorithmViewPair>
    {
        private List<AlgorithmViewPair> _pairs;

        public CubeSolution()
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

        }
    }
}
