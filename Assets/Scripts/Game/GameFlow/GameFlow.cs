using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Threading.Tasks;
using Core.Utilities;
using Core.Auth;
using Simemes;
using Simemes.Treasures;
using Simemes.Tasks;
using Simemes.Request;
using Simemes.Steal;
using Simemes.Frene;
using Simemes.Rank;
using Simemes.Announcement;

public class GameFlow : MonoSingleton<GameFlow>
{
    public static SynchronizationContext MainThread;
    [SerializeField]
    private Animator _state;

    protected override void Awake()
    {
        MainThread = SynchronizationContext.Current;
        base.Awake();
    }

    protected virtual void Start()
    {
        Init();
    }

    public async Task Init()
    {
        try
        {
            Input.multiTouchEnabled = false;
            await AuthMgr.instance.SignIn();
            await GameManager.instance.LoadPlayerData();
            await StealSystem.instance.Init();
            await TreasureSystem.instance.Init();
            await RankSystem.instance.Init();
            await AnnouncementSystem.instance.Init();
            await FreneSystem.instance.Init();
            await StealRequest.GetChestDatas();
            await TaskMgr.instance.Init(GameManager.instance.PlayerProfile.TaskProgress);

            SetBool("UserDataLoaded", true);
        }
        catch (System.Exception e)
        {
            Debug.LogException(e);
        }
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
