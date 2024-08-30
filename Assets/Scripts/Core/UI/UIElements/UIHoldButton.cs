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
    protected Slider _progress;

    [SerializeField]
    protected float _visibleProgressTime = 0.2f;

    [SerializeField]
    protected UnityEvent _onClick;

    [SerializeField]
    protected UnityEvent _onHold;


    protected bool _holdEventTriggered;
    protected bool _holding;
    protected float _holdTimer;

    protected bool _showProgress;

    protected virtual void Awake()
    {
        ShowProgressBar(false);
    }

    public void OnPointerClick(PointerEventData evt)
    {
        if (!_holdEventTriggered)
            _onClick.Invoke();
    }

    public void OnPointerUp(PointerEventData evt)
    {
        _holding = false;

        ShowProgressBar(_showProgress);

    }

    public void OnPointerDown(PointerEventData evt)
    {
        _holding = true;
        _holdTimer = _holdThreshold;
        _holdEventTriggered = false;

        ShowProgressBar(_showProgress);
    }

    public void ShowProgressBar(bool enabled)
    {
        if (_progress == null)
            return;

        _showProgress = enabled;
        _progress.gameObject.SetActive(_showProgress && _holding && _holdTimer < _holdThreshold - _visibleProgressTime);
    }

    protected void Update()
    {
        if (!_holding)
            return;

        _holdTimer -= Time.deltaTime;

        if (_progress != null)
            _progress.value = (_holdThreshold - _holdTimer) / _holdThreshold;

        if (_holdTimer <= 0)
        {
            _holding = false;

            TriggerHoldEvent();
        }

        ShowProgressBar(_showProgress);
    }

    protected void TriggerHoldEvent()
    {
        _holdEventTriggered = true;
        _onHold.Invoke();
    }
}
