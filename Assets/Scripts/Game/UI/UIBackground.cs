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

    private readonly Dictionary<string, Sprite> _backgroundData = new Dictionary<string, Sprite>();

    private void Awake()
    {
        foreach (var sprite in _backgrounds)
            _backgroundData[sprite.name] = sprite;

        GameManager.instance.PlayerProfile.OnUpdateTierData += UpdateBackground;
    }

    private void OnDestroy()
    {
        GameManager.instance.PlayerProfile.OnUpdateTierData -= UpdateBackground;
    }

    private void UpdateBackground(Simemes.Tier.TierData tierData)
    {
        _backgroundData.TryGetValue(tierData.Background, out var sprite);
        if (sprite == null)
            sprite = _backgrounds[_backgrounds.Count - 1];

        _backgroundImage.sprite = sprite;
    }
}
