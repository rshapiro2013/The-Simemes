using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.UI
{
    public class UIPanel : MonoBehaviour
    {
        [SerializeField]
        protected string _panelID;

        [SerializeField]
        protected Canvas _canvas;

        [SerializeField]
        protected bool _defaultEnabled;

        [SerializeField]
        protected Canvas[] _childCanvases;

        [SerializeField]
        protected UnityEngine.Events.UnityEvent _onShow;

        [SerializeField]
        protected UnityEngine.Events.UnityEvent _onHide;

        public bool Enabled => _canvas.enabled;

        protected virtual void Awake()
        {
            if (!string.IsNullOrEmpty(_panelID))
                UIPanelManager.instance.Register(_panelID, this);

            EnablePanel(_defaultEnabled, true);
        }

        protected virtual void OnDestroy()
        {
            if (!string.IsNullOrEmpty(_panelID))
                UIPanelManager.instance?.Unregister(_panelID);
        }

        protected virtual void Reset()
        {
            _canvas = GetComponent<Canvas>();
        }


        public void EnablePanel(bool enabled)
        {
            EnablePanel(enabled, false);
        }

        protected void EnablePanel(bool enabled, bool init)
        {
            if (enabled)
            {
                OnShowPanel();
            }
            else
            {
                OnHidePanel();
            }

            EnableCanvas(enabled);
        }

        protected virtual void OnShowPanel()
        {
            _onShow?.Invoke();
        }

        protected virtual void OnHidePanel()
        {
            _onHide?.Invoke();
        }

        protected void EnableCanvas(bool enabled)
        {
            foreach (var canvas in _childCanvases)
                canvas.enabled = enabled;

            if (_canvas != null)
                _canvas.enabled = enabled;
            else
                gameObject.SetActive(enabled);

            if (enabled)
                OnAfterShowPanel();
            else
                OnAfterHidePanel();
        }

        protected virtual void OnAfterShowPanel()
        {
        }

        protected virtual void OnAfterHidePanel()
        {
        }
    }
}
