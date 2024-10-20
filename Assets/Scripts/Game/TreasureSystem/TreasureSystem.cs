using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Utilities;
using System.Threading.Tasks;
using Simemes.Request;

namespace Simemes.Treasures
{
    public class TreasureSystem : MonoSingleton<TreasureSystem>
    {
        [SerializeField]
        private List<TreasureBoxConfig> _treasureBoxConfigs;

        [SerializeField]
        private List<TreasureConfig> _treasureItems;

        [SerializeField]
        private List<TreasureBuffConfig> _treasureBuffs;

        private List<ChestData> _chests;

        public event System.Action OnUpdateChests;

        public List<TreasureConfig> TreasureItems => _treasureItems;

        public TreasureBoxConfig GetTreasureBoxConfig(int id)
        {
            return _treasureBoxConfigs.Find(x => x.ID == id);
        }

        public TreasureConfig GetTreasureConfig(int id)
        {
            return _treasureItems.Find(x => x.ID == id);
        }

        public TreasureBuffConfig GetBuff(int id)
        {
            return _treasureBuffs.Find(x => x.ID == id);
        }

        public List<ChestData> GetTreasureBoxes()
        {
            return _chests;
        }

        public async Task Init()
        {
            TreasureRequest.OnUpdateList += UpdateTreasureList;
            await TreasureRequest.GetTreasures();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            TreasureRequest.OnUpdateList -= UpdateTreasureList;
        }

        private void UpdateTreasureList(List<ChestData> list)
        {
            _chests = list;
            OnUpdateChests?.Invoke();
        }
    }
}