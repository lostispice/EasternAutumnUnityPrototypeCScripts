using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is used by the PlayerProfile class to store awards and extra lives data.
/// As Unity is unable to directly serialise dictionaries when saving data to JSON files, this script allows for dictionaries to be (de)serialised so that can be saved to (or loaded from) JSON format.
/// American spelling is used for consistency with Unity coding conventions.
/// </summary>
/// <typeparam name="TKey"></typeparam>
/// <typeparam name="TValue"></typeparam>
[System.Serializable]
public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
{
    [SerializeField] private List<TKey> keys = new List<TKey>();
    [SerializeField] private List<TValue> values = new List<TValue>();

    /// <summary>
    /// Converts a dictionary into two, serialisable lists.
    /// Used when saving a dictionary.
    /// </summary>
    public void OnBeforeSerialize()
    {
        keys.Clear(); 
        values.Clear();
        foreach (KeyValuePair<TKey, TValue> pair in this)
        {
            keys.Add(pair.Key);
            values.Add(pair.Value);
        }
    }

    /// <summary>
    /// Reconstructs a dictionary from two, serialised lists.
    /// Used when loading a dictionary.
    /// </summary>
    public void OnAfterDeserialize()
    {
        this.Clear();

        for (int i = 0; i < keys.Count; i++)
        {
            this.Add(keys[i], values[i]);
        }
    }
}
