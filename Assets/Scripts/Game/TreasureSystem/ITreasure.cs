using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simemes.Treasures
{
    public interface ITreasure
    {
        // �_��ID
        int ID { get; }

        // �_�����v��
        int Weight { get; }
        
        // �Ϥ�
        Sprite Image { get; }

        // �����_�����ɶ��W�O
        long CreationTime { get; }

        void Obtain();
    }
}
