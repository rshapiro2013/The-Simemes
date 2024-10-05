using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Core.Utilities
{
    public class ImageLoader : MonoSingleton<ImageLoader>
    {
        private System.Action<Texture2D> _onSelectImage;

        public void SelectImage(System.Action<Texture2D> callback = null)
        {
            _onSelectImage = callback;

#if UNITY_WEBGL && !UNITY_EDITOR
        // WebGL 环境下，使用JavaScript的文件选择器
        OpenFileSelector();
#elif UNITY_EDITOR
            // Unity Editor 环境下，使用UnityEditor的文件选择器
            string path = EditorUtility.OpenFilePanel("Select an image", "", "png,jpg,jpeg");

            if (!string.IsNullOrEmpty(path))
            {
                // 加载图片
                LoadImageFromFile(path);
            }
#endif
        }

        // UnityEditor 下从本地路径加载图片
        private void LoadImageFromFile(string path)
        {
            byte[] imageBytes = System.IO.File.ReadAllBytes(path);

            Texture2D texture = new Texture2D(1, 1);
            texture.LoadImage(imageBytes);

            _onSelectImage?.Invoke(texture);
        }

        // WebGL 下从Base64字符串加载图片
        public void OnImageSelected(string base64Image)
        {
            LoadImage(base64Image);
        }

        private void LoadImage(string base64Image)
        {
            // 移除Base64图片的前缀
            string base64Data = base64Image.Split(',')[1];
            byte[] imageBytes = System.Convert.FromBase64String(base64Data);

            Texture2D texture = new Texture2D(1, 1);
            texture.LoadImage(imageBytes);

            _onSelectImage?.Invoke(texture);
        }

#if UNITY_WEBGL && !UNITY_EDITOR
        [System.Runtime.InteropServices.DllImport("__Internal")]
        private static extern void OpenFileSelector();
#endif
    }
}
