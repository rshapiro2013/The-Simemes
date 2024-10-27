using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class DelayAction : MonoBehaviour
{
    [SerializeField] protected bool _invokeOnEnable;
    [SerializeField] protected float _delayTime = 0;
    [SerializeField] protected private UnityEvent _event;

    private Coroutine _invoke;

    private void OnEnable()
    {
        if(_invokeOnEnable)
            Execute();
    }

    private void OnDisable()
    {
        if (_invoke != null)
            StopCoroutine(_invoke);
        _invoke = null;
    }

    public virtual void Execute()
    {
        _invoke = StartCoroutine(Invoke());
    }

    private IEnumerator Invoke()
    {
        yield return new WaitForSeconds(_delayTime);
        _event.Invoke();
        _invoke = null;
    }
}
