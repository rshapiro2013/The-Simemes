using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simemes.Treasures
{
    public interface ITreasure
    {
        // 寶物ID
        int ID { get; }

        // 寶物的權重
        int Weight { get; }
        
        // 圖片
        Sprite Image { get; }

        // 產生寶物的時間戳記
        long CreationTime { get; }

        void Obtain();
    }
}
