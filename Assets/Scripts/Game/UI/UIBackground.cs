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
        foreach (var sprite in _backgrounds)
            _backgroundData[sprite.name] = sprite;

        if (_updateByPlayerProfile && GameManager.instanceExists)
            GameManager.instance.PlayerProfile.OnUpdateTierData += UpdateBackground;
    }

    private void OnDestroy()
    {
        if (_updateByPlayerProfile && GameManager.instanceExists)
            GameManager.instance.PlayerProfile.OnUpdateTierData -= UpdateBackground;
    }

    private void UpdateBackground(Simemes.Tier.TierData tierData)
    {
        SetBackground(tierData.Background);
    }

    public void SetBackground(string background)
    {
        _backgroundData.TryGetValue(name, out var sprite);
        if (sprite == null)
            sprite = _backgrounds[_backgrounds.Count - 1];

        _backgroundImage.sprite = sprite;
    }
}
