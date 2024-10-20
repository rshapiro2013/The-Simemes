using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.UI;

namespace Core.UI
{
    public class UIListBase<T> : MonoBehaviour where T : MonoBehaviour, new()
    {
        [Header("UIList")]

        [SerializeField]
        protected T _elementTemplate;

        [SerializeField]
        protected Vector3 _elementDeltaDistance;

        [SerializeField]
        protected int _maxCount;

        [SerializeField]
        protected Vector3 _rowDeltaDistance;

        [SerializeField]
        protected int _rowMaxCount;

        [SerializeField]
        protected List<T> _elements;

        [SerializeField]
        protected bool _reverseDisplayOrder;

        [SerializeField]
        protected bool _setSiblingIndex;

        [SerializeField]
        protected bool _isVerticalAlign;

        protected Queue<T> _pool = new Queue<T>();

        protected bool _prepared = false;

        public int MaxCount => _maxCount;
        public int RowMaxCount { get { return _rowMaxCount; } set { _rowMaxCount = value; } }
        public int Count => _elements.Count;
        protected virtual void Awake()
        {
            PrepareElements();
        }

        public virtual T GetElement(int idx)
        {
            if (_elements == null || _elements.Count <= idx)
                return default(T);

            return _elements[idx];
        }

        public virtual void ModifyElements(System.Action<int, T> modifyFunc)
        {
            for (int i = 0; i < _elements.Count; ++i)
                modifyFunc(i, _elements[i]);
        }

        public virtual void PrepareElements()
        {
            if (_prepared)
                return;

            Transform templateTrans = _elementTemplate.transform;

            for (int i = 0; i < _maxCount; ++i)
            {
                T element = InstantiateElement();

                _pool.Enqueue(element);
            }

            _prepared = true;
        }
        public virtual void CreateElements(int count, bool enabled = false, System.Action<T> initFunc = null)
        {
            for (int i = 0; i < count; ++i)
            {
                var element = CreateElement();
                if (enabled)
                    element.gameObject.SetActive(true);
                initFunc?.Invoke(element);
            }
        }

        public virtual T CreateElement()
        {
            T element;
            if (_pool.Count == 0)
            {
                if (_maxCount == 0)
                    element = InstantiateElement();
                else
                    return default(T);
            }
            else
                element = _pool.Dequeue();

            int elementIdx = _elements.Count;
            int rowIdx = 0;

            if (_rowMaxCount > 0)
            {
                rowIdx = elementIdx / _rowMaxCount;
                elementIdx = elementIdx % _rowMaxCount;
            }

            Transform lastElementTransform = _elements.Count == 0 ? null : _elements[_elements.Count - 1].transform;
            Transform templateTrans = _elementTemplate.transform;
            Transform newElementTrans = element.transform;

            SetElementTransform(newElementTrans, rowIdx, elementIdx, templateTrans, lastElementTransform);

            _elements.Add(element);
            if (_setSiblingIndex)
                element.transform.SetSiblingIndex(_elements.Count);

            var rectTrans = newElementTrans as RectTransform;

            return element;
        }

        public virtual void SetElementTransform(Transform newElementTrans, int rowIdx, int elementIdx, Transform templateTrans, Transform lastElementTrans)
        {
            newElementTrans.localPosition = templateTrans.localPosition + _rowDeltaDistance * rowIdx + _elementDeltaDistance * elementIdx;
            newElementTrans.localScale = templateTrans.localScale;
        }

        public virtual void RemoveElement(int idx, bool hide = true)
        {
            T element = _elements[idx];

            for (int i = _elements.Count - 1; i > idx; --i)
            {
                _elements[i].transform.localPosition = _elements[i - 1].transform.localPosition;
            }

            if (hide)
                element.gameObject.SetActive(false);

            _elements.RemoveAt(idx);
            _pool.Enqueue(element);
        }

