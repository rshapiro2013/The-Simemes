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

            var saveData = GameManager.instance.SaveData;

            // 解鎖圖鑑
            saveData.Collection.Unlock(_id);

            // 獲得經驗值
            saveData.Profile.AddExp(count * _exp);
        }

        public override string GetEffect()
        {
            return $"{_exp} XP";
        }
    }
}