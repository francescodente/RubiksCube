using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubiksCube
{
    /// <summary>
    /// Represents an ordered collection of algorithm-view pairs.
    /// </summary>
    public class AlgorithmCollection : ICollection<AlgorithmViewPair>
    {
        private List<AlgorithmViewPair> _pairs;

        /// <summary>
        /// Initializes a new instance of the <see cref="AlgorithmCollection"/> class.
        /// </summary>
        public AlgorithmCollection()
        {
            _pairs = new List<AlgorithmViewPair>();
        }

        /// <summary>
        /// Adds a new <see cref="AlgorithmViewPair"/> item to the collection.
        /// </summary>
        /// <param name="item">Item to be added.</param>
        public void Add(AlgorithmViewPair item)
        {
            _pairs.Add(item);
        }

        /// <summary>
        /// Adds a new algotithm and its view to the collection
        /// </summary>
        /// <param name="algorithm">Algorithm.</param>
        /// <param name="view">View.</param>
        public void Add(IEnumerable<Move> algorithm, CubeView view)
        {
            _pairs.Add(new AlgorithmViewPair(algorithm, view));
        }

        /// <summary>
        /// Removes all items from the collection.
        /// </summary>
        public void Clear()
        {
            _pairs.Clear();
        }

        /// <summary>
        /// Returns a value that indicates if an item is contained in the collection.
        /// </summary>
        /// <param name="item">Item.</param>
        /// <returns>True or false.</returns>
        public bool Contains(AlgorithmViewPair item)
        {
            return _pairs.Contains(item);
        }

        /// <summary>
        /// Copies the collection into a one-dimension array at a specified position.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <param name="arrayIndex">The position.</param>
        public void CopyTo(AlgorithmViewPair[] array, int arrayIndex)
        {
            _pairs.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Gets the number of items in the collection.
        /// </summary>
        public int Count
        {
            get { return _pairs.Count; }
        }

        /// <summary>
        /// Gets a value that indicates if the collection is read-only or not.
        /// </summary>
        public bool IsReadOnly
        {
            get { return false; }
        }

        /// <summary>
        /// Removes the specified item from the collection.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>True if the element was actually removed, false otherwise.</returns>
        public bool Remove(AlgorithmViewPair item)
        {
            return _pairs.Remove(item);
        }

        /// <summary>
        /// Gets the enumerator of the collection.
        /// </summary>
        /// <returns>The enumerator.</returns>
        public IEnumerator<AlgorithmViewPair> GetEnumerator()
        {
            return _pairs.GetEnumerator();
        }

        /// <summary>
        /// Gets the enumerator of the collection.
        /// </summary>
        /// <returns>The enumerator.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary>
        /// Optimizes the algorithm by removing complementary moves and repeated moves.
        /// </summary>
        public void Optimize()
        {
            foreach (AlgorithmViewPair pair in _pairs)
                for (int i = 0; i < pair.Algorithm.Count; i++)
                {
                    if (i < pair.Algorithm.Count - 1 && (int)pair.Algorithm[i] == -(int)pair.Algorithm[i + 1])
                    {
                        // Removes two moves if they are complementary.
                        pair.Algorithm.RemoveAt(i);
                        pair.Algorithm.RemoveAt(i);
                        i--;
                    }
                    else if (i < pair.Algorithm.Count - 2 && pair.Algorithm[i] == pair.Algorithm[i + 1] && pair.Algorithm[i] == pair.Algorithm[i + 2])
                    {
                        // Changes three repeated moves to one complementary move.
                        pair.Algorithm[i] = (Move)(-(int)pair.Algorithm[i]);
                        pair.Algorithm.RemoveAt(i + 1);
                        pair.Algorithm.RemoveAt(i + 1);
                        i--;
                    }
                }

            // Removes all the pairs whose algorithm is empty.
            for (int i = 0; i < _pairs.Count; i++)
            {
                if (_pairs[i].Algorithm.Count <= 0)
                {
                    _pairs.RemoveAt(i);
                    i--;
                }
            }
        }
    }
}
