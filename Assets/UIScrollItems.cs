using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScrollItems : MonoBehaviour
{
    [SerializeField] private ScrollRect _scrollRect;
    [SerializeField] private Transform _contentRoot;
    [SerializeField] private UITreasureIcon _sourceItem;

    private List<UITreasureIcon> _pool = new List<UITreasureIcon>();

    public List<UITreasureIcon> Pool => _pool;

    private void OnEnable()
    {
        _scrollRect.velocity = Vector2.zero;
        _scrollRect.horizontalNormalizedPosition = 0f;
    }

    public void Set(int itemCount)
    {
        OnEnable();

        if (_pool.Count < itemCount)
        {
            int need = itemCount - _pool.Count;
            for (int i = 0; i < need; ++i)
            {
                UITreasureIcon item = Instantiate(_sourceItem, _contentRoot);
                _pool.Add(item);
            }
        }
    }
}
