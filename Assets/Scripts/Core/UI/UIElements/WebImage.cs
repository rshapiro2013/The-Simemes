using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

namespace Core.UI
{
    [RequireComponent(typeof(Image))]
    public class WebImage : MonoBehaviour
    {
        [SerializeField]
        private string _url;

        private Image _image;
        private Coroutine _coroutine;

        public Image Image => _image;

        private void Awake()
        {
            _image = GetComponent<Image>();

            Load(_url);
        }

        IEnumerator SetImage(string url)
        {
            UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
            yield return request.SendWebRequest();

            if (request.isHttpError || request.isNetworkError)
            {
                Debug.LogError("Can not find image!");
                yield break;
            }

            var texture = DownloadHandlerTexture.GetContent(request);
            _image.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            _coroutine = null;
        }

        public void Load(string url)
        {
            if (string.IsNullOrEmpty(url))
                return;

            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _url = url;
            _coroutine = StartCoroutine(SetImage(url));
        }
    }
}