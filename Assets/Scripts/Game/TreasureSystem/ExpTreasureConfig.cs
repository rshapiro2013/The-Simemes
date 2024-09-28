using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simemes.Treasures
{
    [CreateAssetMenu(fileName = "TreasureItem", menuName = "Simemes/Treasure/ExpItem")]
    public class ExpTreasureConfig : TreasureConfig
    {
        [Header("ExpItem")]
        [SerializeField]
        protected int _exp;

        public override void Obtain(int count = 1)
        {
            base.Obtain(count);

            // ¿Ú±o∏g≈Á≠»
            GameManager.instance.PlayerProfile.AddExp(count * _exp);
        }
    }
}