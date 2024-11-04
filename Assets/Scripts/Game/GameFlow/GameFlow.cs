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

            Debug.Log("Steal Init");

            await StealSystem.instance.Init();

            Debug.Log("Treasure Init");
            await TreasureSystem.instance.Init();

            Debug.Log("Rank Init");
            await RankSystem.instance.Init();

            Debug.Log("Announce Init");
            await AnnouncementSystem.instance.Init();

            Debug.Log("Frene Init");
            await FreneSystem.instance.Init();

            Debug.Log("Chest Init");
            await StealRequest.GetChestDatas();

            var profile = GameManager.instance.PlayerProfile;
            await TaskMgr.instance.Init(profile.TaskProgress, profile.TaskEventProgress);
            await DailyCheckInSystem.instance.Init();

            Debug.Log("Init Finished");
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
