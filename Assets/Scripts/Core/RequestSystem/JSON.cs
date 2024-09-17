using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

namespace Core.Networking
{
    public class JSON : Dictionary<string, object>
    {
        public T Parse<T>(string key)
        {
            var result = (T)this[key];
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