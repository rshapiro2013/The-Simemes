using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Utilities;

namespace Simemes.Poop
{
    public class PoopSystem : MonoSingleton<PoopSystem>
    {
        public System.Action OnSpawnPoop;

        [SerializeField] private int _limitCount = 20;
        [SerializeField] private int _poops = 15;
        [SerializeField] private float _testSpawnTime = 10;


        public int PoopCount => _poops;

        protected override void Awake()
        {
            base.Awake();

            StartCoroutine(DoUpdate());
        }

        protected virtual IEnumerator DoUpdate()
        {
            while (true)
            {
                //CheckAirDrop();
                yield return new WaitForSeconds(_testSpawnTime);
                if (_poops < _limitCount)
                {
                    OnSpawnPoop?.Invoke();
                    _poops += 1;
                }
            }
        }

        public void Collect()
        {
            _poops = 0;
        }
    }
}