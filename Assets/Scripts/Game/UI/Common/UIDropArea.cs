using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UIDropArea : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private UnityEvent<UIDropArea, GameObject> _onDrop;

    [SerializeField]
    private Color _color_Enter;

    [SerializeField]
    private Color _color_Normal;

    [SerializeField]
    private UnityEvent<GameObject> _onEnter;

    [SerializeField]
    private UnityEvent<GameObject> _onExit;

    [SerializeField] 
    private Component _userData;

    public Component UserData { get => _userData; set => _userData = value; }

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

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null)
            return;

        _onEnter?.Invoke(eventData.pointerDrag);
}

    public void OnPointerExit(PointerEventData eventData)
    {
        _onExit?.Invoke(eventData.pointerDrag);
    }

}

