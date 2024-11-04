using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Simemes;

public class UIBackground : MonoBehaviour
{
    [SerializeField]
    private List<Sprite> _backgrounds;

    [SerializeField]
    private Image _backgroundImage;

    [SerializeField]
    private bool _updateByPlayerProfile;

    private readonly Dictionary<string, Sprite> _backgroundData = new Dictionary<string, Sprite>();

    private void Awake()
    {
        Init();

        if (_updateByPlayerProfile && GameManager.instanceExists)
            GameManager.instance.PlayerProfile.OnUpdateTierData += UpdateBackground;
    }

    private void OnDestroy()
    {
        if (_updateByPlayerProfile && GameManager.instanceExists)
            GameManager.instance.PlayerProfile.OnUpdateTierData -= UpdateBackground;
    }

    private void Init()
    {
        foreach (var sprite in _backgrounds)
            _backgroundData[sprite.name] = sprite;
    }

    private void UpdateBackground(Simemes.Tier.TierData tierData)
    {
        SetBackground(tierData.Background);
    }

    public void SetBackground(string background)
    {
        if (_backgroundData.Count < 1)
            Init();

        _backgroundData.TryGetValue(background, out var sprite);
        if (sprite == null)
            sprite = _backgrounds[0];

        _backgroundImage.sprite = sprite;
    }
}
