using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Simemes.Rewards;

public class RewardSlot : MonoBehaviour
{
    [SerializeField]
    private Image _icon;

    [SerializeField]
    private GameObject _obj_Received;

    [SerializeField]
    private GameObject _obj_Today;

    [SerializeField]
    private TextMeshProUGUI _count;

    public void Set(RewardData reward)
    {
        var config = RewardMgr.instance.GetRewardConfig(reward.ID);
        _icon.sprite = config.Image;

        _count.text = reward.Count.ToString();

        _obj_Received.SetActive(false);
        _obj_Today.SetActive(false);
    }

    public void SetAsToday()
    {
        _obj_Today.SetActive(true);
    }

    public void SetReceived()
    {
        _obj_Received.SetActive(true);

        _count.text = "GET";
    }
}
