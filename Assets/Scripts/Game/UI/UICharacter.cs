using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Spine;
using Spine.Unity;

using Simemes;

public class UICharacter : MonoBehaviour
{
    [SerializeField]
    private List<Sprite> _characters;

    [SerializeField]
    private List<SkeletonDataAsset> _spineData;

    [SerializeField]
    private Image _characterImage;

    [SerializeField]
    private SkeletonGraphic _characterSpine;

    private readonly Dictionary<string, Sprite> _characterData = new Dictionary<string, Sprite>();

    private void Awake()
    {
        foreach (var sprite in _characters)
            _characterData[sprite.name] = sprite;

        //GameManager.instance.PlayerProfile.OnUpdateTierData += UpdateCharacter;
        GameManager.instance.PlayerProfile.OnUpdateTierData += UpdateCharacterSpine;
    }

    private void OnDestroy()
    {
        if (GameManager.instance != null)
        {
            //GameManager.instance.PlayerProfile.OnUpdateTierData -= UpdateCharacter;
            GameManager.instance.PlayerProfile.OnUpdateTierData -= UpdateCharacterSpine;
        }
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

    private void UpdateCharacterSpine(Simemes.Tier.TierData tierData)
    {
        var skeletonData = _spineData[tierData.Tier - 1];
        _characterSpine.skeletonDataAsset = skeletonData;
        _characterSpine.Initialize(true);
    }
}
