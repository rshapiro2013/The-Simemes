using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simemes.TreasureSystem
{
    public interface ITreasure
    {
        // �_�����v��
        int Weight { get; }
        
        // �����_�����ɶ��W�O
        long CreationTime { get; }
    }
}
