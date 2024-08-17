using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Utilities;

namespace Simemes.Poop
{
    public class PoopSystem : MonoSingleton<PoopSystem>
    {
        public System.Action OnSpawnPoop;

        [SerializeField] private int _limitCount = 50;
        [SerializeField] private int _props;
        [SerializeField] private float _testSpawnTime = 5;


        public int PropCount => _props;

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
                if (_props < _limitCount)
                {
                    OnSpawnPoop?.Invoke();
                    _props += 1;
                }
            }
        }

        public void Collect()
        {
            _props = 0;
        }
    }
}