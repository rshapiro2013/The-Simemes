using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropArea : MonoBehaviour
{
    [SerializeField]
    private List<RectTransform> _dropAreas;

    public void GetParentAndPos(out RectTransform parent, out Vector2 pos)
    {
        int dropAreaIdx = Random.Range(0, _dropAreas.Count);

        var dropArea = _dropAreas[dropAreaIdx];

        Vector2 worldMin = dropArea.TransformPoint(dropArea.rect.min);
        Vector2 worldMax = dropArea.TransformPoint(dropArea.rect.max);


        float randomX = Random.Range(worldMin.x, worldMax.x);
        float randomY = Random.Range(worldMin.y, worldMax.y);
        Vector2 randomPosition = new Vector2(randomX, randomY);

        parent = dropArea;
        pos = randomPosition;
    }
}
