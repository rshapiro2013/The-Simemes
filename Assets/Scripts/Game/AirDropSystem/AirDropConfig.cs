using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simemes.AirDrop
{
    [CreateAssetMenu(fileName = "AirDropConfig", menuName = "Simemes/AirDrop/AirDropConfig")]
    public class AirDropConfig : ScriptableObject
    {
        [System.Serializable]
        public class Drop
        {
            public int ItemID;
            public int Chance;
        }

        [SerializeField]
        private List<Drop> _dropItems;

        public int GetRandomDropItem()
        {
            int totalWeight = 0;

            foreach (var drop in _dropItems)
                totalWeight += drop.Chance;

            int randomValue = Random.Range(0, totalWeight);

            foreach (var drop in _dropItems)
            {
                if (randomValue < drop.Chance)
                    return drop.ItemID;

                randomValue -= drop.Chance;
            }

            return -1;
        }
    }
}
