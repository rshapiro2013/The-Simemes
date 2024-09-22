using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropRangeModifier : MonoBehaviour
{
    [SerializeField]
    private RectTransform _dropRange;

    [SerializeField]
    private RectTransform _dropRange_Upper;

    [SerializeField]
    private RectTransform _dropRange_Lower;

    [SerializeField]
    private float _minHeight = 100;

    private void Start()
    {
        AdjustLandscapeArea();
    }

    private void AdjustLandscapeArea()
    {
        var sizeDelta = _dropRange.sizeDelta;
        var posUpper = _dropRange_Upper.localPosition;
        var posLower = _dropRange_Lower.localPosition;

        sizeDelta.y = Mathf.Max(posUpper.y - posLower.y, _minHeight);
        _dropRange.sizeDelta = sizeDelta;
    }
}
