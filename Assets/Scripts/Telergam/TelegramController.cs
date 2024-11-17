using UnityEngine;
using Core.Utilities;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

public class TelegramController : MonoSingleton<TelegramController>
{
    [SerializeField]
    private string _imgProxyServer;

    private WebAppUser _user;
    private string _startParam;
#if RELEASE
    private const string _botId = "barrytestbotbot";
#else
    private const string _botId = "barrytestbotbot";
#endif

    public WebAppUser User => _user;
    public string StartParam => _startParam;
    
    public void SetWebAppUser(string data)
    {
        _user = JsonUtility.FromJson<WebAppUser>(data);
        Debug.Log("Telegram User:" + data);
    }

    public void SetStartParam(string startParam)
    {
        _startParam = startParam;
    }

    public async Task RequestPhoto()
    {
        long id = _user == null ? 6484493714 : _user.id;

        string data = await TelegramAPI.GetUserProfilePhoto(id.ToString());
        var response = JsonConvert.DeserializeObject<JSON>(data);
        var profilePhoto = response.Parse<UserProfilePhotos>("result");

        if (profilePhoto.total_count == 0)
            return;

        var photos = profilePhoto.photos[0];
        var file = photos[photos.Count - 1];

        data = await TelegramAPI.GetFile(file.file_id);
        response = JsonConvert.DeserializeObject<JSON>(data);
        var filePath = (string)response.ToJSON("result")["file_path"];

        string fileUrl = _imgProxyServer + TelegramAPI.GetPhotoUrl(filePath);

        if (_user != null)
            _user.photo_url = fileUrl;
    }

    public void Share(string text)
    {
        string url = GetReferralLink();
        string link = $"https://t.me/share/url?url={url}&text={text}";
#if UNITY_EDITOR
        Debug.Log("OpenTelegramLink: " + link);
#else
        OpenTelegramLink(link);
#endif
    }

    public void CopyShareLink()
    {
        string url = GetReferralLink();
        CopyToClipboard(url);
    }

    public string GetReferralLink()
    {
        string userId = _user != null ? _user.id.ToString() : "xxxxxx";
        return $"https://t.me/{_botId}/start?startapp={userId}";
    }

    [DllImport("__Internal")]
    public static extern void OpenTelegramLink(string link);

    [DllImport("__Internal")]
    public static extern void CopyToClipboard(string textToCopy);

}
