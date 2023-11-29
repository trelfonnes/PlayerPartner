using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData 
{
    private Dictionary<string, object> saveData = new Dictionary<string, object>();

    public void AddData(string key, object value)
    {
        if (!saveData.ContainsKey(key))
            saveData.Add(key, value);
        else
            saveData[key] = value;
    }

    public T GetData<T>(string key, T defaultValue = default(T))
    {
        if (saveData.TryGetValue(key, out object value))
            return (T)value;
        return defaultValue;
    }
}
