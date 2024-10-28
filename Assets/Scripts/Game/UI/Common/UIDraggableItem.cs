using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIDraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private UnityEvent<GameObject> _onDrop;

    private RectTransform _rectTransform;
    private Transform _originalParent;
    private Vector2 _originalPosition;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _originalParent = transform.parent;
        _originalPosition = _rectTransform.anchoredPosition; 
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _originalParent = transform.parent;

        transform.SetParent(_originalParent.root);

        GetComponent<Image>().raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GetComponent<Image>().raycastTarget = true;

        if (transform.parent == _originalParent.root)
        {
            ResetBase();
        }
        else
            _onDrop?.Invoke(gameObject);
    }

    public void ResetBase()
    {
        GetComponent<Image>().raycastTarget = true;
        _rectTransform.anchoredPosition = _originalPosition;
        transform.SetParent(_originalParent);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        ResetBase();
    }
}
