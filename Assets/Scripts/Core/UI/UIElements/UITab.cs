using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UITab : MonoBehaviour
{
    [SerializeField]
    private List<ObjectState> _tabs;

    [SerializeField]
    private int _defaultTab;

    [SerializeField]
    private UnityEvent<int> _onSwitchTab;

    private void Start()
    {
        SetTab(_defaultTab);
    }

    public void SetTab(int idx)
    {
        for (int i = 0; i < _tabs.Count; ++i)
            _tabs[i].SetState(i == idx ? 1 : 0);

        _onSwitchTab.Invoke(idx);
    }
}
