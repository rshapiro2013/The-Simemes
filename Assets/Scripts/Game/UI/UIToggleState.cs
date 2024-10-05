using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIToggleState : MonoBehaviour
{
    [SerializeField]
    private GameObject _object;

    [SerializeField]
    private string _stateId;

    private void Awake()
    {
        int state = PlayerPrefs.GetInt(_stateId, 0);

        _object.SetActive(state == 1);
    }
}
