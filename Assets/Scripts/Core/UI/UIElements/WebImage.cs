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

        private Vector2 _size;
        private Image _image;
        private Coroutine _coroutine;

        public Image Image => _image;

        private void Awake()
        {
            _image = GetComponent<Image>();
            _size = (_image.transform as RectTransform).sizeDelta;

            Load(_url);
        }

        IEnumerator SetImage(string url)
        {
            Debug.Log($"Load Image: {url}");

            UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
            yield return request.SendWebRequest();

            if (request.isHttpError || request.isNetworkError)
            {
                Debug.LogError("Can not find image!");
                yield break;
            }

            var texture = DownloadHandlerTexture.GetContent(request);
            _image.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            AdjustImageSize();

            _coroutine = null;
            Debug.Log($"Load Image Finished: {url}");
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

        private void AdjustImageSize()
        {
            float aspect = (float)_image.sprite.texture.width / _image.sprite.texture.height;

            var size = _image.rectTransform.sizeDelta;
            if (aspect < 1.0f)
                size /= aspect;
            else
                size *= aspect;

            _image.rectTransform.sizeDelta = size;
        }
    }
}