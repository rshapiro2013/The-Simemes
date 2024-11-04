using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;

public class TelegramAPI
{
    private const string _token = "7959869249:AAGKSGT6xhmUUS09amcIrrzlczSdyq9dMKw";
    public static string API_URL
    {
        get
        {
            return $"https://api.telegram.org/bot{_token}/";
        }
    }

    public static string FILE_URL
    {
        get
        {
            return $"https://api.telegram.org/file/bot{_token}/";
        }
    }

    public static async Task<string> GetUserProfilePhoto(string id)
    {
        WWWForm form = new WWWForm();
        form.AddField("user_id", id);
        UnityWebRequest www = UnityWebRequest.Post(API_URL + "getUserProfilePhotos", form);

        string result = await SendRequest(www);

        return result;
    }

    public static async Task<string> GetFile(string fileId)
    {
        WWWForm form = new WWWForm();
        form.AddField("file_id", fileId);
        UnityWebRequest www = UnityWebRequest.Post(API_URL + "getFile", form);

        string result = await SendRequest(www);

        return result;
    }

    public static string GetPhotoUrl(string filePath)
    {
        return FILE_URL + filePath;
    }

    private static async Task<string> SendRequest(UnityWebRequest www)
    {
        www.SendWebRequest();

        while (!www.isDone)
            await UniTask.DelayFrame(20); // save Power

        if (www.isHttpError || www.isNetworkError)
        {
            Debug.LogError($"Telegram API Error: {www.error}");
            return string.Empty;
        }

        return www.downloadHandler.text;
    }
}
