using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Utilities;
using System.Threading.Tasks;
using Simemes.Request;

namespace Simemes.Steal
{
    public class StealSystem : MonoSingleton<StealSystem>
    {
        private List<ChestDatas> _chests;

        //public event System.Action OnUpdateChests;

        public List<ChestDatas> ChestDatas => _chests;

        public async Task Init()
        {
            StealRequest.OnUpdateChests += OnUpdateChests;
            await TreasureRequest.GetTreasures();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            StealRequest.OnUpdateChests -= OnUpdateChests;
        }

        private void OnUpdateChests(List<ChestDatas> list)
        {
            _chests = list;
            //OnUpdateChests?.Invoke();
        }
    }
}