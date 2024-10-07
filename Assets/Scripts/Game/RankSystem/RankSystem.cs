using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Utilities;
using System.Threading.Tasks;
using Simemes.Request;

namespace Simemes.Rank
{
    public class RankSystem : GenericSystem<RankSystem, List<RankData>>
    {
        protected int _userRank;
        public int UserRank => _userRank;
        public async override Task SyncData()
        {
            if (_data == null)
                _data = new List<RankData>();
            await RankRequest.Rank(response=>
            {
                _data.Clear();
                _data.AddRange(response.rankDatas);
                _userRank = response.userRank;
            });
        }
    }
}