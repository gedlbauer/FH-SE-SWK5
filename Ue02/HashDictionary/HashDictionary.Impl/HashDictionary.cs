using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace HashDictionary.Impl
{
    public class HashDictionary<K, V> : IDictionary<K, V>
    {
        private const int DEFAULT_SIZE = 8;

        #region inner classes
        private class Node
        {
            public K Key { get; set; }
            public V Value { get; set; }
            public Node Next { get; set; }
        }
        #endregion inner classes

        #region internal values
        private Node[] ht = new Node[DEFAULT_SIZE];
        private int size;
        #endregion internal values

        #region helper methods

        private Node FindNode(K key)
        {
            for (Node n = ht[IndexFor(key)]; n != null; n = n.Next)
            {
                if (n.Key.Equals(key))
                    return n;
            }
            return null;
        }

        private int IndexFor(K key)
        {
            return IndexFor(key, ht.Length);
        }

        private int IndexFor(K key, int tableLength)
        {
            return Math.Abs(key.GetHashCode()) % tableLength;
        }

        private bool Add(K key, V value, out Node node)
        {
            node = FindNode(key);
            if (node is not null)
            {
                return false; // key already exists
            }
            var index = IndexFor(key);
            node = new Node { Key = key, Value = value, Next = ht[index] };
            ht[index] = node;
            ++size;
            return true;
        }

        #endregion helper methods


        public V this[K key]
        {
            get
            {
                var node = FindNode(key);
                if (node is null)
                {
                    throw new KeyNotFoundException();
                }
                return node.Value;
            }

            set
            {
                if (!Add(key, value, out Node node))
                {
                    node.Value = value;
                }
            }
        }

        public ICollection<K> Keys => this.Select(x => x.Key).ToList();

        public ICollection<V> Values => this.Select(x => x.Value).ToList();

        public int Count => size;

        public bool IsReadOnly => false;

        public void Add(K key, V value)
        {
            if (!Add(key, value, out _))
            {
                throw new ArgumentException("Item has already benn added");
            }
        }

        public void Add(KeyValuePair<K, V> item)
        {
            Add(item.Key, item.Value);
        }

        public void Clear()
        {
            for (int i = 0; i < ht.Length; i++)
            {
                ht[i] = null;
            }
            size = 0;
        }

        public bool Contains(KeyValuePair<K, V> item)
        {
            return ContainsKey(item.Key); // technisch falsch, value könnte unterschiedlich sein, trotzdem true
        }

        public bool ContainsKey(K key) => FindNode(key) is not null;

        public void CopyTo(KeyValuePair<K, V>[] array, int arrayIndex)
        {
            foreach (var pair in this)
            {
                array[arrayIndex++] = pair;
            }
        }

        public IEnumerator<KeyValuePair<K, V>> GetEnumerator()
        {
            for (int i = 0; i < ht.Length; i++)
            {

                for (var node = ht[i]; node is not null; node = node.Next)
                {
                    yield return new KeyValuePair<K, V>(node.Key, node.Value);
                }
            }
        }

        public bool Remove(K key)
        {
            var index = IndexFor(key);
            Node prev = null;
            for (var node = ht[index]; node is not null; node = node.Next)
            {
                if(node.Key.Equals(key))
                {
                    if(prev is not null)
                    {
                        prev.Next = node.Next;
                    } else
                    {
                        ht[index] = node.Next;
                    }
                    size--;
                    return true;
                }
                prev = node;
            }
            return false;
        }

        public bool Remove(KeyValuePair<K, V> item)
        {
            return Remove(item.Key);
        }

        public bool TryGetValue(K key, [MaybeNullWhen(false)] out V value)
        {
            Node node = FindNode(key);
            value = node is not null ? node.Value : default(V);
            return node is not null;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
