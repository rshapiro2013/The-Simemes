using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;

public class UISpriteAnim : MonoBehaviour
{
    [SerializeField]
    private bool _loop;

    [SerializeField]
    private float _loopDelayTime;

    [SerializeField]
    private bool _autoPlay;

    [SerializeField]
    private float _autoPlayFPS;

    [SerializeField]
    private Sprite[] _spriteList;

    [SerializeField]
    private bool _setNativeSize = true;

    [SerializeField]
    private bool _hideIfNoSprite;

    [SerializeField]
    private Image[] _images;

    [SerializeField]
    private int _loopStartIndex;


    [SerializeField]
    private bool _setOnAwake = true;

    [SerializeField]
    private UnityEngine.Events.UnityEvent _onStop;

    private Image _image;
    private SpriteRenderer _spriteRenderer;

    private int _spriteIndex = -1;

    public int SpriteIndex { get { return _spriteIndex; } }

    private bool _isInit = false;

    private bool _reversed;

    private int _playNumber = 0;

    public Image Image
    {
        get
        {
            if (_image == null)
                _image = GetComponent<Image>();

            return _image;
        }
    }

    public int SpriteCount => _spriteList.Length;

    public bool IsPlaying { get; private set; } = false;

    private bool _enabled = false;

    private void Awake()
    {
        Init();
    }

    private void OnDestroy()
    {
        _isInit = false;
    }

    private void OnEnable()
    {
        _enabled = true;
        if (_autoPlay)
        {
            AsyncUpdate();
        }
    }

    private void OnDisable()
    {
        _enabled = false;
        IsPlaying = false;
    }

    public void Init()
    {
        if (!_isInit)
        {
            _isInit = true;

            _image = GetComponent<Image>();
            _spriteRenderer = GetComponent<SpriteRenderer>();

            if (_setOnAwake)
                SetSprite(0);
            else
            {
                for (int i =0; i < _spriteList.Length; i++)
                {
                    if (_spriteList[i] == _image.sprite)
                    {
                        _spriteIndex = i;
                        break;
                    }
                }
            }
        }
    }

    private async void AsyncUpdate()
    {
        if (!_isInit)
            return;

        int playNumber = ++_playNumber;

        int idx = _reversed ? _spriteList.Length - 1 : 0;
        int length = _spriteList.Length;

        SetSprite(idx);

        while (_enabled)
        {
            IsPlaying = true;
            await UniTask.DelayFrame((int)(1000 / _autoPlayFPS));

            if (!IsPlaying || playNumber != _playNumber)
                return;

            if (!_isInit || !gameObject.activeSelf)
                break;

            if (_reversed)
                --idx;
            else
                ++idx;

            if (idx == length && !_reversed)
            {
                if (_loop)
                {
                    idx = _loopStartIndex;
                    if (_loopDelayTime > 0)
                    {
                        SetSprite(idx);
                        await UniTask.DelayFrame((int)(_loopDelayTime * 1000));
                    }
                }
                else
                    break;
            }

            if (idx < 0 && _reversed)
            {
                if (_loop)
                {
                    idx = _spriteList.Length - 1;
                    if (_loopDelayTime > 0)
                    {
                        SetSprite(idx);
                        await UniTask.DelayFrame((int)(_loopDelayTime * 1000));
                    }
                }
                else
                    break;
            }

            SetSprite(idx);
        }
        IsPlaying = false;
        _onStop.Invoke();
    }

    public void SetSprite(int idx)
    {
        SetSprite(idx, false);
    }

    public void NextSprite()
    {
        int nextIdx = (_spriteIndex + 1) % SpriteCount;
        SetSprite(nextIdx, true);
    }

    public void SetSprite(int idx, bool forceUpdate)
    {
        if (_spriteIndex == idx && !forceUpdate)
            return;

        if (_image == null && _spriteRenderer == null && _images.Length == 0)
            Init();

        if (_image == null && _spriteRenderer == null && _images.Length ==0)
            return;

        if (idx >= _spriteList.Length)
            return;

        _spriteIndex = idx;
        Sprite sprite = _spriteList[idx];
        if (_image != null)
        {
            SetSprite(_image, sprite);
        }
        else if (_spriteRenderer != null)
            _spriteRenderer.sprite = sprite;
        
        if(_images.Length > 0)
        {
            foreach (var image in _images)
                SetSprite(image, sprite);
        }
    }

    public void SetSprites(Sprite[] sprites)
    {
        _spriteList = sprites;
        if (_spriteIndex >= 0)
            SetSprite(_spriteIndex, true);
    }

    public void ReplaceSprite(int idx, Sprite sprite)
    {
        if (idx >= 0 && idx < _spriteList.Length)
            _spriteList[idx] = sprite;
    }

    public void ApplySprites(UISpriteAnim spriteAnim)
    {
        _spriteList = spriteAnim._spriteList;
    }

    public void Play(bool reversed = false)
    {
        _reversed = reversed;
        if (_reversed)
            SetSprite(_spriteList.Length - 1);
        else
            SetSprite(0);

        AsyncUpdate();
    }

    public void Stop(bool reversed = false)
    {
        IsPlaying = false;
        _reversed = reversed;

        if (_reversed)
            SetSprite(_spriteList.Length - 1);
        else
            SetSprite(0);
    }

    private void SetSprite(Image image, Sprite sprite)
    {
        image.sprite = sprite;
        if (sprite != null && _setNativeSize)
            image.SetNativeSize();

        if (_hideIfNoSprite)
            image.enabled = sprite != null;
    }

}
