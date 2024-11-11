using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Simemes;

public class UIDialogEditor : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField _inputField;

    public void SaveDialog()
    {
        GameManager.instance.SaveData.Profile.DialogText = _inputField.text;
        GameManager.instance.SavePlayerData();

        gameObject.SetActive(false);
    }

    public void LoadDialog()
    {
        var text = GameManager.instance.SaveData.Profile.DialogText;
        _inputField.text = text;

        gameObject.SetActive(true);

    }

    public void ToggleDialog()
    {
        if (gameObject.activeSelf)
            SaveDialog();
        else
            LoadDialog();
    }
}
