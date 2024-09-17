using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using Core.Utilities;
using Core.Networking;
using Simemes;
using Simemes.Treasures;

public class GameFlow : MonoSingleton<GameFlow>
{
    [SerializeField]
    private Animator _state;

    protected virtual void Start()
    {
        Init();
    }

    public async Task Init()
    {
        Input.multiTouchEnabled = false;

        await RequestSystem.instance.Login();
        await GameManager.instance.LoadPlayerData();
        await TreasureSystem.instance.Init();

        SetBool("UserDataLoaded", true);
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
