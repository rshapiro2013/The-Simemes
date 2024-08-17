using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Simemes.Poop;
using Simemes.Landscape;

namespace Simemes.UI
{
    public class PoopItem : ILandscapeItem
    {
        private GameObject _prefab;
        private Sprite image;

        public GameObject Prefab => _prefab;

        public Sprite Image => image;

        public long CreationTime { get; set; }

        public PoopItem(GameObject prefab, long currentTime)
        {
            _prefab = prefab;
            CreationTime = currentTime;
        }
    }

    public class UIPoopDrop : MonoBehaviour
    {
        [SerializeField] private RectTransform _dropRange;

        [SerializeField] private GameObject _dropPrefab;

        [SerializeField] private GameObject _collectBtn;

        [SerializeField] private AnimationCurve _collectCurve;
        [SerializeField] private float _collectTime;
        [SerializeField] private RectTransform _collectTarget;


        private List<GameObject> _spawnedObjects = new List<GameObject>();
        private bool _isPlaying;

        protected void Awake()
        {
            if (PoopSystem.instance)
                PoopSystem.instance.OnSpawnPoop += RandomSpawnOne;
        }

        protected void OnDestroy()
        {
            if(PoopSystem.instance)
                PoopSystem.instance.OnSpawnPoop -= RandomSpawnOne;
        }

        private void Start()
        {
            _collectBtn.SetActive(PoopSystem.instance.PropCount > 0);
            Spawn();
        }

        public void Spawn()
        {
            RandomSpawn(PoopSystem.instance.PropCount);
        }

        public void RandomSpawnOne()
        {
            _collectBtn.SetActive(true);
            RandomSpawn(1);
        }

        public void RandomSpawn(int count)
        {
            Vector2 worldMin = _dropRange.TransformPoint(_dropRange.rect.min);
            Vector2 worldMax = _dropRange.TransformPoint(_dropRange.rect.max);
            for (int i = 0; i < count; ++i)
            {
                float randomX = Random.Range(worldMin.x, worldMax.x);
                float randomY = Random.Range(worldMin.y, worldMax.y);
                Vector2 randomPosition = new Vector2(randomX, randomY);
                Create(randomPosition);
            }
        }

        private GameObject Create(Vector2 randomPosition)
        {
            GameObject newObject = Instantiate(_dropPrefab, _dropRange);
            RectTransform rectTransform = newObject.transform as RectTransform;
            rectTransform.position = randomPosition;
            rectTransform.parent = transform;
            _spawnedObjects.Add(newObject);
            newObject.SetActive(true);

            return newObject;
        }

        public void ClearAll()
        {
            foreach (GameObject obj in _spawnedObjects)
            {
                Destroy(obj);
            }

            _spawnedObjects.Clear();
        }

        public void Collect()
        {
            _collectBtn.SetActive(false);
            StartCoroutine(PlayCollect());
        }

        private void CollectBase(List<GameObject> spawnedObjects)
        {
            GameManager.instance.AddCoin(PoopSystem.instance.PropCount);
            PoopSystem.instance.Collect();

            foreach (GameObject obj in spawnedObjects)
            {
                _spawnedObjects.Remove(obj);
                Destroy(obj);
            }
        }

        private IEnumerator PlayCollect()
        {
            if (_isPlaying)
                yield break;

            _isPlaying = true;

            float timer = 0;

            List<GameObject> spawnedObjects = new List<GameObject>(_spawnedObjects);
            Vector3[] pos = new Vector3[spawnedObjects.Count];
            for (int i = 0; i < spawnedObjects.Count; ++i)
            {
                pos[i] = ((RectTransform)spawnedObjects[i].transform).position;
            }

            Vector3 target = _collectTarget.position;
            while (_isPlaying)
            {
                timer += Time.deltaTime;
                if (timer > _collectTime)
                {
                    break;
                }
                float time = timer / _collectTime;
                for (int i = 0; i < spawnedObjects.Count; ++i)
                {
                    RectTransform rectTransform = spawnedObjects[i].transform as RectTransform;
                    rectTransform.position = Vector3.Lerp(pos[i], target, _collectCurve.Evaluate(time));
                }
                yield return new WaitForEndOfFrame();
            }

            CollectBase(spawnedObjects);

            _isPlaying = false;
        }
    }
}