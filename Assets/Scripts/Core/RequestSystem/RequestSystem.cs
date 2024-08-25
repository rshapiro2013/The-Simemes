using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Utilities;
using Newtonsoft.Json;

public class RequestSystem : MonoSingleton<RequestSystem>
{
    public void RequestData<T>(string requestName, T data)
    {
        string serializedData = PlayerPrefs.GetString(requestName, string.Empty);
        JsonConvert.PopulateObject(serializedData, data);
    }

    public void UploadData<T>(string requestName, T data)
    {
        string serializedData = JsonConvert.SerializeObject(data);
        PlayerPrefs.SetString(requestName, serializedData);
    }
}
