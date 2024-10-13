using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Utilities;
using Simemes;
using Simemes.Rewards;
using System.Threading.Tasks;

public class DailyCheckInSystem : MonoSingleton<DailyCheckInSystem>
{
    [SerializeField]
    private List<RewardData> _rewardData;

    public event System.Action<int> OnCheckIn;

    public async Task Init()
    {
        var today = System.DateTime.Today;
        string date = today.ToShortDateString();

        int checkInDay = PlayerPrefs.GetInt("CheckInDays", 0);
        string lastCheckInDate = PlayerPrefs.GetString("LastCheckIn", string.Empty);
        if(lastCheckInDate != date)
        {
            ++checkInDay;

            if (checkInDay > _rewardData.Count)
                checkInDay = 1;

            OnCheckIn?.Invoke(checkInDay);
            
            PlayerPrefs.SetInt("CheckInDays", checkInDay);
            PlayerPrefs.SetString("LastCheckIn", date);

            var reward = GetReward(checkInDay - 1);

            RewardMgr.instance.ObtainReward(reward.ID, reward.Count);
            GameManager.instance.SavePlayerData();
        }
    }
    
    public RewardData GetReward(int idx)
    {
        return _rewardData[idx];
    }
}
