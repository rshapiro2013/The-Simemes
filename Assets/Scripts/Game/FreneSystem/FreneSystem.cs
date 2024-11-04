using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Utilities;
using System.Threading.Tasks;
using Simemes.Request;
using Simemes.Tier;

namespace Simemes.Frene
{
    public class FreneSystem : GenericSystem<FreneSystem, List<FreneData>>
    {
        private List<FriendRequestData> _frendRequestData = new List<FriendRequestData>();
        public List<FriendRequestData> FreneRequestDatas => _frendRequestData;


        private Dictionary<string, FreneData> _frendMap = new Dictionary<string, FreneData>();

        public async override Task Init()
        {
            await SyncData();
            await SyncFreneRequestData();
            _frendMap.Clear();

            foreach(FreneData f in _data)
            {
                _frendMap[f.id] = f;
            }
        }

        public async override Task SyncData()
        {
            if (_data == null)
                _data = new List<FreneData>();
            await FriendRequest.GetFriend(OnUpdateBase);

#if LOCAL_TEST
            if (_data.Count == 0)
            {
                TierSystem tierSystem = TierSystem.instance;
                int count = Random.Range(10, 20);

                for (int i = 0; i < count; ++i)
                {
                    int count2 = Random.Range(3, 8);
                    List<string> items = new List<string>();

                    for (int j = 0; j < count2; ++j)
                    {
                        items.Add(Random.Range(2201, 2227).ToString());
                    }

                    items.Sort();

                    _data.Add(new FreneData
                    {
                        id = i.ToString(),
                        name = UI.UIStolenInfo.Names[i],
                        coinAmount = Random.Range(1000, 10000), 
                        screenName = name, 
                        items = items,
                        background = tierSystem.GetTierData(Random.Range(1, tierSystem.TierCount)).Background
                    });
                }
            }
#endif
        }

        public async Task SyncFreneRequestData()
        {
            await FriendRequest.GetFriendRequest(list =>
            {
                if (list != null)
                {
                    _frendRequestData.Clear();
                    _frendRequestData.AddRange(list);
                }
            });
        }

        public async Task SearchFrene(string id, System.Action<List<FreneData>> callback = null)
        {
            await FriendRequest.PostFriend(id, callback);
        }

        public async void AddFrene(string id, System.Action<List<FriendRequestData>> callback = null)
        {
            await FriendRequest.PostFriendRequest(id, isRequest:true, callback: callback);
        }

        public async void ConfirmeFrene(string id, System.Action<List<FriendRequestData>> callback = null)
        {
            await FriendRequest.PostFriendRequest(id, isConfirmed: true, callback: callback);
        }

        public async void RejecteFrene(string id, System.Action<List<FriendRequestData>> callback = null)
        {
            await FriendRequest.PostFriendRequest(id, isRejected: true, callback: callback);
        }

        public async void CancelleFrene(string id, System.Action<List<FriendRequestData>> callback = null)
        {
            await FriendRequest.PostFriendRequest(id, isCancelled: true, callback: callback);
        }

        public bool HasFrene(FreneData data) => _data.Contains(data);


        public bool HasFrene(string name) => _frendMap.ContainsKey(name);
    }
}