using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace RotaryHeart.Lib.SerializableDictionaryPro
{
    /// <summary>
    /// Base class that most be used for any dictionary that wants to be implemented
    /// </summary>
    /// <typeparam name="TKey">Key type</typeparam>
    /// <typeparam name="TValue">Value type</typeparam>
    [System.Serializable]
    public class SerializableDictionary<TKey, TValue> : DrawableDictionary, IDictionary<TKey, TValue>, ISerializationCallbackReceiver
    {
        public SerializableDictionary(bool canAdd = true, bool canRemove = true, bool canReorder = true, bool readOnly = false)
            : base(canAdd, canRemove, canReorder, readOnly) { }
        
        public SerializableDictionary() : base(true, true, true, false) { }

        Dictionary<TKey, TValue> m_dict;

        public Dictionary<TKey, TValue> Dict
        {
            get
            {
                if (m_dict == null)
                    m_dict = new Dictionary<TKey, TValue>();

                return m_dict;
            }
        }

        /// <summary>
        /// Copies the data from a dictionary. If an entry with the same key is found it replaces the value
        /// </summary>
        /// <param name="src">Dictionary to copy the data from</param>
        public void CopyFrom(IDictionary<TKey, TValue> src)
        {
            foreach (KeyValuePair<TKey, TValue> data in src)
            {
                if (ContainsKey(data.Key))
                {
                    this[data.Key] = data.Value;
                }
                else
                {
                    Add(data.Key, data.Value);
                }
            }
        }

        /// <summary>
        /// Copies the data from a dictionary. If an entry with the same key is found it replaces the value.
        /// Note that if the <paramref name="src"/> is not a dictionary of the same type it will not be copied
        /// </summary>
        /// <param name="src">Dictionary to copy the data from</param>
        public void CopyFrom(object src)
        {
            IDictionary<TKey, TValue> dictionary = src as IDictionary<TKey, TValue>;
            if (dictionary != null)
            {
                CopyFrom(dictionary);
            }
        }

        /// <summary>
        /// Copies the data to a dictionary. If an entry with the same key is found it replaces the value
        /// </summary>
        /// <param name="dest">Dictionary to copy the data to</param>
        public void CopyTo(IDictionary<TKey, TValue> dest)
        {
            foreach (KeyValuePair<TKey, TValue> data in this)
            {
                if (dest.ContainsKey(data.Key))
                {
                    dest[data.Key] = data.Value;
                }
                else
                {
                    dest.Add(data.Key, data.Value);
                }
            }
        }

        /// <summary>
        /// Copies the data to a dictionary. If an entry with the same key is found it replaces the value.
        /// Note that if <paramref name="dest"/> is not a dictionary of the same type it will not be copied
        /// </summary>
        /// <param name="dest">Dictionary to copy the data to</param>
        public void CopyTo(object dest)
        {
            IDictionary<TKey, TValue> dictionary = dest as IDictionary<TKey, TValue>;
            if (dictionary != null)
            {
                CopyTo(dictionary);
            }
        }

        /// <summary>
        /// Returns a copy of the dictionary.
        /// </summary>
        public Dictionary<TKey, TValue> Clone()
        {
            Dictionary<TKey, TValue> dest = new Dictionary<TKey, TValue>(Count);

            foreach (KeyValuePair<TKey, TValue> data in this)
            {
                dest.Add(data.Key, data.Value);
            }

            return dest;
        }

        /// <summary>
        /// Returns true if the value exists; otherwise, false
        /// </summary>
        /// <param name="value">Value to check</param>
        public bool ContainsValue(TValue value)
        {
            return Dict.ContainsValue(value);
        }

        #region IDictionary Interface

        #region Properties

        public TValue this[TKey key]
        {
            get
            {
                return Dict[key];
            }
            set
            {
                Dict[key] = value;
            }
        }

        public ICollection<TKey> Keys
        {
            get
            {
                return Dict.Keys;
            }
        }

        public ICollection<TValue> Values
        {
            get
            {
                return Dict.Values;
            }
        }

        public int Count
        {
            get
            {
                return Dict.Count;
            }
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly
        {
            get { return false; }
        }

        #endregion Properties

#if UNITY_EDITOR
        public void Add(TKey key, TValue value)
        {
            Dict.Add(key, value);

            if (m_keys == null)
                m_keys = new List<TKey>();
            if (m_values == null)
                m_values = new List<TValue>();

            m_keys.Add(key);
            m_values.Add(value);
        }

        public void Clear()
        {
            Dict.Clear();

            if (m_keys != null)
                m_keys.Clear();
            if (m_values != null)
                m_values.Clear();
        }

        public bool Remove(TKey key)
        {
            if (m_keys != null)
            {
                int index = m_keys.IndexOf(key);

                if (index != -1)
                {
                    m_keys.RemoveAt(index);

                    if (m_values != null)
                        m_values.RemoveAt(index);
                }
            }

            return Dict.Remove(key);
        }
#else
        public void Add(TKey key, TValue value)
        {
            Dict.Add(key, value);
        }

        public void Clear()
        {
            Dict.Clear();
        }

        public bool Remove(TKey key)
        {
            return Dict.Remove(key);
        }
#endif

        public bool ContainsKey(TKey key)
        {
            return Dict.ContainsKey(key);
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            return Dict.TryGetValue(key, out value);
        }

        void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)
        {
            (Dict as ICollection<KeyValuePair<TKey, TValue>>).Add(item);
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item)
        {
            return (Dict as ICollection<KeyValuePair<TKey, TValue>>).Contains(item);
        }

        void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            (Dict as ICollection<KeyValuePair<TKey, TValue>>).CopyTo(array, arrayIndex);
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
        {
            return (Dict as ICollection<KeyValuePair<TKey, TValue>>).Remove(item);
        }

        public Dictionary<TKey, TValue>.Enumerator GetEnumerator()
        {
            return Dict.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region ISerializationCallbackReceiver

        [SerializeField, FormerlySerializedAs("_keys")]
        List<TKey> m_keys;
        [SerializeField, FormerlySerializedAs("_values")]
        List<TValue> m_values;

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            if (m_keys == null || m_values == null)
            {
                return;
            }

            //Need to clear the dictionary
            Dict.Clear();

            for (int i = 0; i < m_keys.Count; i++)
            {
                //Key cannot be null, skipping entry
                if (m_keys[i] == null)
                {
                    continue;
                }

                //Add the data to the dictionary. Value can be null so no special step is required
                if (i < m_values.Count)
                {
                    Dict[m_keys[i]] = m_values[i];
                }
                else
                {
                    Dict[m_keys[i]] = default;
                }
            }

            //Outside of editor we clear the arrays to free up memory
#if !UNITY_EDITOR
            m_keys = null;
            m_values = null;
#endif
        }
        
        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            if (!Application.isPlaying)
            {
                return;
            }
            
            if (Dict.Count == 0)
            {
                //Dictionary is empty, erase data
                m_keys = null;
                m_values = null;
            }
            else
            {
                //Initialize arrays
                int cnt = Dict.Count;
                m_keys ??= new List<TKey>(cnt);
                m_values ??= new List<TValue>(cnt);
                
                using Dictionary<TKey, TValue>.Enumerator e = Dict.GetEnumerator();
                for (int i = 0; e.MoveNext(); i++)
                {
                    KeyValuePair<TKey, TValue> pair = e.Current;
                    if (m_keys.Count > i)
                    {
                        m_keys[i] = pair.Key;
                    }
                    else
                    {
                        m_keys.Add(pair.Key);
                    }

                    if (m_values.Count > i)
                    {
                        m_values[i] = pair.Value;
                    }
                    else
                    {
                        m_values.Add(pair.Value);
                    }
                }

                while (m_keys.Count > cnt)
                {
                    m_keys.RemoveAt(m_keys.Count - 1);
                }

                while (m_values.Count > cnt)
                {
                    m_values.RemoveAt(m_values.Count - 1);
                }
            }
        }

        #endregion

    }
}
