using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectState : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _stateObjects;

    [SerializeField]
    private int _defaultState = 0;

    private int _lastState = 0;

    public int State => _lastState;

    private void Awake()
    {
        _lastState = _defaultState;
    }

    private void OnEnable()
    {
        foreach (var obj in _stateObjects)
        {
            if (obj != null)
                obj.SetActive(false);
        }

        if (_stateObjects[_lastState] != null)
            _stateObjects[_lastState].SetActive(true);
    }

    public void SetState(int state)
    {
        if (_stateObjects[_lastState] != null)
            _stateObjects[_lastState].SetActive(false);

        if (_stateObjects[state] != null)
            _stateObjects[state].SetActive(true);

        _lastState = state;
    }

    public GameObject GetCurrentStateObject()
    {
        return _stateObjects[_lastState];
    }
}
