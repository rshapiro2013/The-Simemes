using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using Core.Utilities;

namespace Core.UI
{
    public class UIPanelManager : MonoSingleton<UIPanelManager>
    {
        private readonly Dictionary<string, UIPanel> _panels = new Dictionary<string, UIPanel>();

        protected override void Awake()
        {
            base.Awake();
        }

        public async Task EnablePanel(string id, bool enabled)
        {
            if (_panels.TryGetValue(id, out var panel))
            {
                panel.EnablePanel(enabled);
                return;
            }
        }

        public void Register(string id, UIPanel panel)
        {
            _panels[id] = panel;
        }

        public void Unregister(string id)
        {
            _panels.Remove(id);
        }
    }
}
