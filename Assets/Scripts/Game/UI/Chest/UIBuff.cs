using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Simemes.Treasures;

public class UIBuff : MonoBehaviour
{
    [SerializeField]
    private Button _btnAddBuff;

    [SerializeField]
    private Image _icon;

    public void Set(ITreasureBuff buff, bool showBtn)
    {
        if (buff != null)
        {
            _icon.sprite = buff.Image;
        }

        _icon.enabled = buff != null;
        _btnAddBuff.gameObject.SetActive(showBtn);

    }
}
