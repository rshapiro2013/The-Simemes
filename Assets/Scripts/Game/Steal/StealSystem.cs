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
#if LOCAL_TEST

            System.DateTime now = System.DateTime.Now;
            foreach (ChestDatas data in list)
            {
                data.ChestDataList.Clear();
                int count = Random.Range(3, 9);

                for (int i = 0; i < count; ++i)
                {
                    data.ChestDataList.Add(new Treasures.ChestData
                    {
                        ChestID = 21011,
                        EndTime = ((System.DateTimeOffset)now.AddSeconds(Random.Range(1000f, 86400f))).ToUnixTimeSeconds(),
                        IsSealed = true,
                        SlotID = i,
                        StartTime = ((System.DateTimeOffset)now).ToUnixTimeSeconds(),
                        BuffID = Random.Range(0, 100) > 75 ? 5001 : 0,
                        Treasures = new List<int> { Random.Range(2201, 2227) }
                    }); ;
                }
            }
#endif
            //OnUpdateChests?.Invoke();
        }
    }
}