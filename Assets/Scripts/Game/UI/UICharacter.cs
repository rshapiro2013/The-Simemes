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

        GameManager.instance.PlayerProfile.OnUpdateTierData += UpdateCharacter;
    }

    private void OnDestroy()
    {
        if (GameManager.instance != null)
            GameManager.instance.PlayerProfile.OnUpdateTierData -= UpdateCharacter;
    }

    private void UpdateCharacter(Simemes.Tier.TierData tierData)
    {
        string character = GameManager.instance.PlayerProfile.Character;

        string characterLevelSprite = $"{character} {tierData.Tier}";

        _characterData.TryGetValue(characterLevelSprite, out var sprite);

        if (sprite == null)
            sprite = _characters[_characters.Count - 1];

        _characterImage.sprite = sprite;
        _characterImage.SetNativeSize();
    }
}
