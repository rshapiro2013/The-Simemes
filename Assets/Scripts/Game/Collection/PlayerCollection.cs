using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simemes.Collection
{
    public class PlayerCollection
    {
        public Dictionary<int, int> CollectionState = new Dictionary<int, int>();

        public void Unlock(int id)
        {
            if (CollectionState == null)
                CollectionState = new Dictionary<int, int>();

            CollectionState[id] = 1;
        }

        public bool IsUnlocked(int id)
        {
            if (!CollectionState.TryGetValue(id, out var state))
                return false;

            return state == 1;

        }
    }
}