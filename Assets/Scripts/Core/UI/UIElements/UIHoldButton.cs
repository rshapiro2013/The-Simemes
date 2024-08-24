using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UIHoldButton : MonoBehaviour, IPointerClickHandler, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField]
    protected float _holdThreshold;

    [SerializeField]
    protected UnityEvent _onClick;

    [SerializeField]
    protected UnityEvent _onHold;


    protected bool _holdEventTriggered;
    protected bool _holding;
    protected float _holdTimer;

    public void OnPointerClick(PointerEventData evt)
    {
        if (!_holdEventTriggered)
            _onClick.Invoke();
    }

    public void OnPointerUp(PointerEventData evt)
    {
        _holding = false;
    }

    public void OnPointerDown(PointerEventData evt)
    {
        _holding = true;
        _holdTimer = _holdThreshold;
        _holdEventTriggered = false;
    }

    protected void Update()
    {
        if (!_holding)
            return;

        _holdTimer -= Time.deltaTime;
        if(_holdTimer <= 0)
        {
            _holding = false;
            TriggerHoldEvent();
        }
    }

    protected void TriggerHoldEvent()
    {
        _holdEventTriggered = true;
        _onHold.Invoke();
    }
}
