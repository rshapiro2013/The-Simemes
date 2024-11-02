using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIStealSuccess : MonoBehaviour
{
    [SerializeField] private  Image _icon;

    public void SetSprite(Sprite sprite)
    {
        if(sprite != null && _icon!=null)
            _icon.sprite = sprite;
    }
}
