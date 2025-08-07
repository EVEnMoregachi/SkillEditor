using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using Newtonsoft.Json;


public static class DataUtility
{
    public static JsonSerializerSettings jsonSetting;
    static DataUtility()
    {
        jsonSetting = new JsonSerializerSettings()
        {
            TypeNameHandling = TypeNameHandling.Auto,
            Formatting = Formatting.Indented,
        };
    }

    public static string ToJson<T>(T obj)
    {
        string json = JsonConvert.SerializeObject(obj, jsonSetting);
        return json;
    }

    public static T FromJson<T>(string json)
    {
        T obj = JsonConvert.DeserializeObject<T>(json, jsonSetting);
        return obj;
    }
}
