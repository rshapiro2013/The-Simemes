using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using Core.Utilities;

public class GameFlow : MonoSingleton<GameFlow>
{
    [SerializeField]
    private Animator _state;

    public async Task Init()
    {
        Input.multiTouchEnabled = false;

        SetTrigger("LicenseValid");
    }

    public void SetBool(string name, bool value)
    {
        _state.SetBool(name, value);
    }

    public void SetTrigger(string name)
    {
        _state.SetTrigger(name);
    }

    public void HandleEvent(string evtName)
    {
        if (evtName == "Init")
        {
            Init();
        }
    }
}
