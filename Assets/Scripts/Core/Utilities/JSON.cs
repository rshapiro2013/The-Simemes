using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

namespace Core.Utilities
{
    public class JSON : Dictionary<string, object>
    {
        public T Parse<T>(string key)
        {
            if (this[key] is T)
                return (T)this[key];
            else
            {
                string data = JsonConvert.SerializeObject(this[key]);
                var result = JsonConvert.DeserializeObject<T>(data);
                return result;
            }
        }

        public JSON ToJSON(string key)
        {
            string data = JsonConvert.SerializeObject(this[key]);
            var result = JsonConvert.DeserializeObject<JSON>(data);
            return result;
        }

        public List<T> ParseList<T>(string key)
        {
            List<T> list = new List<T>();
            if (!TryGetValue(key, out var obj))
                return list;

            if(obj is IEnumerable)
            {
                foreach(var element in (obj as IEnumerable))
                {
                    if (element is T)
                        list.Add((T)element);
                    else
                    {
                        string str = element.ToString();
                        var listElement = JsonConvert.DeserializeObject<T>(str);
                        list.Add(listElement);
                    }
                }
            }

            return list;
        }
    }
}