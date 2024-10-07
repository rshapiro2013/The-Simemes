using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Utilities;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Simemes.Request;

namespace Simemes
{
    public abstract class GenericSystem<T, U> : MonoSingleton<T> where T : MonoSingleton<T>
    {
        protected U _data = default;

        public U Datas => _data;

        public async virtual Task Init()
        {
            await SyncData();
        }

        public async virtual Task SyncData() => await UniTask.DelayFrame(0);

        protected virtual void OnUpdateBase(U data)
        {
            _data = data;
        }
    }
}