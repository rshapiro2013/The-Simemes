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

        public override void Obtain()
        {
            base.Obtain();

            // ¿Ú±o∏g≈Á≠»
            GameManager.instance.AddExp(_exp);
        }

    }
}