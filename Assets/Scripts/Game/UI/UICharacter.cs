using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Simemes;

public class UICharacter : MonoBehaviour
{
    [SerializeField]
    private List<Sprite> _characters;

    [SerializeField]
    private Image _characterImage;

    private readonly Dictionary<string, Sprite> _characterData = new Dictionary<string, Sprite>();

    private void Awake()
    {
        foreach (var sprite in _characters)
            _characterData[sprite.name] = sprite;

        GameManager.instance.PlayerProfile.OnUpdateTierData += UpdateBackground;
    }

    private void OnDestroy()
    {
        GameManager.instance.PlayerProfile.OnUpdateTierData -= UpdateBackground;
    }

    private void UpdateBackground(Simemes.Tier.TierData tierData)
    {
        string character = GameManager.instance.PlayerProfile.Character;

        string characterLevelSprite = $"{character} {tierData.Tier}";

        _characterData.TryGetValue(characterLevelSprite, out var sprite);
        _characterImage.sprite = sprite;
    }
}
