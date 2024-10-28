using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UIDropArea : MonoBehaviour, IDropHandler
{
    [SerializeField] private UnityEvent<UIDropArea, GameObject> _onDrop;
    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedObject = eventData.pointerDrag;

        if (droppedObject != null)
        {
            //droppedObject.transform.SetParent(transform);
            RectTransform rect = droppedObject.GetComponent<RectTransform>();
            rect.anchoredPosition = Vector2.zero;
            _onDrop?.Invoke(this, droppedObject);
        }
    }
}