        public virtual void RemoveElement(List<int> idxList, bool hide = true)
        {
            foreach (int idx in idxList)
            {
                if (hide)
                    _elements[idx].gameObject.SetActive(false);

                _pool.Enqueue(_elements[idx]);
                _elements[idx] = null;
            }

            _elements.RemoveAll(T => T == null);

            Transform templateTrans = _elementTemplate.transform;

            for (int i = 0; i < _elements.Count; ++i)
            {
                Transform newElementTrans = _elements[i].transform;
                int elementIdx = i;
                int rowIdx = 0;
                if (_rowMaxCount > 0)
                {
                    rowIdx = i / _rowMaxCount;
                    elementIdx = i % _rowMaxCount;
                }
                newElementTrans.localPosition = templateTrans.localPosition + _rowDeltaDistance * rowIdx + _elementDeltaDistance * elementIdx;
            }
        }

        public virtual void Clear()
        {
            for (int i = 0; i < _elements.Count; ++i)
            {
                T element = _elements[i];
                _pool.Enqueue(element);
                element.gameObject.SetActive(false);
            }

            _elements.Clear();
        }

        public void UpdateNavigation()
        {
            int count = _elements.Count;
            int rows = (_rowMaxCount == 0) ? 1 : count / _rowMaxCount + 1;
            int cols = (_rowMaxCount == 0) ? count : _rowMaxCount;

            int idx = 0;
            int left, right, up, down;

            for (int r = 0; r < rows; ++r)
            {
                for (int c = 0; c < cols; ++c)
                {
                    if (idx >= _elements.Count)
                        return;

                    GetNeighborIndex(idx, r, c, rows, cols, count, out left, out right, out up, out down);

                    var selectable = _elements[idx].GetComponent<Selectable>();

                    var nav = selectable.navigation;
                    nav.mode = Navigation.Mode.Explicit;
                    nav.selectOnLeft = _elements[left].GetComponent<Selectable>();
                    nav.selectOnRight = _elements[right].GetComponent<Selectable>();
                    nav.selectOnUp = _elements[up].GetComponent<Selectable>();
                    nav.selectOnDown = _elements[down].GetComponent<Selectable>();

                    selectable.navigation = nav;
                    ++idx;
                }
            }
        }

        private void GetNeighborIndex(int idx, int r, int c, int rows, int cols, int maxCount, out int left, out int right, out int up, out int down)
        {
            if (c > 0)
                left = idx - 1;
            else
                left = Mathf.Min(maxCount - 1, idx + cols - 1);

            if (c < cols - 1 && idx < maxCount - 1)
                right = idx + 1;
            else
                right = idx - c;

            if (r > 0)
                up = idx - cols;
            else
            {
                up = (rows - 1) * cols + c;
                if (up >= maxCount)
                    up -= cols;
            }

            if (r < rows - 1 && idx + cols < maxCount)
                down = idx + cols;
            else
                down = c;

            if (_isVerticalAlign)
            {
                int temp = right;
                right = down;
                down = temp;

                temp = left;
                left = up;
                up = temp;
            }
        }

        private void UpdateNavigation(T element)
        {
            var selectable = element.GetComponent<Selectable>();
            var left = selectable.navigation.selectOnLeft;
            var right = selectable.navigation.selectOnRight;
            var up = selectable.navigation.selectOnUp;
            var down = selectable.navigation.selectOnDown;

        }

        public void Sort(System.Comparison<T> sortFunc)
        {
            _elements.Sort(sortFunc);

            UpdateAllElementPosition();
        }

        protected void UpdateAllElementPosition()
        {
            int rowIdx = 0;
            int elementIdx = 0;
            Transform templateTrans = _elementTemplate.transform;

            for (int i = 0; i < _elements.Count; ++i)
            {
                var element = _elements[i];

                if (_rowMaxCount > 0)
                {
                    rowIdx = i / _rowMaxCount;
                    elementIdx = i % _rowMaxCount;
                }

                Transform newElementTrans = element.transform;
                newElementTrans.localPosition = templateTrans.localPosition + _rowDeltaDistance * rowIdx + _elementDeltaDistance * elementIdx;
            }
        }

        protected virtual T InstantiateElement()
        {
            GameObject poolObj = Instantiate<GameObject>(_elementTemplate.gameObject, _elementTemplate.transform.parent);
            if (_reverseDisplayOrder)
                poolObj.transform.SetSiblingIndex(0);

            T element = poolObj.GetComponent<T>();

            return element;
        }
    }
}