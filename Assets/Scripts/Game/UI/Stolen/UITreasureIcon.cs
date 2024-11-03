using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UITreasureIcon : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _text;

    public void Set(Sprite sprite, string name)
    {
        SetSprite(sprite);
        SetName(name);
    }

    public void SetSprite(Sprite sprite)
    {
        if(sprite != null && _icon!=null)
            _icon.sprite = sprite;
    }

    public void SetName(string name)
    {
        if (_text != null)
            _text.text = name;
    }
}
