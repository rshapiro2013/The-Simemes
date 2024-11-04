using UnityEngine;
using Core.Utilities;
using Newtonsoft.Json;
using System.Threading.Tasks;

public class TelegramController : MonoSingleton<TelegramController>
{
    [SerializeField]
    private string _imgProxyServer;

    private WebAppUser _user;

    public WebAppUser User => _user;

    
    public void SetWebAppUser(string data)
    {
        _user = JsonUtility.FromJson<WebAppUser>(data);
        Debug.Log("Telegram User:" + data);
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
}
