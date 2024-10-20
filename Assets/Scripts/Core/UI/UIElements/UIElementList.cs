using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.UI
{
    public class UIElementList : UIListBase<UIElement>
    {
        public T CreateElement<T>() where T : MonoBehaviour
        {
            var element = base.CreateElement();

            return element as T;
        }

        public T GetElement<T>(int idx) where T : MonoBehaviour
        {
            var element = base.GetElement(idx);

            return element as T;
        }
    }
}
